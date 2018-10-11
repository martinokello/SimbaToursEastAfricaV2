import { Component, OnInit, ViewChild, ElementRef, Injectable, AfterViewInit, AfterViewChecked, Inject, Input, Output, EventEmitter } from '@angular/core';
import { ILaguage, SafariTourServices, IHotelBooking, ItemType, ISchedulesPricing } from '../../services/safariTourServices';
import * as $ from "jquery";
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
@Component({
    selector: 'schedulesPricing',
    templateUrl: './schedulesPricing.component.html',
    styleUrls: ['./schedulesPricing.component.css'],
    providers: [SafariTourServices]
})
@Injectable()
export class SchedulesPricingComponent implements OnInit, AfterViewInit, AfterViewChecked {

    isAdminUser: boolean | any;
    public schedules: ISchedulesPricing | any;
    model: any;

    private safariTourService: SafariTourServices | any;

    @ViewChild('schedulesPricingItem') schedulesPricingItem: HTMLElement | any;

    public constructor(safariTourService: SafariTourServices) {

        this.safariTourService = safariTourService;
    }
    public updateSchedules() {
        this.safariTourService.UpdateSchedulesPricing(this.schedules).subscribe((data: any) => {
            alert("Schedules Pricing Updated: " + data.result);
        });
    }
    public createOrUpdateSchedules(): void {
        this.safariTourService.PostOrUpdateSchedulesPricing(this.schedules).subscribe((data: any) => {
            alert("Schedules Pricing Set: " + data.result);
        });
    }
    public selectSchedulesPricing() {
        let results: Observable<ISchedulesPricing> = this.safariTourService.GetLaguagePricingById(this.schedules.schedulesPricingId);
        results.map((q: ISchedulesPricing) => {
            this.schedules = q;
        }).subscribe();
    }
    ngAfterViewChecked() {

        console.log("inside After View Checked");

    }
    ngAfterViewInit() {
        console.log("inside After View Init, " + this.schedulesPricingItem);
    }
    ngOnInit() {
        //console.log("inside OnInit");
        this.isAdminUser = SafariTourServices.actUserStatus.isUserAdministrator;
        this.model = {};
        this.model.editable = true;
        this.model.viewable = false;
        let schedulesDefault: ISchedulesPricing = {
            schedulesPricingId: 0,
            schedulesPricingName: "",
            schedulesDescription: "",
            price: 0.00
        };
        this.schedules = schedulesDefault;
        let items: ISchedulesPricing[] = this.safariTourService.GetSchedulesPricing();
        let div = this.schedulesPricingItem;

        let select = div.nativeElement.querySelector("select");
        console.log(select);

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
            $(select).append('<option value="-1" selected="true">Select An Item Type</option>');
            for (let i = 0; i < items.length; i++) {
                $(select).append('<option value="' + items[i].schedulesPricingId + '">' + items[i].schedulesPricingName + '</option>');
            }
        }).subscribe();

    }
}