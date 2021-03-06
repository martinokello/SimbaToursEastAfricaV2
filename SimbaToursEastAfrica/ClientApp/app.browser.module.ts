﻿import { NgModule } from '@angular/core';
import { AppModule } from './app.module';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserXhr } from '@angular/http';
import { CustExtBrowserXhr } from './cust-ext-browser-xhr';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { AuthGuard } from './guards/AuthGuard';
import { SafariTourServices } from './services/safariTourServices';
import { AdminAuthGuard } from './guards/AdminAuthGuard';


@NgModule({
    imports: [
        AppModule,
        BrowserModule,
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule
    ],
    providers: [SafariTourServices, AuthGuard, AdminAuthGuard,
        { provide: BrowserXhr, useClass: CustExtBrowserXhr },
        { provide: 'BASE_URL', useFactory: getBaseUrl }
    ],
    bootstrap: [AppComponent]
})
export class AppClientModule {
}

export function getBaseUrl() {
    
    let appRoot: string = "/SimbaSafariToursV2";
    return appRoot+document.getElementsByTagName('base')[0].href;
}