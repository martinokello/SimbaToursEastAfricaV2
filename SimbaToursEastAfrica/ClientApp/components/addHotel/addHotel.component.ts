import { Component, OnInit, AfterViewChecked, ViewChild, ElementRef, Injectable, Output, EventEmitter, OnChanges } from '@angular/core';
import { LocationComponent } from '../location/location.component';
import { BrowserModule } from '@angular/platform-browser';
import { HotelPricingComponent } from '../hotelPricing/hotelPricing.component';
import { Element } from '@angular/compiler';
import { IHotel, SafariTourServices, ILocation, IHotelPricing, IAddress } from '../../services/safariTourServices';
import * as $ from 'jquery';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

@Component({
    selector: 'addHotel',
    templateUrl: './addHotel.component.html',
    styleUrls: ['./addHotel.component.css'],
    providers: [SafariTourServices]
})
@Injectable()
export class AddHotelComponent implements OnInit, AfterViewChecked, OnChanges  {

    @ViewChild('hotelItems') hotelItems: HTMLElement | any;
    private hotelDetails: IHotel[] | any;
    private safariTourService: SafariTourServices|any;
    public model: any;
    public hotel: IHotel | any;
    public theLocation: ILocation | any;
    public hotelPricing: IHotelPricing | any;
    public hotelBeforeRefUpdates: IHotel | any;
    
    public constructor(safariTourService: SafariTourServices) {
        this.safariTourService = safariTourService;
    }
    public addHotelPricing(hotelPricing: IHotelPricing): void {
        this.hotelPricing = hotelPricing;
        this.hotel.hotelPricing = hotelPricing;
        this.hotel.hotelPricingId = hotelPricing.hotelPricingId;
    }
    public makeEditable(): void{
        if (this.hotelPricing.hotelPricingId < 1)
        this.hotelPricing = {
            price: 0.00,
            description: "",
            name: "",
            hotelPricingId:0,
            model: {}
        };

        this.hotelPricing.model.editable = true;
        this.hotelPricing.model.viewable = false;
    }
    public addLocation(hotel:any): void {
        this.theLocation = hotel.location;
        this.theLocation.address = hotel.location.address;
        this.theLocation.locationId = hotel.location.locationId;
        console.log(this.theLocation );
    }
    public updateHotel() {
        this.hotel.location = this.theLocation;
        this.hotel.hotelPricing = this.hotelPricing;
        this.hotel.locationId = this.theLocation.locationId;
        this.hotel.addressId = this.theLocation.addressId;
        this.hotel.hotelPricingId = this.hotelPricing.hotelPricingId;
        this.hotel.address = this.theLocation.address;
        this.hotel.addressId = this.theLocation.addressId;

        let actualResult: Observable<boolean> = this.safariTourService.UpdateHotel(this.hotel);
        actualResult.map((p: any) => {

            alert('Hotel Updated: ' + p.result);
        }).subscribe();
    }
    public createHotel(): void {
        this.hotel.location = this.theLocation;
        this.hotel.hotelPricing = this.hotelPricing;
        this.hotel.locationId = this.theLocation.locationId;
        this.hotel.addressId = this.theLocation.addressId;
        this.hotel.hotelPricingId = this.hotelPricing.hotelPricingId;
        this.hotel.address = this.theLocation.address;
        this.hotel.addressId = this.theLocation.addressId;
         
        let actualResult: Observable<any> = this.safariTourService.PostCreateHotel(this.hotel);
        actualResult.map((p: any) => {

            alert('Hotel Added: ' + p.result);
        }).subscribe();

    }

    public selectHotel(): void {
        let hotelIdstr: any = $('select#hotelId').val();

        let selectedHotelId = parseInt(hotelIdstr);
        this.updateHotelReferenceHotelId(selectedHotelId);
    }
    public updateHotelReferenceHotelId(selectedHotelId:number) {
        let actualResult: Observable<any> = this.safariTourService.GetHotelLocationByHotelId(selectedHotelId);
        actualResult.map((p: ILocation) => {
            this.hotel.locatonId = p.locationId;
            this.hotel.address = p.address;
            this.theLocation.address = p.address;
            this.theLocation.addressId = p.address.addressId;
            this.theLocation.locationId = p.locationId;
            this.theLocation.locationName = p.locationName;
            this.hotel.location = this.theLocation;
        }).subscribe();

    }
    ngOnChanges(): void {

        console.log('location: ' + this.theLocation);
        console.log('hotelPricing: ' + this.hotelPricing);
    }
    ngAfterViewChecked(): void {
        console.log("view AddHotel: this.hotelPricing.model.editable: ", this.hotelPricing.model.editable)
    }
    public ngOnInit(): void {
        let tempHotel: IHotel = {
            hotelId: 0,
            hotelName: "",
            hasMealsIncluded: false,
            location: {},
            hotelPricing: {},
            address: {},
            hotelPricingId: 0,
            locationId: 0
        }
        let addTemp: IAddress = {
            addressLine1: "",
            addressLine2: "",
            addressId: 0,
            postCode: "",
            town: "",
            country: ""
        }
        let tempLoc: ILocation = {
            address: addTemp,
            locationName: "",
            locationId: 0,
            addressId: 0
        };
        tempHotel.address= addTemp;
        tempHotel.location = tempLoc;
        let model: any = {};

        model.editable = true;
        model.viewable = false;
        this.hotelPricing = {
            price: 0.00,
            description: "",
            name: "",
            hotelPricingId: 0,
            model: model
        };


        tempHotel.hotelPricing = this.hotelPricing;
        this.theLocation = tempLoc;

        this.hotel = tempHotel;

        let actualRes: Observable<IHotel[]> = this.safariTourService.GetAllHotelDetails();

        actualRes.subscribe((p: IHotel[]) => {
            let results: IHotel[] = p;
            this.hotelDetails = results;
            this.hotel = this.hotelDetails.length == 0 ? tempHotel : this.hotelDetails[0];
            this.updateHotelReferenceHotelId(this.hotel.hotelId);
            let div = this.hotelItems;
            let select = div.nativeElement.querySelector("select#hotelId");
            console.log(select);

            $(select).remove('option');
            if (this.hotelDetails.length > 0) {
                $(select).append('<option value="-1" selected="true">Select A Hotel</option>');

                for (let i = 0; i < this.hotelDetails.length; i++) {
                    $(select).append('<option value="' + this.hotelDetails[i].hotelId + '">' + this.hotelDetails[i].hotelName + '</option>');
                }
            }
        });

        $('input[type="button"]#selectHotel').css('display', 'inline-block');
        $('input[type="submit"]#createHotel').css('display', 'inline-block');
        $('input[type="submit"]#updateHotel').css('display', 'inline-block');
        $('input[type="submit"]#makeEditable').css('display', 'block');
        $('input[type="submit"]#addLocation').css('display', 'inline-block'); 
        $('div#locationView').removeClass('col-lg-12');
        $('div#hotelPricingItemView').removeClass('col-lg-12');

    }

    public updateLocationAndPricingReferences(): void {
        //get hotel pricing and location:
        let pricings: Observable<any> = this.safariTourService.GetHotelPricingById(this.hotelBeforeRefUpdates.hotelPricingId);
        let locations: Observable<any> = this.safariTourService.GetHotelLocationById(this.hotelBeforeRefUpdates.locationId);
        let addresses: Observable<any>;

        pricings.map((p: IHotelPricing) => {
            this.hotelPricing = p;
            this.hotelPricing.model.editable = true;
            this.hotelPricing.model.viewable = false;
            this.hotelBeforeRefUpdates.hotelPricing = this.hotelPricing;
            locations.subscribe((p: ILocation) => {
                this.hotelBeforeRefUpdates.location = p;

                this.theLocation = this.hotelBeforeRefUpdates.location;
                addresses = this.safariTourService.GetHotelAddressById(this.hotelBeforeRefUpdates.location.addressId);

                addresses.subscribe((p: IAddress) => {

                    this.hotelBeforeRefUpdates.address  = p;
                    this.theLocation.address = this.hotelBeforeRefUpdates.address;
                    this.hotel = this.hotelBeforeRefUpdates;
                });
            });

        }).subscribe();

    }
}