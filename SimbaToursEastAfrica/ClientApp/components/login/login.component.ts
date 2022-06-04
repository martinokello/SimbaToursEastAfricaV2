import { Component, OnInit, ViewChild, ElementRef, Injectable, AfterViewInit, AfterViewChecked, AfterContentInit, Inject, Output, Input, EventEmitter } from '@angular/core';
import {Router} from '@angular/router';
import { Observable } from 'rxjs/Observable';
import {SafariTourServices, IUserDetail, IUserStatus, IUserLoginStatus } from '../../services/safariTourServices';
import 'rxjs/add/operator/map';
import * as $ from 'jquery';

@Component({
    selector: 'login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css'],
    providers: [SafariTourServices]
})
@Injectable()
export class LoginComponent implements AfterContentInit{

    public userDetail: IUserDetail | any;
    private safariTourService: SafariTourServices | any;
    private router: Router;
    @Output() loggedInEvent = new EventEmitter<boolean>();
    @Output() isAdminEvent = new EventEmitter<boolean>();

    ngAfterContentInit(): void {

        this.userDetail = {
            password: "",
            role: "",
            emailAddress: "",
            repassword: "",
            name: "",
            keepLoggedIn: false
        };
    }
    public constructor(safarTourService: SafariTourServices, router: Router) {
        this.safariTourService = safarTourService;
        this.router = router;
    }
    public loginUser(): void {
        let loginResults: Observable<IUserLoginStatus> = this.safariTourService.LoginByPost(this.userDetail);
        loginResults.map((q: IUserLoginStatus) => {
            if (q.isLoggedIn) {
                $('span#loginName').css('display', 'block');
                $('span#loginName').text("logged in as: " + this.userDetail.emailAddress);
                SafariTourServices.isLoginPage = false;
                SafariTourServices.SetUserEmail(this.userDetail.emailAddress);
                let userLoggedIn: IUserStatus = {
                    isUserLoggedIn: true,
                    isUserAdministrator: q.isAdministrator
                };
                //localStorage.removeItem('actUserStatus');
                localStorage.setItem('actUserStatus', JSON.stringify(userLoggedIn));
                this.loggedInEvent.emit(true);
                this.isAdminEvent.emit(q.isAdministrator);
                this.router.navigateByUrl('/book-tour');
            }
            else {
                let userLoggedOut: IUserStatus = {
                    isUserLoggedIn: false,
                    isUserAdministrator: false
                };
                localStorage.setItem('actUserStatus', JSON.stringify(userLoggedOut));
                alert('Login Failed. Unknown User')
            }
        }).subscribe();
    }
}