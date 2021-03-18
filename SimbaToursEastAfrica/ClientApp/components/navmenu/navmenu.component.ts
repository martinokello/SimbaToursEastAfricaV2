import { Component, OnInit, ViewChild, ElementRef, Input,Output, EventEmitter } from '@angular/core';
import { Element } from '@angular/compiler';
import { Observable } from 'rxjs/Observable';
import { SafariTourServices, IUserStatus, IUserDetail } from '../../services/safariTourServices';
import 'rxjs/add/operator/map';
import * as $ from 'jquery';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css'],
    providers: [SafariTourServices]
})
export class NavMenuComponent implements OnInit {
    @Input() actUserStatus: IUserStatus = {
        isUserLoggedIn: false,
        isUserAdministrator: false,
    };
    public safariTourService: SafariTourServices | any;
    public constructor(safariTourService: SafariTourServices) {

        this.safariTourService = safariTourService;
        this.actUserStatus = JSON.parse(localStorage.getItem('actUserStatus'));
    }
    ngOnInit(): void {
        this.actUserStatus = SafariTourServices.actUserStatus;
        this.verifyLoggedInUser();
        if (!this.actUserStatus.isUserLoggedIn &&
        window.location.href.toLowerCase().indexOf('/book-tour') > -1 &&
        window.location.href.toLowerCase().indexOf('/login') > -1 &&
        window.location.href.toLowerCase().indexOf('/register') > -1 &&
        window.location.href.toLowerCase().indexOf('/forgot-password') > -1)
        {
            window.location.href = "/home";
        }
    }

    verifyLoggedInUser(): void {
        let verifyResult: Observable<any> = this.safariTourService.VerifyLoggedInUser();
        verifyResult.map((p: any) => {
            if (p.isLoggedIn) {
                $('span#loginName').css('display', 'block');
                $('span#loginName').text("logged in as: " + p.name);
                SafariTourServices.SetUserEmail(p.name);
                this.safariTourService.actUserStatus.isUserLoggedIn = true;
                this.actUserStatus.isUserLoggedIn = true;
                this.actUserStatus.isUserAdministrator = p.isAdministrator;
                this.actUserStatus = {
                    isUserLoggedIn: true,
                    isUserAdministrator: p.isAdministrator
                }
                localStorage.removeItem('actUserStatus');
                localStorage.setItem('actUserStatus', JSON.stringify(this.actUserStatus))
            }
        }).subscribe();
    }
    makePayments(): void {
    }
    setIsLogInPage(): void {
        SafariTourServices.isLoginPage = true;
    }
    logOut(): void {
        $('span#loginName').css('display', 'none');
        let logOutResult: Observable<any> = this.safariTourService.LogOut();
        logOutResult.map((p:any)=>{
            this.actUserStatus.isUserLoggedIn = this.safariTourService.actUserStatus.isUserLoggedIn = false;
            this.actUserStatus.isUserAdministrator = this.safariTourService.actUserStatus.isUserAdministrator = false;

            this.actUserStatus = SafariTourServices.actUserStatus;
        }).subscribe();

    }
}
