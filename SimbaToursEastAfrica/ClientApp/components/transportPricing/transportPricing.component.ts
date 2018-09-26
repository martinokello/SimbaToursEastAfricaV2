import { Component, OnInit, ViewChild, ElementRef, Injectable, AfterViewInit, AfterViewChecked, Inject, Input, Output, EventEmitter } from '@angular/core';
import { ILaguage, SafariTourServices, IHotelBooking, ItemType, ISchedulesPricing, ITransportPricing } from '../../services/safariTourServices';
import * as $ from "jquery";
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
@Component({
    selector: 'transportPricing',
    templateUrl: './transportPricing.component.html',
    styleUrls: ['./transportPricing.component.css'],
    providers: [SafariTourServices]
})
@Injectable()
export class TransportPricingComponent implements OnInit, AfterViewInit, AfterViewChecked {

    public transportPricing: ITransportPricing | any;
    model: any;

    private safariTourService: SafariTourServices | any;

    @ViewChild('transportPricingItem') transportPricingItem: HTMLElement | any;

    public constructor(safariTourService: SafariTourServices) {

        this.safariTourService = safariTourService;
    }
    public updateTransportPricing() {
        this.safariTourService.UpdateTransportPricing(this.transportPricing).subscribe((data: any) => {
            alert("Transport Pricing Updated: " + data.result);
        });
    }
    public createTransportPricing(): void {
        this.safariTourService.CreateTransportPricing(this.transportPricing).subscribe((data: any) => {
            alert("Transport Pricing Set: " + data.result);
        });
    }
    public selectTransportPricing() {
        let results: Observable<ITransportPricing> = this.safariTourService.GetTransportPricingById(this.transportPricing.schedulesPricingId);
        results.map((q: ITransportPricing) => {
            this.transportPricing = q;
        }).subscribe();
    }
    ngAfterViewChecked() {

        console.log("inside After View Checked");

    }
    ngAfterViewInit() {
        console.log("inside After View Init, " + this.transportPricing);
    }
    ngOnInit() {
        //console.log("inside OnInit");
        this.model = {};
        this.model.editable = true;
        this.model.viewable = false;
        let transportPricingDefault: ITransportPricing = {
            transportPricingId: 0,
            transportPricingName: "",
            description: "",
            taxiPricing: 0.00,
            pickupTruckPricing: 0.00,
            tourBusPricing: 0.00,
            miniBusPricing: 0.00,
            fourByFourPricing: 0.00
        };
        this.transportPricing = transportPricingDefault;
        let div = this.transportPricingItem;

        let select = div.nativeElement.querySelector("select");
        console.log(select);

        let actualResult: Observable<ITransportPricing[]> = this.safariTourService.GetTransportPricing();
        actualResult.map((p: ITransportPricing[]) => {
            let items = p;
            if (items && items.length > 0) {
                this.transportPricing = items[0];
            } else {
                let transportPricingDefault: ITransportPricing = {
                    transportPricingId: 0,
                    transportPricingName: "",
                    description: "",
                    taxiPricing: 0.00,
                    pickupTruckPricing: 0.00,
                    tourBusPricing: 0.00,
                    miniBusPricing: 0.00,
                    fourByFourPricing: 0.00
                };
                this.transportPricing = transportPricingDefault;
                items = [];
            }

            $(select).remove('option');
            $(select).append('<option value="-1" selected="true">Select An Item Type</option>');
            for (let i = 0; i < items.length; i++) {
                $(select).append('<option value="' + items[i].transportPricingId + '">' + items[i].transportPricingName + '</option>');
            }
        }).subscribe();

    }
}