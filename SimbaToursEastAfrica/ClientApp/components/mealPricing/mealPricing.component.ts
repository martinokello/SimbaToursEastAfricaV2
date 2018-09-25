import { Component, OnInit, ViewChild, ElementRef, Injectable, AfterViewInit, AfterViewChecked, Inject, Input, Output, EventEmitter } from '@angular/core';
import { ILaguage, SafariTourServices, IHotelBooking, ItemType, IMealPricing, IDealsPricing } from '../../services/safariTourServices';
import * as $ from "jquery";
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
@Component({
    selector: 'mealPricing',
    templateUrl: './mealPricing.component.html',
    styleUrls: ['./mealPricing.component.css'],
    providers: [SafariTourServices]
})
@Injectable()
export class MealPricingComponent implements OnInit, AfterViewInit, AfterViewChecked {

    public meal: IMealPricing | any;
    private safariTourService: SafariTourServices | any;
    model: any;
    @ViewChild('mealPricingItem') mealPricingItem: HTMLElement | any;

    public constructor(safariTourService: SafariTourServices) {

        this.safariTourService = safariTourService;
    }
    public createMealPricing() {
        this.safariTourService.PostCreateMealPricing(this.meal).subscribe((data: any) => {
            alert("Meal Pricing Set: "+data.result);
        });
    }
    public updateMealPricing(): void {
        this.safariTourService.UpdateMealPricing(this.meal).subscribe((data: any) => {
            alert("Meal Pricing Updated: "+data.result);
        });
    }
    public selectMealPricing() {
        let results: Observable<IMealPricing> = this.safariTourService.GetLaguagePricingById(this.meal.mealPricingId);
        results.map((q: IMealPricing) => {
            this.meal = q;
        }).subscribe();
    }
    ngAfterViewChecked() {

        console.log("inside After View Checked");

    }
    ngAfterViewInit() {
        console.log("inside After View Init, " + this.mealPricingItem);
    }
    ngOnInit() {
        //console.log("inside OnInit");
        this.model = {};
        this.model.editable = true;
        this.model.viewable = false;
        let defaultMeal: IMealPricing = {
            price: 0.00,
            mealName: "",
            mealDescription: "",
            mealPricingId: 0
        };
        this.meal = defaultMeal;

        let items: IMealPricing[];
        let actualResult: Observable<IMealPricing[]> = this.safariTourService.GetMealsPricing();
        actualResult.map((p: IMealPricing[]) => {
            let div = this.mealPricingItem;
            let select = div.nativeElement.querySelector("select");
            console.log(select);
            try {
                items = p;
            }
            catch (ex) {
                items = [];
            }
            if (items && items.length > 0) {
                this.meal = items[0];
            }
            else {
                let defaultMeal: IMealPricing = {
                    price: 0.00,
                    mealName: "",
                    mealDescription: "",
                    mealPricingId: 0
                };
                this.meal = defaultMeal;
                items = [];
            }
            $(select).remove('option');
            $(select).append('<option value="-1" selected="true">Select An Item Type</option>');
            for (let i = 0; i < items.length; i++) {
                $(select).append('<option value="' + items[i].mealPricingId + '">' + items[i].mealName + '</option>');
            }
            alert('Location Added: ' + p);
        }).subscribe();
        
    }
}