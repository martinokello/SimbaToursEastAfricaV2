import { Component, OnInit, ViewChild, ElementRef, Input, Injectable, EventEmitter, Output } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import {SafariTourServices, ITourClient }  from '../../services/safariTourServices';
import { Element } from '@angular/compiler';
import * as $ from 'jquery';
import 'rxjs/add/operator/map';
@Component({
    selector: 'payments',
    templateUrl: './installments.component.html',
    styleUrls: ['./installments.component.css'],
    providers: [SafariTourServices]
})
@Injectable()
export class PayByInstallments implements OnInit {
    private safariTourService: SafariTourServices | any;
    public currentPayment: number | any;
    public amountLeftToPay: number | any;
    public tourClient: any;
    public constructor(safariTourService: SafariTourServices) {
        this.safariTourService = safariTourService;
    }
    public makePayment(): void {
        let result: Observable<boolean> = this.safariTourService.MakePayment(this.currentPayment, SafariTourServices.clientEmailAddress);

            result.map((q: boolean) => {
                alert("Payment made. Currently being processed by paypal service");
            }).subscribe();
    }
  
    public ngOnInit(): void {
        this.tourClient = {};
        this.currentPayment = 0.00;

        let result: Observable<ITourClient> = this.safariTourService.GetTourClientByEmail(SafariTourServices.clientEmailAddress);

        result.map((resp: ITourClient) => {
            this.tourClient = resp;
            this.amountLeftToPay = Math.round(this.tourClient.grossTotalCosts - this.tourClient.paidInstallments).toFixed(2);
        }).subscribe();
    }
}