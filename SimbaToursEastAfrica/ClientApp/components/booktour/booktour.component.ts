import { Component, OnInit, ViewChild, ElementRef, EventEmitter } from '@angular/core';
import { NgForm } from "@angular/forms";
import { Element } from '@angular/compiler';
import { SafariTourServices, IHotelBooking, IHotel, IMeal, ILaguage, ILocation, IHotelPricing, IAddress, IVehicle, IItem, ItemType, IInvoice } from '../../services/safariTourServices';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
@Component({
    selector: 'booktour',
    templateUrl: './booktour.component.html',
    styleUrls: ['./booktour.component.css'],
    providers: [SafariTourServices]
})
export class BookTourComponent implements OnInit {
    vehicles: IVehicle[] = [];
    hotelBookings: IHotelBooking[] = [];
    mealItems: IItem[] = [];
    items: IItem[] = [];
    currentPayment: number = 0.00;
    combinedMeals: any = {
        mealId: 0,
        mealItems: this.mealItems,
        tourClientId:0
    };
   combinedLaguage: any = {
        laguageId: 0,
        items: this.items,
        tourClientId:0
    };
    private safariTourService: SafariTourServices | any;
    public hotelBooking: IHotelBooking | any;
    public hotel: IHotel|any;
    public constructor(safarTourService: SafariTourServices) {
        this.safariTourService = safarTourService;
    }
    ngOnInit(): void {
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
            model: { }
        };

        let tmpHotel: IHotel = {
            hotelId : 0,
            hotelName:"",
            address: addTemp,
            hotelPricing: tmpHotelPricing,
            hasMealsIncluded: false,
            location: tempLoc,
            hotelPricingId: 0,
            locationId: 0
        }
        if(!this.hotel)
            this.hotel = tmpHotel;
        this.vehicles = [];
    }
    public updateHotelBooking(hotelBooking:IHotelBooking) {
        this.hotelBooking = hotelBooking;
    }
    
    public updateHotel(hotel: IHotel) {
        this.hotel = hotel;
        $('input[type="submit"]#submitTourBooking').removeClass('disabled');
    }
    public updateMeal(meal: IMeal) {
        let invoice: IInvoice = {
            grossTotalCost: 0,
            invoicedItems: [],
            invoiceId: 0,
            invoiceName: "Base Meal Invoice",
            netCost: 0,
            percentTaxAppliable:0
        }
        let mealItem: IItem = {
            itemType: ItemType.Meal,
            itemId: 0,
            invoice: invoice,
            itemName: "Base Meal",
            quantity: meal.quantity,
            itemTypeId: 0,
            InvoiceId: 0,
            laguagePricingId:0
        }
        this.combinedMeals.mealItems.push(mealItem);
    }

    public updateLaguage(laguage: ILaguage) {
        let invoice: IInvoice = {
            grossTotalCost: 0,
            invoicedItems: [],
            invoiceId: 0,
            invoiceName: "Laguage Item Invoice",
            netCost: 0,
            percentTaxAppliable: 0
        }
        let laguageItem: IItem = {
            itemType: laguage.itemType,
            itemId: 0,
            invoice: invoice,
            itemName: "Laguage Items",
            quantity: laguage.quantity,
            itemTypeId: 0,
            InvoiceId: 0,
            laguagePricingId:0
        }
        this.combinedLaguage.items.push(laguageItem);
    }
    public updateLocaton(location: ILocation) {
        this.hotel.location = location;
        this.hotel.location.address = this.hotel.address;
        this.hotelBooking.location = location;
    }
    public updateVehicle(vehicle: IVehicle) {
        this.vehicles.push(vehicle);
    }
    public bookTour(): void {
        this.hotelBookings.push(this.hotelBooking);
        this.safariTourService.AddItemsToTourClient(this.hotel,this.combinedMeals, this.combinedLaguage, this.vehicles, this.currentPayment)


        let actualRes: Observable<any> = this.safariTourService.BookTour();
        actualRes.subscribe((q: any) => {

            console.log('Response received');
            console.log(q);
            SafariTourServices.tourClientModel.grossTotalCosts = 0;
            SafariTourServices.tourClientModel.combinedLaguage = {};
            SafariTourServices.tourClientModel.combinedMeals = {};
            SafariTourServices.tourClientModel = {};
            alert('Tour Booked successfully: ' + q.result + ', Message: ' + q.message);
            console.log(q.stackTrace);
        });
    }
}