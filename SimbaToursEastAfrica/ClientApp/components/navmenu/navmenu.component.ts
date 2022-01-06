import { AfterContentInit, Component, Input, OnInit } from '@angular/core';
import { Element } from '@angular/compiler';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/filter';
import { SafariTourServices, IUserStatus, IUserDetail, IUserLoginStatus } from '../../services/safariTourServices';
import * as $ from 'jquery';
import { Router, NavigationEnd } from '@angular/router';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css'],
    providers: [SafariTourServices]
})
export class NavMenuComponent implements AfterContentInit, OnInit {
    @Input() actUserStatus: IUserStatus;
    public safariTourService: SafariTourServices | any;
    public constructor(safariTourService: SafariTourServices, private router: Router) {

        this.safariTourService = safariTourService;
        this.actUserStatus = {
            isUserLoggedIn: false,
            isUserAdministrator: false
        };
        this.router = router;
        this.router.events.filter(evt => evt instanceof NavigationEnd).subscribe((val: any) => {
            this.myInit();
        });
    }
    ngOnInit() {
        this.actUserStatus = {
            isUserLoggedIn: false,
            isUserAdministrator: false
        };
    }

    ngAfterContentInit():void {

        let actUserLoginStatus: IUserLoginStatus = JSON.parse(localStorage.getItem('actUserLoginStatus'));
        if (actUserLoginStatus) {

            this.actUserStatus.isUserLoggedIn = actUserLoginStatus.isLoggedIn;
            this.actUserStatus.isUserAdministrator = actUserLoginStatus.isAdministrator;
            localStorage.removeItem('actUserStatus');
            localStorage.setItem('actUserStatus', JSON.stringify(this.actUserStatus));
        }

        if (!this.actUserStatus.isUserLoggedIn) this.logOut();
    }
    myInit(): void {
        this.actUserStatus = JSON.parse(localStorage.getItem('actUserStatus'));
        this.verifyLoggedInUser();
        if (!this.actUserStatus.isUserLoggedIn &&
        window.location.href.toLowerCase().indexOf('/book-tour') > -1 &&
        window.location.href.toLowerCase().indexOf('/register') > -1 &&
        window.location.href.toLowerCase().indexOf('/forgot-password') > -1)
        {
            this.router.navigateByUrl("/home");
        }
    }

    verifyLoggedInUser(): void {
        let verifyResult: Observable<any> = this.safariTourService.VerifyLoggedInUser();
        verifyResult.map((p: any) => {
            if (p.isLoggedIn) {
                $('span#loginName').css('display', 'block');
                $('span#loginName').text("logged in as: " + p.name);
                SafariTourServices.SetUserEmail(p.name);
                this.actUserStatus = {
                    isUserLoggedIn: true,
                    isUserAdministrator: p.isAdministrator
                }
                localStorage.removeItem('actUserStatus');
                localStorage.setItem('actUserStatus', JSON.stringify(this.actUserStatus));
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
        $('span#loginName').text("");
        let userLoggedOut: IUserStatus = {
            isUserLoggedIn: false,
            isUserAdministrator: false
        };
        localStorage.setItem('actUserLoginStatus', null);
        this.actUserStatus = userLoggedOut;
        let logOutResult: Observable<any> = this.safariTourService.LogOut();
        logOutResult.map((p:any)=>{
            localStorage.removeItem('actUserStatus');
            localStorage.setItem('actUserStatus', JSON.stringify(userLoggedOut));
            this.router.navigateByUrl("/home");

        }).subscribe();
        
    }
}
