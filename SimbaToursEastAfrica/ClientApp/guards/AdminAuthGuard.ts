import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { SafariTourServices, IUserStatus } from '../services/safariTourServices';
import { map } from 'rxjs/operator/map';

@Injectable()
export class AdminAuthGuard implements CanActivate {
    private safariTourServices: SafariTourServices;
    // Inject Router so we can hand off the user to the Login Page 
    constructor(private router: Router, safariTourServices: SafariTourServices) {
        this.safariTourServices = safariTourServices;
    }

    canActivate(): boolean {

        // Token from the LogIn is avaiable, so the user can pass to the route
        let actUserStatus: IUserStatus = JSON.parse(localStorage.getItem("actUserStatus"));


        return actUserStatus.isUserAdministrator;
    }
}