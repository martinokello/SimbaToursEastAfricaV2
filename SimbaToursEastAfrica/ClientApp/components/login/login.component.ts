import { Component, OnInit, ViewChild, ElementRef, Injectable, AfterViewInit, AfterViewChecked, Inject, Output, Input, EventEmitter } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import {SafariTourServices, IUserDetail, IUserStatus } from '../../services/safariTourServices';
import 'rxjs/add/operator/map';


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
    public constructor(safarTourService: SafariTourServices) {
        this.safariTourService = safarTourService;
    }
    public loginUser(): void {
        let loginResults: Observable<any> = this.safariTourService.LoginByPost(this.userDetail);
        loginResults.map((q: any) => {
            if (q.isLoggedIn) {
                SafariTourServices.actUserStatus.isUserLoggedIn = q.isLoggedIn;
                if (q.isAdministrator) {
                    SafariTourServices.actUserStatus.isUserAdministrator = q.isAdministrator;
                }
                SafariTourServices.isLoginPage = false;
                SafariTourServices.SetUserEmail(this.userDetail.emailAddress);
                alert('You are logged in: ' + q.isLoggedIn);
            }
        }).subscribe();
    }
}