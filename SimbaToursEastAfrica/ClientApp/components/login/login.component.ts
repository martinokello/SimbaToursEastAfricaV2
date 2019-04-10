import { Component, OnInit, ViewChild, ElementRef, Injectable, AfterViewInit, AfterViewChecked, Inject, Output, Input, EventEmitter } from '@angular/core';
import {Router} from '@angular/router';
import { Observable } from 'rxjs/Observable';
import {SafariTourServices, IUserDetail, IUserStatus } from '../../services/safariTourServices';
import 'rxjs/add/operator/map';
import * as $ from 'jquery';

@Component({
    selector: 'login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css'],
    providers: [SafariTourServices]
})
@Injectable()
export class LoginComponent implements OnInit{

    public userDetail: IUserDetail | any;
    private safariTourService: SafariTourServices | any;
    private router: Router;

    ngOnInit(): void {

        let userDetail: IUserDetail = {
            password: "",
            role: "",
            emailAddress: "",
            repassword: "",
            name: "",
            keepLoggedIn: false
        };
        this.userDetail = userDetail;
    }
    public constructor(safarTourService: SafariTourServices, router: Router) {
        this.safariTourService = safarTourService;
        this.router = router;
    }
    public loginUser(): void {
        let loginResults: Observable<any> = this.safariTourService.LoginByPost(this.userDetail);
        loginResults.map((q: any) => {
            if (q.isLoggedIn) {
                SafariTourServices.actUserStatus.isUserLoggedIn = q.isLoggedIn;
                $('span#loginName').css('display', 'block');
                $('span#loginName').text("logged in as: " + this.userDetail.emailAddress);
                if (q.isAdministrator) {
                    SafariTourServices.actUserStatus.isUserAdministrator = q.isAdministrator;

                }
                SafariTourServices.isLoginPage = false;
                SafariTourServices.SetUserEmail(this.userDetail.emailAddress);
                this.router.navigateByUrl('/book-tour');
            }
            else {
                alert('Login Failed. Unknown User')
            }
        }).subscribe();
    }
}