import { Component, OnInit, ViewChild, ElementRef, Injectable, AfterViewInit, AfterViewChecked, Inject, Input, Output, EventEmitter  } from '@angular/core';
import { ILaguage, SafariTourServices, IHotelBooking, ItemType, ILaguagePricing } from '../../services/safariTourServices';
import * as $ from "jquery";
import { Observable } from 'rxjs/Observable';

@Component({
    selector: 'laguage',
    templateUrl: './laguage.component.html',
    styleUrls: ['./laguage.component.css'],
    providers: [SafariTourServices]
})
@Injectable()
export class LaguageComponent implements OnInit, AfterViewInit, AfterViewChecked {

    private safariTourService: SafariTourServices | any;

    @ViewChild('laguageItem') laguageItem: HTMLElement|any;
    @Input()
    public hotelBooking: IHotelBooking|any;
    public laguagePrices: ILaguagePricing[] | any;

    @Output() valueChangeLaguage = new EventEmitter<ILaguage>();
    public laguage: ILaguage|any;
    public constructor(safariTourService: SafariTourServices) {
       
        this.safariTourService = safariTourService;
    }
    public addLaguage(): void {
        this.updateLaguageAndPricingReferences();
    }
    public updateLaguageAndPricingReferences(): void {
        //get hotel pricing and location:
        let pricings: Observable<any> = this.safariTourService.GetLaguagePricing();

        pricings.subscribe((data: any) => {
            let p: ILaguagePricing[] = data;
            this.laguagePrices = p;
            this.laguage.laguagePricing = this.laguagePrices[0];
            this.valueChangeLaguage.emit(this.laguage);
            alert('Laguage and assortments added successfully');
        });
    }

    ngAfterViewChecked() {

        console.log("inside After View Checked");    

    }
    ngAfterViewInit() {
        console.log("inside After View Init, " + this.laguageItem);
    }
    ngOnInit() {
        //console.log("inside OnInit");
        let tempLag: ILaguage = {
            laguageId: 0,
            items: [],
            itemType: ItemType.Laguage,
            quantity: 0,
            laguagePricing: {
                laguageDescription:"",
                laguagePricingId:0,
                laguagePricingName:"",
                unitLaguagePrice: 0.00,
                unitTravelDocumentPrice: 0.00,
                unitMedicalPrice: 0.00,
                unitMealPrice:0.00
            },
            laguagePricingId:0
        }
        this.laguage = tempLag;
        this.updateLaguageAndPricingReferences();
        this.hotelBooking = {};
        this.hotelBooking.laguages = [];
        let div = this.laguageItem
        console.log(div);
        
        let select = div.nativeElement.querySelector("select");
        console.log(select);
        let items = this.safariTourService.GetItemTypeNames();

        $(select).remove('option');
        $(select).append('<option value="-1" selected="true">Select An Item Type</option>');
       for (let i = 0; i < items.length; i++) {
            $(select).append('<option value="'+ items[i].valueOf().toString()+'">'+ItemType[items[i]] as string+'</option>');
        }
    }
}