import { Component, OnInit, Input,ViewChild, ElementRef, EventEmitter } from '@angular/core';
import { NgForm } from "@angular/forms";
import { Element } from '@angular/compiler';
import { SafariTourServices, ITourClient, IHotelBooking, IMealPricing, ILaguagePricing, ITransportPricing, IHotel, IMeal, ILaguage, ILocation, IHotelPricing, IAddress, IVehicle, IItem, ItemType, IInvoice, VehicleType, IExtraCharges } from '../../services/safariTourServices';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import * as $ from 'jquery';
@Component({
    selector: 'booktour',
    templateUrl: './booktour.component.html',
    styleUrls: ['./booktour.component.css'],
    providers: [SafariTourServices]
})
export class BookTourComponent implements OnInit {
    private isHotelSet: boolean = false;
    @ViewChild("f4") paymentsForm: HTMLElement | any;
    runningCost: number = 0.00;
    vehicles: IVehicle[] = [];
    hotelBookings: IHotelBooking[] = [];
    mealItems: IItem[] = [];
    items: IItem[] = [];
    extraCharges: IExtraCharges[] = [];
    extraPayments: number = 0.00;
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

    transportPricing: ITransportPricing | any;
    hotelPrincing: IHotelPricing | any;
    mealPricing: IMealPricing | any;
    laguageAccessoriesPricing: ILaguagePricing | any;

    private safariTourService: SafariTourServices | any;
    public hotelBooking: IHotelBooking | any;
    public hotel: IHotel|any;
    public constructor(safarTourService: SafariTourServices) {
        this.safariTourService = safarTourService;
    }
    ngOnInit(): void {
        window.onbeforeunload = function () {
            localStorage.removeItem("extraCharges"); return '';
        }
        if (localStorage.getItem('extraCharges')) {
            this.extraCharges = JSON.parse(localStorage.getItem('extraCharges'));
            for (let i = 0; i < this.extraCharges.length; i++) {
                this.extraPayments += this.extraCharges[i].extraCharges;
            }
            this.runningCost += this.extraPayments;
        }
        let transportPrincingRes: Observable<ITransportPricing[]> = this.safariTourService.GetTransportPricing();

        transportPrincingRes.map((resp: ITransportPricing[]) => {
            this.transportPricing = resp[0]
        }).subscribe();
        let mealPrincingRes: Observable<IMealPricing[]> = this.safariTourService.GetMealsPricing();

        mealPrincingRes.map((resp: IMealPricing[]) => {
            this.mealPricing = resp[0]
        }).subscribe();

        let laguageAccessoriesPricingRes: Observable<ILaguagePricing[]> = this.safariTourService.GetLaguagePricing();

        laguageAccessoriesPricingRes.map((resp: ILaguagePricing[]) => {
            this.laguageAccessoriesPricing = resp[0]
        }).subscribe();
        
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

    public hotelSet(isSet: boolean) {
        if (!this.isHotelSet && this.runningCost === this.extraPayments) {
            let currentTotal = (this.hotel.hotelPricing.price * SafariTourServices.tourClientModel.numberOfIndividuals);
            this.runningCost = this.runningCost + currentTotal;
            this.isHotelSet = true;
        }
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
        let currentTotal = (mealItem.quantity * this.mealPricing.price);

        this.runningCost += currentTotal;
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
        let currentTotal: number = 0;
        let strLaguageType:string = laguageItem.itemType + "";
        switch (parseInt(strLaguageType)) {
            case ItemType.Laguage:
                currentTotal= (laguageItem.quantity * this.laguageAccessoriesPricing.unitLaguagePrice);
                break;
            case ItemType.Meal:
                currentTotal= (laguageItem.quantity * this.mealPricing.price);
                break;
            case ItemType.MedicalTreatment:
                currentTotal= (laguageItem.quantity * this.laguageAccessoriesPricing.unitMedicalPrice);
                break;
            case ItemType.TravelDocuments:
                currentTotal= (laguageItem.quantity * this.laguageAccessoriesPricing.unitTravelDocumentPrice);
                break;
        }
        let runningCostStr: string = this.runningCost + currentTotal + "";
        this.runningCost = parseFloat(parseFloat(runningCostStr).toFixed(2));
    }
    public updateLocaton(location: ILocation) {
        this.hotel.location = location;
        this.hotel.location.address = this.hotel.address;
        this.hotelBooking.location = location;
    }
    public updateVehicle(vehicle: IVehicle) {
        this.vehicles.push(vehicle);
        let currentTotal: number = 0;
        let vehType: string = vehicle.vehicleType + "";
        switch (parseInt(vehType)) {
            case VehicleType.Taxi:
                currentTotal= (vehicle.actualNumberOfPassengersAllocated * this.transportPricing.taxiPricing);
                break;
            case VehicleType.MiniBus:
                currentTotal= (vehicle.actualNumberOfPassengersAllocated * this.transportPricing.miniBusPricing);
                break;
            case VehicleType.PickUpTrack:
                currentTotal= (vehicle.actualNumberOfPassengersAllocated * this.transportPricing.pickupTruckPricing);
                break;
            case VehicleType.FourWheelDriveCar:
                currentTotal= (vehicle.actualNumberOfPassengersAllocated * this.transportPricing.fourByFourPricing);
                break;
            case VehicleType.TourBus:
                currentTotal= (vehicle.actualNumberOfPassengersAllocated * this.transportPricing.tourBusPricing);
                break;
        }
        let runningCostStr: string = this.runningCost + currentTotal + "";
        this.runningCost = parseFloat(parseFloat(runningCostStr).toFixed(2));
    }
    public bookTour(): void {
        this.hotelBookings.push(this.hotelBooking);

        if (localStorage.getItem('extraCharges')) {
            this.extraCharges = JSON.parse(localStorage.getItem('extraCharges'));
        }
        this.safariTourService.AddItemsToTourClient(this.hotel, this.combinedMeals, this.combinedLaguage, this.vehicles, this.extraCharges, this.currentPayment)

        let actualRes: Observable<any> = this.safariTourService.BookTour();
        actualRes.subscribe((q: any) => {
            $('iframe#payPalPayments').attr('src', q.payPalRedirectUrl);
            $('iframe#payPalPayments').show("slow");
            //this.safariTourService.GetRequest(q.payPalRedirectUrl);
            //$(this.paymentsForm.nativeElement).attr("action", q.payPalRedirectUrl);
            //$(this.paymentsForm.nativeElement).submit();
            console.log('Response received');
            console.log(q);
            
            localStorage.setItem('extraCharges', null);
            let model: ITourClient = {
                tourClientId: 0,
                mealId: 0,
                laguageId: 0,
                clientFirstName: "",
                clientLastName: "",
                nationality: "",
                hasRequiredVisaStatus: true,
                numberOfIndividuals: 0,
                vehicles: null,
                hotelBookings: null,
                costPerIndividual: 0,
                hotel: null,
                emailAddress: "",
                hasFullyPaid: false,
                paidInstallments: 0,
                currentPayment: 0,
                grossTotalCosts: 0,
                dateCreated: new Date(),
                dateUpdated: new Date(),
                combinedLaguage: null,
                combinedMeals: null,
                extraCharges: null
            };
            SafariTourServices.tourClientModel = model;
            alert('Tour Booked successfully: ' + q.result + ', Message: ' + q.message);
            console.log(q.stackTrace);
        });
        
    }
}