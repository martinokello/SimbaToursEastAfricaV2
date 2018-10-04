import { Component, OnInit, ViewChild, ElementRef, Injectable, AfterViewInit, AfterViewChecked, Inject } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { IVehicle, SafariTourServices, IUserDetail } from '../../services/safariTourServices';
import * as $ from "jquery";
import 'rxjs/add/operator/map';
@Component({
    selector: 'register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css'],
    providers: [SafariTourServices]
})
@Injectable()
export class RegisterComponent implements OnInit{
    public userDetail: IUserDetail | any;
    private safariTourService: SafariTourServices | any;
    ngOnInit(): void {
        let userDetail: IUserDetail = {
            password: "",
            role: "",
            emailAddress: "",
            repassword: "",
            name: "",
            keepLoggedIn: false
        };

        this.userDetail = userDetail;
    }
    public constructor(safarTourService: SafariTourServices) {
        this.safariTourService = safarTourService;
    }
    public registerUser(): void {
        let registeResults: Observable<any> =this.safariTourService.registerByPost(this.userDetail);

        registeResults.map((q: any) => {
            if (q.isRegistered) {
                alert('Registration Successfull: ' + q.isRegistered);
            }
            else {
                alert('Failed to Register user. Please contact the Site Administrator')
            }
        }).subscribe();

    }
}