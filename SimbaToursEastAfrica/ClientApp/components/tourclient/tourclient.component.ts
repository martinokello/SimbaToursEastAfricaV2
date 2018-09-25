import { Component, OnInit, ViewChild, ElementRef,Injectable } from '@angular/core';
import { ITourClient, SafariTourServices } from '../../services/safariTourServices';
import { Element } from '@angular/compiler';
import * as $ from 'jquery';
import 'rxjs/add/operator/map';
@Component({
    selector: 'tourclient',
    templateUrl: './tourclient.component.html',
    styleUrls: ['./tourclient.component.css'],
    providers: [SafariTourServices]
})
@Injectable()
export class TourClientComponent implements OnInit  {
    public tourClient: ITourClient | any
    private safariTourService: SafariTourServices | any;

    public constructor(safarTourService: SafariTourServices) {
        this.safariTourService = safarTourService;
    }
    public AddTourClient(): void {
        this.tourClient = this.tourClient as ITourClient;
        this.safariTourService.AddTourClient(this.tourClient);

        $('div#hotelDetails').css('display', 'block').slideDown();
    }
    public ngOnInit(): void {
        this.tourClient = {};
    }
}