﻿import {    Component, OnInit, ViewChild, ElementRef, Injectable,Input} from '@angular/core';
import { Observable } from 'rxjs/Observable';
import {SafariTourServices, IUserRole } from '../../services/safariTourServices';
import 'rxjs/add/operator/map';
import * as $ from 'jquery';

@Component({
    selector: 'new-user-roles',
    templateUrl: './newroles.component.html',
    styleUrls: ['./newroles.component.css'],
    providers: [SafariTourServices]
})
@Injectable()
export class NewRolesComponent implements OnInit{

    @ViewChild('rolesView') div: HTMLElement | any;
    private safariTourService: SafariTourServices | any;
    public userRoles: IUserRole[] | any;
    public email: string | any;
    public newRoleName: string | any;

    ngOnInit(): void {
    }
    public constructor(safarTourService: SafariTourServices) {
        this.safariTourService = safarTourService;
    }

    public createUserRole(): void {
        let role = this.newRoleName;
        let results: Observable<any> = this.safariTourService.CreateUserRole(role);
        results.map((q: boolean) => {
            if (q) {
                alert('Role: ' + role +' Created Successfully!');
                return true;
            }
            else {

                alert('Failed to Create: ' + role + ' Role.');
                return false;
            }
        }).subscribe();
        
    }
    public deleteUserRole(): void {
        let role = this.newRoleName;
        let results: Observable<any> = this.safariTourService.DeleteUserRole(role);
        results.map((q: boolean) => {
            if (q) {
                alert('Role: ' + role + ' Deleted Successfully!');
                return true;
            }
            else {

                alert('Failed to Delete: ' + role + ' Role.');
                return false;
            }
        }).subscribe();

    }

}