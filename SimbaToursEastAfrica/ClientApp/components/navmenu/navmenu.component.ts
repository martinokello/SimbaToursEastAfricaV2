import { Component, OnInit, ViewChild, ElementRef, Input,Output, EventEmitter } from '@angular/core';
import { Element } from '@angular/compiler';
import { Observable } from 'rxjs/Observable';
import { SafariTourServices, IUserStatus } from '../../services/safariTourServices';
import 'rxjs/add/operator/map';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css'],
    providers: [SafariTourServices]
})
export class NavMenuComponent implements OnInit {
    @Input() actUserStatus: IUserStatus = {
        isUserLoggedIn: false,
        isUserAdministrator: false
    };
    private safariTourService: SafariTourServices | any;
    public constructor(safariTourService: SafariTourServices) {

        this.safariTourService = safariTourService;
    }
    ngOnInit(): void {
        if (!this.actUserStatus.isUserLoggedIn &&
        window.location.href.toLowerCase().indexOf('/book-tour') > -1 &&
        window.location.href.toLowerCase().indexOf('/login') > -1 &&
        window.location.href.toLowerCase().indexOf('/register') > -1 &&
        window.location.href.toLowerCase().indexOf('/forgot-password') > -1)
        {
            window.location.href = "/home";
        }
    }
    makePayments(): void {
    }
    setIsLogInPage(): void {
        SafariTourServices.isLoginPage = true;
    }
    logOut(): void {

        this.actUserStatus.isUserLoggedIn = SafariTourServices.actUserStatus.isUserLoggedIn = false;
        this.actUserStatus.isUserAdministrator = SafariTourServices.actUserStatus.isUserAdministrator = false;
        let logOutResult: Observable<any> = this.safariTourService.LogOut();
        logOutResult.map((p:any)=>{
            this.actUserStatus.isUserLoggedIn = SafariTourServices.actUserStatus.isUserLoggedIn = false;
            this.actUserStatus.isUserAdministrator = SafariTourServices.actUserStatus.isUserAdministrator = false;
        }).subscribe();

    }
}
