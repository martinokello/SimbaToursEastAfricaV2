import { Component, OnInit, ViewChild, ElementRef, Injectable, AfterViewInit, AfterViewChecked, Inject, Input, Output, EventEmitter, AfterContentInit } from '@angular/core';
import { ILaguage, SafariTourServices, IHotelBooking, ItemType, ISchedulesPricing, IUserStatus } from '../../services/safariTourServices';
import * as $ from "jquery";
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
@Component({
    selector: 'schedulesAdmin',
    templateUrl: './schedulesAdmin.component.html',
    styleUrls: ['./schedulesAdmin.component.css'],
    providers: [SafariTourServices]
})
@Injectable()
export class SchedulesAdminComponent implements OnInit, AfterContentInit {

    isAdminUser: boolean | any;
    public schedules: ISchedulesPricing | any;
    model: any;

    private safariTourService: SafariTourServices | any;

    @ViewChild('schedulesPricingItem') schedulesPricingItem: HTMLElement | any;

    public constructor(safariTourService: SafariTourServices) {

        this.safariTourService = safariTourService;
    }
    public updateSchedules() {
        let div = this.schedulesPricingItem;

        let select = div.nativeElement.querySelector("select");
        this.schedules.schedulesPricingId = $(select).val();
        this.safariTourService.UpdateSchedulesPricing(this.schedules).subscribe((data: any) => {
            alert("Schedules Pricing Updated: " + data.result);
        });
    }
    public createSchedules(): void {
        this.safariTourService.PostOrCreateSchedulesPricing(this.schedules).subscribe((data: any) => {
            alert("Schedules Pricing Set: " + data.result);
        });
    }
    public selectSchedulesPricing() {
        let div = this.schedulesPricingItem;

        let select = div.nativeElement.querySelector("select");
        this.schedules.schedulesPricingId = $(select).val();

        let results: Observable<ISchedulesPricing> = this.safariTourService.GetSchedulesPricingById(this.schedules.schedulesPricingId);
        results.map((q: ISchedulesPricing) => {
            this.schedules = q;
        }).subscribe();
    }
    ngAfterViewChecked() {

        console.log("inside After View Checked");

    }

    ngAfterContentInit() {
        this.model = {};
        let userDetails: IUserStatus = JSON.parse(localStorage.getItem("actUserStatus"));

        this.isAdminUser = userDetails.isUserAdministrator;
        if (this.isAdminUser) {
            this.model.editable = true;
            this.model.viewable = false;
        }
        else {
            this.model.editable = false;
            this.model.viewable = true;
        }
    }

    ngOnInit() {
        this.model = {};

        let userDetails: IUserStatus = JSON.parse(localStorage.getItem("actUserStatus"));
        this.isAdminUser = userDetails.isUserAdministrator;

        if (this.isAdminUser) {
            this.model.editable = true;
            this.model.viewable = false;
        } else {
            this.model.editable = false;
            this.model.viewable = true;
        }

        let schedulesDefault: ISchedulesPricing = {
            schedulesPricingId: 0,
            schedulesPricingName: "",
            schedulesDescription: "",
            price: 0.00
        };
        this.schedules = schedulesDefault;
        let items: ISchedulesPricing[]|any = [];
        let div = this.schedulesPricingItem;

        let select = div.nativeElement.querySelector("select");

        let actualResult: Observable<ISchedulesPricing[]> = this.safariTourService.GetSchedulesPricing();
        actualResult.map((p: ISchedulesPricing[]) => {
            items = p;
            if (items && items.length > 0) {
                this.schedules = items[0];
            } else {
                let schedulesDefault: ISchedulesPricing = {
                    schedulesPricingId: 0,
                    schedulesPricingName: "",
                    schedulesDescription: "",
                    price: 0.00
                };
                this.schedules = schedulesDefault;
                items = [];
            }

            $(select).remove('option');
            $(select).append('<option value="0" selected="true">Select An Item Type</option>');
            for (let i = 0; i < items.length; i++) {
                $(select).append('<option value="' + items[i].schedulesPricingId + '">' + items[i].schedulesPricingName + '</option>');
            }
        }).subscribe();

    }
}