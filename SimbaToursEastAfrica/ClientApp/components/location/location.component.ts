import { Component, OnInit, ViewChild, ElementRef, Input, Output, Injectable, Inject, EventEmitter, OnChanges } from '@angular/core';
import { IAddress, ILocation, SafariTourServices, IHotelBooking, IHotel, IHotelPricing, ILaguage, IMeal, IItem, ItemType } from '../../services/safariTourServices';
import { Element } from '@angular/compiler';
import * as $ from 'jquery';
import 'rxjs/add/operator/map';
@Component({
    selector: 'tempLocation',
    templateUrl: './location.component.html',
    styleUrls: ['./location.component.css'],
    providers: [SafariTourServices]
})
@Injectable()
export class LocationComponent implements OnInit, OnChanges {
    private safariTourService: SafariTourServices | any;

    @ViewChild('locationView') public locationView: HTMLElement | any;

    public constructor(safarTourService: SafariTourServices) {
        this.safariTourService = safarTourService;
    }
    @Input()
    public hotelBooking: IHotelBooking | any;
    public locations: ILocation[] | any;

    @Input("hotel") hotel: IHotel | any;
    @Output() valueChangeLocation = new EventEmitter<IHotel>();
    public getHotelLocation() {
        this.valueLocationChanged();
    }
    public valueLocationChanged(): void {
        this.valueChangeLocation.emit(this.hotel);
    }
    public selectLocationChanged() {

        let locationIdstr: any = $('select#locationId').val();

        let selectedLocationId:number = parseInt(locationIdstr);

        let location: ILocation = this.locations.find(function (val: ILocation) {
            return val.locationId === selectedLocationId;
        });
        this.hotel.address = location.address;
        this.hotel.location = location;
        this.valueLocationChanged()
    }
    public addLocation(): void {
        /*
        let location1: ILocation = {
            address: this.hotel.location.address,
            locationName: this.hotel.location.locationName,
            locationId: this.hotel.location.locationId,
            addressId: this.hotel.location.address.addressId
        };
        this.hotel.location = location1;
        */
        this.valueChangeLocation.emit(this.hotel);
        $('div#vehicleDetails').css('display', 'block').slideDown();
    }
    ngOnChanges() {
    }
    public ngOnInit(): void {
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
        let tmpHotelPricing: IHotelPricing = {
            hotelPricingId: 0,
            description: "",
            name: "",
            price: 0.0,
            model: {}
        };

        let tmpHotel: IHotel = {
            hotelId: 0,
            hotelName: "",
            address: addTemp,
            hotelPricing: tmpHotelPricing,
            hasMealsIncluded: false,
            location: tempLoc,
            hotelPricingId: 0,
            locationId: 0
        }

        this.hotel = tmpHotel;
        this.safariTourService.GetHotelLocations().map((q: ILocation[]) => {
            this.locations = q;
            let div = this.locationView;
            let select = div.nativeElement.querySelector("select#locationId");

            $(select).remove('option');
            $(select).append('<option value="-1" selected="true">Select A Location</option>');
            for (let i = 0; i < this.locations.length; i++) {
                $(select).append('<option value="' + this.locations[i].locationId + '">' + this.locations[i].locationName + '</option>');
            }
        }).subscribe();
    }
}