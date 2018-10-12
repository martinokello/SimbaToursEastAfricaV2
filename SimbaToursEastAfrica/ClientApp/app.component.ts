import { Component, OnInit } from '@angular/core';
import { SafariTourServices, IUserDetail, IUserStatus } from './services/safariTourServices';
import { Observable } from 'rxjs/Observable';
import * as $ from 'jquery';
@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
    providers:[SafariTourServices]
})
export class AppComponent implements OnInit {
    actUserStatus: any = SafariTourServices.actUserStatus;
    safariTourService: SafariTourServices | any;
    twitterFeeds: any[];
    public constructor(safariTourService: SafariTourServices) {

        this.safariTourService = safariTourService;
    }
    public ngOnInit(): void {
        SafariTourServices.actUserStatus.isUserLoggedIn = false;
        SafariTourServices.actUserStatus.isUserAdministrator = false;
        this.GetTwitterFeeds();
    }

    public GetTwitterFeeds(): void {

        let twitterResults: Observable<any[]> = this.safariTourService.GetTwitterFeeds();
        twitterResults.map((p: any[]) => {
            this.twitterFeeds = p;
        }).subscribe();
    }
}

