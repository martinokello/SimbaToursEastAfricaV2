import { Component, OnInit, ViewChild, ElementRef, Input, Injectable, EventEmitter, Output} from '@angular/core';
import { IMeal, SafariTourServices, IHotelBooking }  from '../../services/safariTourServices';
import { Element } from '@angular/compiler';
import * as $ from 'jquery';
import 'rxjs/add/operator/map';
@Component({
    selector: 'meal',
    templateUrl: './meal.component.html',
    styleUrls: ['./meal.component.css'],
    providers: [SafariTourServices]
})
@Injectable()
export class MealComponent implements OnInit {
    private safariTourService: SafariTourServices | any;
    @Input()
    public hotelBooking: IHotelBooking | any;
    
    @Output() valueChangeMeal = new EventEmitter<IMeal>();
    public meal: IMeal|any;
    public constructor(safariTourService: SafariTourServices) {
        this.safariTourService = safariTourService;
    }
    public addMeal(): void {
        this.valueChangeMeal.emit(this.meal);

        alert('Meals added successfully');

        $('div#laguageDetails').css('display', 'block').slideDown();
        $('input[type="button"').removeClass('disabled');
    }
  
    public ngOnInit(): void {
        let tempMeal: IMeal = {
            mealId: 0,
            mealItems: [],
            quantity:0
        };
        this.meal = tempMeal;
        this.hotelBooking = {};
    }
}