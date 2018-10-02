import { Component, OnInit } from '@angular/core';
import { SafariTourServices, IUserDetail, IUserStatus } from './services/safariTourServices';
@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
    actUserStatus: any = SafariTourServices.actUserStatus;
    public ngOnInit(): void {
        SafariTourServices.actUserStatus.isUserLoggedIn = false;
        SafariTourServices.actUserStatus.isUserAdministrator = false;
    }
}

