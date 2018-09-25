import { Component, OnInit, ViewChild, ElementRef, Injectable, AfterViewInit, AfterViewChecked, Inject, Input, Output, EventEmitter, OnChanges } from '@angular/core';
import { ILaguage, SafariTourServices, IHotelBooking, ItemType, IHotelPricing } from '../../services/safariTourServices';
import * as $ from "jquery";
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

@Component({
    selector: 'hotelPricing',
    templateUrl: './hotelPricing.component.html',
    styleUrls: ['./hotelPricing.component.css'],
    providers: [SafariTourServices]
})
@Injectable()
export class HotelPricingComponent implements OnInit, OnChanges, AfterViewInit, AfterViewChecked {
    @Input("hotelPricing")
    public hotelPricing: IHotelPricing | any;
    @Output() valueChange = new EventEmitter();

    private safariTourService: SafariTourServices | any;

    @ViewChild('hotelPricingItem') hotelPricingItem: HTMLElement | any;

    public constructor(safariTourService: SafariTourServices) {

        this.safariTourService = safariTourService;
    }
    public valueChanged(): void {
        this.valueChange.emit(this.hotelPricing);
    }
    public updateHotelPricing() {
        let hotPrc: IHotelPricing = {
            description: this.hotelPricing.description,
            name: this.hotelPricing.name,
            model: this.hotelPricing.model,
            price: this.hotelPricing.price,
            hotelPricingId: this.hotelPricing.hotelPricingId
        }
        this.hotelPricing = hotPrc;
        this.valueChanged();
        if (this.hotelPricing.model.editable) {
            this.safariTourService.UpdateHotelPricing(hotPrc).subscribe((data: any) => {
                alert("Hotel Pricing Updated: " + data.result);
            }).subscribe();
        }
    }
    public CreateHotelPricing(): void {
        let hotPrc: IHotelPricing = {
            description: this.hotelPricing.description,
            name: this.hotelPricing.name,
            model: this.hotelPricing.model,
            price: this.hotelPricing.price,
            hotelPricingId: this.hotelPricing.hotelPricingId
        }
        this.hotelPricing = hotPrc;
       
            this.safariTourService.PostCreateHotelPricing(hotPrc).subscribe((data: any) => {
                alert("Hotel Pricing Created: "+data.result);
            }).subscribe();
    }

    public UpdateHotelPricing(): void {
        let hotPrc: IHotelPricing = {
            description: this.hotelPricing.description,
            name: this.hotelPricing.name,
            model: this.hotelPricing.model,
            price: this.hotelPricing.price,
            hotelPricingId: this.hotelPricing.hotelPricingId
        }
        this.hotelPricing = hotPrc;
        this.hotelPricing.model.editable = true;
        this.hotelPricing.model.viewable = false;
        this.valueChange.emit(this.hotelPricing);
            this.safariTourService.UpdateHotelPricing(hotPrc).subscribe((data: any) => {
                alert('hotelPricing updated: '+data.result);
            }).subscribe();
    }
    public SelectHotelPricing(): void {

        let strValue:any = $('select#name').val();
        let hotelPricingId: number = parseInt(strValue);

         this.safariTourService.GetHotelPricingById(hotelPricingId).map((data: IHotelPricing) => {
             this.hotelPricing = data;
             this.hotelPricing.model = {};
             this.hotelPricing.model.editable = true;
             this.hotelPricing.model.viewable = false;
             this.valueChange.emit(this.hotelPricing);
            }).subscribe();
    }
    ngAfterViewChecked(): void {
        console.log("Hotel Pricing component: this.hotelPricing.model.editable: ", this.hotelPricing.model.editable)
    }
    ngAfterViewInit() {
        console.log("inside After View Init, " + this.hotelPricingItem);
    }
    ngOnChanges() {
        console.log("inside OnChanges hotelPricing: " + this.hotelPricing.model.editable);
    }
    ngOnInit() {
        //console.log("inside OnInit");
        let temp: IHotelPricing = {
            price: 0.00,
            description: "",
            name: "",
            hotelPricingId: 0,
            model: {}
        };
        this.hotelPricing = temp;
        this.hotelPricing.model.editable = true;
        this.hotelPricing.model.viewable = false;
        this.hotelPricing.model.disableUpdateOrCreateButton = false;

        let actualResult: Observable<IHotelPricing[]> = this.safariTourService.GetHotelPricing();
        actualResult.map((p: IHotelPricing[]) => {
            let items: IHotelPricing[];

            let div = this.hotelPricingItem;

            let select = div.nativeElement.querySelector("select");
            
            console.log(select);
            try {
                items = p
            }
            catch (ex) {
                items = [];
            }
            if (items && items.length > 0) {
                this.hotelPricing = items[0];
            } else {
                let defaultItem: IHotelPricing = {
                    price: 0.00,
                    description: "",
                    name: "",
                    hotelPricingId: 0,
                    model: {}
                };
                this.hotelPricing = defaultItem;
                this.hotelPricing.model.editable = false;
                this.hotelPricing.model.viewable = true;

                items = [];
            }
            $(select).remove('option');
            $(select).append('<option value="-1" selected="true">Select An Item Type</option>');
            for (let i = 0; i < items.length; i++) {
                $(select).append('<option value="' + items[i].hotelPricingId + '">' + items[i].name + '</option>');
            }
        }).subscribe();

        
    }
}