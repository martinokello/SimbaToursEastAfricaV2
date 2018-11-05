import { Component, OnInit, ViewChild, ElementRef, Injectable, AfterViewInit, AfterViewChecked, Inject, Output, Input, EventEmitter } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import {SafariTourServices, IUserDetail, IUserStatus, IUserRole } from '../../services/safariTourServices';
import 'rxjs/add/operator/map';
import * as $ from 'jquery';

@Component({
    selector: 'user-roles',
    templateUrl: './userroles.component.html',
    styleUrls: ['./userroles.component.css'],
    providers: [SafariTourServices]
})
@Injectable()
export class UserRolesComponent implements OnInit{

    @ViewChild('rolesView') div: HTMLElement | any;
    public userDetail: IUserDetail | any;
    private safariTourService: SafariTourServices | any;
    private userRoles: IUserRole[] | any;
    private email: string | any;

    ngOnInit(): void {
        this.getAllRoles();
    }
    public constructor(safarTourService: SafariTourServices) {
        this.safariTourService = safarTourService;
    }

    getSelectedRole(): string {

        let select = this.div.nativeElement.querySelector("select#roleName");
        return $(select).val()+"";
    }
    public addUserToRole(): void {
        let role = this.getSelectedRole();
        let results: Observable<any> = this.safariTourService.AddUserToRole(this.email, role);
        results.map((q: boolean) => {
            if (q) {
                alert('Added user: ' + this.email + ' to role: ' + role);
            }
            else {

                alert('Failed to add user: ' + this.email + ' to role: ' + role);
            }
        }).subscribe();
    }
    public removeUserFromRole(): void {
        let role = this.getSelectedRole();
        let results: Observable<any> = this.safariTourService.RemoveUserFromRole(this.email, role);
        results.map((q: boolean) => {
            if (q) {
                alert('Removed user: ' + this.email + ' from role: ' + role);
            }
            else {

                alert('Failed to remove user: ' + this.email + ' from role: ' + role);
            }
        }).subscribe();
    }

    public getAllRoles() {
        let results: Observable<any> = this.safariTourService.GetAllRoles();
        results.map((q: IUserRole[]) => {
            this.userRoles = q;

            let select = this.div.nativeElement.querySelector("select#roleName");
            console.log(select);

            $(select).remove('option');
            $(select).append('<option value="-1" selected="true">Select A Role</option>');
            for (let i = 0; i < this.userRoles.length; i++) {
                $(select).append('<option value="' + this.userRoles[i].name + '">' + this.userRoles[i].name + '</option>');
            } 
        }).subscribe();
    }

}