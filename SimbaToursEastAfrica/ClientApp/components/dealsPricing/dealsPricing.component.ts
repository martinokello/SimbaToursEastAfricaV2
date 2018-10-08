import { Component, OnInit, ViewChild, ElementRef, Injectable, AfterViewInit, AfterViewChecked, Inject, Input, Output, EventEmitter  } from '@angular/core';
import { ILaguage, SafariTourServices, IHotelBooking, ItemType, IDealsPricing } from '../../services/safariTourServices';
import * as $ from "jquery";
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

@Component({
    selector: 'dealsPricing',
    templateUrl: './dealsPricing.component.html',
    styleUrls: ['./dealsPricing.component.css'],
    providers: [SafariTourServices]
})
@Injectable()
export class DealsPricingComponent implements OnInit, AfterViewInit, AfterViewChecked {
    public deal: IDealsPricing | any;
    model: any;
    private safariTourService: SafariTourServices | any;

    @ViewChild('dealsPricingItem') dealsPricingItem: HTMLElement | any;

    public constructor(safariTourService: SafariTourServices) {

        this.safariTourService = safariTourService;
    }
    public updateDeal() {
        this.safariTourService.UpdateDealsPricing(this.deal).subscribe((data: any) => {
            alert("Deals Pricing Updated: " + data.result);
        });
    }
    public createOrUpdateDeal(): void {
        this.safariTourService.PostOrUpdateDealsPricing(this.deal).subscribe((data: any) => {
            alert("Deals Pricing Set: " + data.result);
        });
    }
    public selectDealsPricing(): void {
        let results: Observable<IDealsPricing> = this.safariTourService.GetLaguagePricingById(this.deal.dealPricingId);
        results.map((q: IDealsPricing) => {
            this.deal = q;
        }).subscribe();
    }
    ngAfterViewChecked() {

        console.log("inside After View Checked");    

    }
    ngAfterViewInit() {
        console.log("inside After View Init, " + this.dealsPricingItem);
    }
    ngOnInit() {
        console.log("inside OnInit");
        this.model = {};
        this.model.editable = true;
        this.model.viewable = false;
        let defaultItem: IDealsPricing = {
            dealName: "",
            dealsPricingId: 0,
            description: "",
            price: 0.00
        };
        this.deal = defaultItem;

        console.log("inside OnInit - model.editable: " + this.model.editable + ", model.viewable: " + this.model.viewable);
        let div = this.dealsPricingItem;
        let select = div.nativeElement.querySelector("select");

        let actualRes: Observable<IDealsPricing[]> = this.safariTourService.GetDealsPricing();

        actualRes.map((p: IDealsPricing[]) => {
            let items: IDealsPricing[] | any[];
            try {
                items = p;
            }
            catch (ex) {
                items = [];
            }
            if (items && items.length > 0) {
                this.deal = items[0];
            } else {
                let defaultItem: IDealsPricing = {
                    dealName: "",
                    dealsPricingId: 0,
                    description: "",
                    price: 0.00
                };
                this.deal = defaultItem;
                items = [];
            }

            $(select).remove('option');
            $(select).append('<option value="-1" selected="true">Select An Item Type</option>');
            for (let i = 0; i < items.length; i++) {
                $(select).append('<option value="' + items[i].dealsPricingId + '">' + items[i].dealName + '</option>');
            }
            console.log(select);
        }).subscribe();
    }
}