import { Component, OnInit, ViewChild, ElementRef, Injectable, Output, Input,EventEmitter } from '@angular/core';
import { Element } from '@angular/compiler';
import { IHotelBooking, SafariTourServices, IHotel, IAddress, ILocation, IHotelPricing, ILaguage, IMeal, IItem, ItemType, ILaguagePricing} from '../../services/safariTourServices';
import * as $ from 'jquery';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

@Component({
    selector: 'hotelBooking',
    templateUrl: './hotelBooking.component.html',
    styleUrls: ['./hotelBooking.component.css'],
    providers: [SafariTourServices]
})
@Injectable()
export class HotelBookingComponent implements OnInit {
    private safariTourService: SafariTourServices | any;
    private hotelDetails: IHotel[]|any;
    @Input()
    public hotel: IHotel | any;
    @Output() valueChangeHotel = new EventEmitter<IHotel>();
    @Input()
    public hotelBooking: IHotelBooking | any;
    @Output() valueChangeHotelBooking = new EventEmitter<IHotelBooking>();

    @ViewChild('hotelbookingView') hotelbookingView: HTMLElement | any;
    
    public constructor(safariTourService: SafariTourServices) {
        this.safariTourService = safariTourService;
    }
    public captureHotelBooking(): void {
        this.valueChangeHotel.emit(this.hotel);
        this.valueChangeHotelBooking.emit(this.hotelBooking);
        console.log(this.hotel);
        $('div#locationDetails').css('display', 'block').slideDown();
    }
    public hotelBookingHasChanged() {

        let hotelIdstr:any = $("select#hotelId").val();
        let selectId: number = parseInt(hotelIdstr);

        let hotel = this.hotelDetails.find(function(val:IHotel){
            return val.hotelId === selectId;
        });
        this.hotel = hotel;
        this.updateLocationAndPricingReferences();
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
            addressId:0
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
        let tempHotel: IHotel = {
            hotelId: 0,
            hotelName: "",
            hotelPricingId: 0,
            hotelPricing: {},
            address: {},
            hasMealsIncluded: false,
            location: {},
            locationId: 0
        };
        let laguagePricing: ILaguagePricing = {
            laguageDescription: "",
            laguagePricingId: 0,
            laguagePricingName: "",
            unitLaguagePrice: 0.00,
            unitMedicalPrice: 0.00,
            unitTravelDocumentPrice: 0.00,
            unitMealPrice:0.00
        }
        let laguages: ILaguage[] = [];
        let meals: IMeal[] = [];
        let items: IItem[] = [];
        let laguage: ILaguage = {
            items: [],
            itemType: ItemType.Laguage,
            laguageId: 0,
            quantity: 0,
            laguagePricing: laguagePricing,
            laguagePricingId:0
        };
        let tempHotelBooking: IHotelBooking = {
            hotelName: "",
            location: tempLoc,
            address: addTemp,
            laguages: laguages,
            meals: meals,
            combinedLaguage: laguage,
            accomodatonCost: 0.00,
            grossTotalCost: 0.00,
            hasMealsIncluded: false,
            hotelBookingId: 0,
            invoicedItems: items,
            invoiceId: 0,
            invoiceName: "",
            netCost: 0.00,
            percentTaxApplicable: 0,
            tourClient: SafariTourServices.tourClientModel
        }
        if(!this.hotel)
            this.hotel = tempHotel;
        if(!this.hotelBooking)
        this.hotelBooking = tempHotelBooking;

        let actualRes: Observable<IHotel[]> = this.safariTourService.GetAllHotelDetails();

        actualRes.subscribe((p: IHotel[]) => {
            let results: IHotel[] = p;
            this.hotelDetails = results;
            if (this.hotelDetails.length > 0) {
                this.hotel = this.hotelDetails[0];
                this.updateLocationAndPricingReferences();
                let div = this.hotelbookingView;
                let select = div.nativeElement.querySelector("select#hotelId");
                console.log(select);

                $(select).remove('option');
                $(select).append('<option value="-1" selected="true">Select A Hotel</option>');
                for (let i = 0; i < this.hotelDetails.length; i++) {
                    $(select).append('<option value="' + this.hotelDetails[i].hotelId + '">' + this.hotelDetails[i].hotelName + '</option>');
                }
            }
            else {
                    this.hotel = tempHotel;
                    this.hotelBooking = tempHotelBooking;

            }
        });
    }

    public updateLocationAndPricingReferences():void {
        //get hotel pricing and location:
        let pricings: Observable<any> = this.safariTourService.GetHotelPricingById(this.hotel.hotelPricingId);
        let locations: Observable<any> = this.safariTourService.GetHotelLocationById(this.hotel.locationId); 
        let addresses: Observable<any>;

        pricings.subscribe((data: any) => {
            let p: IHotelPricing = data;
            this.hotel.hotelPricing = p;
            this.hotelBooking.accomodatonCost = this.hotel.hotelPricing.price;
            this.valueChangeHotel.emit(this.hotel);
            this.valueChangeHotelBooking.emit(this.hotelBooking);

        });


        locations.subscribe((data:any) => {
            let p: ILocation = data;
            this.hotel.location = p;
            this.hotelBooking.hotelName = this.hotel.hotelName;
            this.hotelBooking.location = this.hotel.location;

            this.valueChangeHotel.emit(this.hotel);
            this.valueChangeHotelBooking.emit(this.hotelBooking);
            addresses = this.safariTourService.GetHotelAddressById(this.hotel.location.addressId);

            addresses.subscribe((resp: any) => {
                let p: IAddress = data;
                this.hotel.address = p;
                this.hotelBooking.location.address = this.hotel.address;
                this.valueChangeHotel.emit(this.hotel);
                this.valueChangeHotelBooking.emit(this.hotelBooking);
            });
        });

    }
}