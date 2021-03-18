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
    safariTourService: SafariTourServices;
    actUserStatus: IUserStatus;
    twitterFeeds: any[];
    public constructor(safariTourService: SafariTourServices) {
        this.safariTourService = safariTourService;
    }
    public ngOnInit(): void {
        this.actUserStatus = JSON.parse(localStorage.getItem('actUserStatus'));
        if (!this.actUserStatus) {
            this.actUserStatus = {
                isUserLoggedIn : false,
                isUserAdministrator : false
            }
        }
        this.GetTwitterFeeds();
    }

    public GetTwitterFeeds(): void {

        let twitterResults: Observable<any[]> = this.safariTourService.GetTwitterFeeds();
        twitterResults.map((p: any[]) => {
            this.twitterFeeds = p;
        }).subscribe();
    }
}

