import { Component, OnInit, ViewChild, ElementRef, Injectable, AfterViewInit, AfterViewChecked, Inject, Input, Output, EventEmitter } from '@angular/core';
import { ILaguage, SafariTourServices, IHotelBooking, ItemType, ILaguagePricing } from '../../services/safariTourServices';
import * as $ from "jquery";
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
@Component({
    selector: 'laguagePricing',
    templateUrl: './laguagePricing.component.html',
    styleUrls: ['./laguagePricing.component.css'],
    providers: [SafariTourServices]
})
@Injectable()
export class LaguagePricingComponent implements OnInit, AfterViewInit, AfterViewChecked {

    public laguage: ILaguagePricing | any;
    public safariTourService: SafariTourServices | any;
    model: any;

    @ViewChild('laguagePricingItem') laguagePricingItem: HTMLElement | any;

    public constructor(safariTourService: SafariTourServices) {

        this.safariTourService = safariTourService;
    }
    public createOrUpdateLaguagePricing(): void {
        
        this.safariTourService.PostOrUpdateLaguagePricing(this.laguage).subscribe((data: any)=> {
            alert("Laguage Pricing Set: " + data.result);
        });
    }

    public updateLaguagePricing() {
        this.safariTourService.UpdateLaguagePricing(this.laguage).subscribe((data: any) => {
            alert("Laguage Pricing Updated: " + data.result);
        });
    }
    public selectLaguagePricing() {
        let results: Observable<ILaguagePricing> = this.safariTourService.GetLaguagePricingById(this.laguage.laguagePricingId);
        results.map((q: ILaguagePricing) => {
            this.laguage = q;
        }).subscribe();
    }
    ngAfterViewChecked() {

        console.log("inside After View Checked");

    }
    ngAfterViewInit() {
        console.log("inside After View Init, " + this.laguagePricingItem);
    }
    ngOnInit() {
        this.model = {};
        this.model.editable = true;
        this.model.viewable = false;

        let defualtLaguage: ILaguagePricing = {
            unitLaguagePrice: 0.00,
            unitMedicalPrice: 0.00,
            unitTravelDocumentPrice: 0.00,
            unitMealPrice: 0.00,
            laguageDescription: "",
            laguagePricingName: "",
            laguagePricingId: 0
        }
        this.laguage = defualtLaguage;

        let actualResult: Observable<ILaguagePricing[]> = this.safariTourService.GetLaguagePricing();
        actualResult.map((p: ILaguagePricing[]) => {
            let items: ILaguagePricing[] = p;

            let div = this.laguagePricingItem;
            let select = div.nativeElement.querySelector("select");
            console.log(select);
 

            if (items && items.length > 0) {
                this.laguage = items[0];
            }
            else {
                let defualtLaguage: ILaguagePricing = {
                    unitMealPrice:0.00,
                    unitLaguagePrice: 0.00,
                    unitMedicalPrice: 0.00,
                    unitTravelDocumentPrice: 0.00,
                    laguageDescription: "",
                    laguagePricingName: "",
                    laguagePricingId: 0,
                }
                this.laguage = defualtLaguage;
                items = [];
            }
            $(select).remove('option');
            $(select).append('<option value="-1" selected="true">Select An Item Type</option>');
            for (let i = 0; i < items.length; i++) {
                $(select).append('<option value="' + items[i].laguagePricingId + '">' + items[i].laguagePricingName + '</option>');
            }
            alert('Location Added: ' + p);
        }).subscribe();
        
    }
}