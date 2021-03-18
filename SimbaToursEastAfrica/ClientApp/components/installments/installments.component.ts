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
    @ViewChild("paymentsForm") paymentsForm: HTMLElement | any;
    public currentPayment: number | any;
    public amountLeftToPay: number | any;
    public tourClient: any;
    public constructor(safariTourService: SafariTourServices) {
        this.safariTourService = safariTourService;
    }
    public makePayment(): void {
        let result: Observable<any>;
        if (this.currentPayment > 0 && this.currentPayment <= this.amountLeftToPay) {
            result = this.safariTourService.MakePayment(this.currentPayment, SafariTourServices.clientEmailAddress);
        }
        else {
            alert('Payment should be greater than 0.00 and not more than the amount to be paid!');
            return;
        }

        result.map((q: any) => {
            window.open(q.payPalRedirectUrl);
            console.log('Response received');
            console.log(q);
            alert("Payment made. Currently being processed by paypal service");
        }).subscribe();
    }
  
    public ngOnInit(): void {
        this.tourClient = {};
        this.currentPayment = 0.00;

        let result: Observable<ITourClient> = this.safariTourService.GetTourClientByEmail(SafariTourServices.clientEmailAddress);

        result.map((resp: ITourClient) => {
            this.tourClient = resp;
            var balance = this.tourClient.grossTotalCosts - this.tourClient.paidInstallments;
            this.amountLeftToPay = balance.toFixed(2);
        }).subscribe();
    }
}