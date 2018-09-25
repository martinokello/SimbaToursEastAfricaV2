import { Component, OnInit, ViewChild, ElementRef, Injectable, AfterViewInit, AfterViewChecked, Inject } from '@angular/core';
import { IVehicle, SafariTourServices, IHotelBooking, VehicleType } from '../../services/safariTourServices';
import * as $ from "jquery";
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

@Component({
    selector: 'addVehicle',
    templateUrl: './addVehicle.component.html',
    styleUrls: ['./addVehicle.component.css'],
    providers: [SafariTourServices]
})
@Injectable()
export class AddVehicleComponent implements OnInit, AfterViewInit, AfterViewChecked{
    public vehicle: IVehicle | any;
    private safariTourService: SafariTourServices | any;
    @ViewChild('vehicleItem') vehicleItem: HTMLElement | any; 

    public constructor(safarTourService: SafariTourServices) {
        this.safariTourService = safarTourService;
    }
    public addVehicle(): void{
        //$('div#vehicleView').css('display', 'block').slideDown();
        let actualResult: Observable<any> = this.safariTourService.PostOrUpdateVehicle(this.vehicle);
        actualResult.map((p: any) => {

            alert('Vehicle Added: ' + p.result);
        }).subscribe();
    }
    public updateVehicle() {
        let actualResult: Observable<any> = this.safariTourService.UpdateVehicle(this.vehicle);
        actualResult.map((p: any) => {

            alert('Vehicle Added: ' + p.result);
        }).subscribe();

    }
    public ngOnInit(): void {
        this.vehicle = {};
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
    }
}