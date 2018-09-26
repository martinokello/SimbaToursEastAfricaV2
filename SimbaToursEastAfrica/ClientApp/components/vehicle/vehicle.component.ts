import { Component, OnInit, ViewChild, ElementRef, Injectable, AfterViewInit, AfterViewChecked, Inject, Output, EventEmitter } from '@angular/core';
import { IVehicle, SafariTourServices, IHotelBooking, VehicleType } from '../../services/safariTourServices';
import * as $ from "jquery";
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
@Component({
    selector: 'vehicle',
    templateUrl: './vehicle.component.html',
    styleUrls: ['./vehicle.component.css'],
    providers: [SafariTourServices]
})
@Injectable()
export class VehicleComponent implements OnInit, AfterViewInit, AfterViewChecked{
    public vehicle: IVehicle | any;
    private safariTourService: SafariTourServices | any;
    @ViewChild('vehicleItem') vehicleItem: HTMLElement | any;
    @Output() valueChangeVehicle = new EventEmitter<IVehicle>();
    public constructor(safarTourService: SafariTourServices) {
        this.safariTourService = safarTourService;
    }
    public addVehicle(): void{
        this.valueChangeVehicle.emit(this.vehicle);
         $('div#mealDetails').css('display', 'block').slideDown();
        alert('Vehicle Added');
    }
    public updateVehicle() {

    }
    public ngOnInit(): void {
        let tempVeh: IVehicle = {
            vehicleid: 0,
            vehicleRegistration: "",
            actualNumberOfPassengersAllocated: 0,
            maxNumberOfPassengers:0
        };
        this.vehicle = tempVeh;
        console.log("inside OnInit");
        let div = this.vehicleItem
        console.log(div);
        
        let select = div.nativeElement.querySelector("select");
        console.log(select);
        let items = this.safariTourService.GetVehicleTypeNames();

        $(select).remove('option');
        $(select).append('<option value="-1" selected="true">Select An Item Type</option>');
       for (let i = 0; i < items.length; i++) {
           $(select).append('<option value="' + items[i].valueOf().toString() + '">' + VehicleType[items[i]] as string+'</option>');
        }
    }
    ngAfterViewInit() {

    }
    ngAfterViewChecked() {
        //console.log("inside After View Checked");
       /* debugger;
        let items = this.safariTourService.GetVehicleTypeNames();
        let select = this.divContainer.getElementsByTagName('select')[0];
        for (let i = 0; i < select.ChildNodes().length; i++) {
            select.removeChild(select.ChildNodes[i]);
        }
        let option = new HTMLOptionElement();
        option.text = "Select Vehicle Type";
        option.value = "-1";
        select.appendChild(option);

        for (let i = 0; i < items.length; i++) {
            option.text = items[i].toString();
            option.value = items[i].valueOf()).toString();
            select.appendChild(option);
        }*/
    }
}