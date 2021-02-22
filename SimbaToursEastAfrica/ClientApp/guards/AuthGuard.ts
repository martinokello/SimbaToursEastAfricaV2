import { Injectable } from '@angular/core';
import {CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router} from '@angular/router';
import { Observable } from 'rxjs';
import { SafariTourServices } from '../services/safariTourServices';
import { map } from 'rxjs/operator/map';

@Injectable()
export class AuthGuard implements CanActivate {
    // Inject Router so we can hand off the user to the Login Page 
    constructor(private router: Router, private safariTourService: SafariTourServices) {
    }

    canActivate(): Observable<boolean> {

        // Token from the LogIn is avaiable, so the user can pass to the route

        return this.safariTourService.appUserIsLoggedIn.map(
            (res: boolean) => {
                    if (res) {
                        return true;
                    }
                    else {
                        this.router.navigateByUrl('login');
                        return false;
                    }
                });
    }
}