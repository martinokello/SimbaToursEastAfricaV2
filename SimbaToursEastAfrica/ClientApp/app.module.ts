import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { BookTourComponent } from './components/booktour/booktour.component';
import { CounterComponent } from './components/counter/counter.component';
import { TourClientComponent } from './components/tourclient/tourclient.component';
import { HotelBookingComponent } from './components/hotelbooking/hotelbooking.component';
import { MealComponent } from './components/meal/meal.component';
import { LocationComponent } from './components/location/location.component';
import { LaguageComponent } from './components/laguage/laguage.component';
import { VehicleComponent } from './components/vehicle/vehicle.component';
import { MealPricingComponent } from './components/mealPricing/mealPricing.component';
import { LaguagePricingComponent } from './components/laguagePricing/laguagePricing.component';
import { HotelPricingComponent } from './components/hotelPricing/hotelPricing.component';
import { DealsPricingComponent } from './components/dealsPricing/dealsPricing.component';
import { SchedulesPricingComponent } from './components/schedulesPricing/schedulesPricing.component';
import { AddLocationComponent } from './components/addLocation/addLocation.component';
import { AddVehicleComponent } from './components/addVehicle/addVehicle.component';
import { AddHotelComponent } from './components/addHotel/addHotel.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { ForgotPasswordComponent } from './components/forgotPassword/forgotPassword.component';
import { PayByInstallments } from './components/installments/installments.component';
import { TransportPricingComponent } from './components/transportPricing/transportPricing.component';
import { UserRolesComponent } from './components/userroles/userroles.component';
import { ContactUsComponent } from './components/contactus/contactus.component';
import { AboutUsComponent } from './components/about/aboutus.component'; 

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        FetchDataComponent,
        CounterComponent,
        BookTourComponent,
        TourClientComponent,
        HotelBookingComponent,
        MealComponent,
        LocationComponent,
        LaguageComponent,
        VehicleComponent,
        MealPricingComponent,
        LaguagePricingComponent,
        HotelPricingComponent,
        DealsPricingComponent,
        SchedulesPricingComponent,
        AddVehicleComponent,
        AddHotelComponent,
        AddLocationComponent,
        LoginComponent,
        ForgotPasswordComponent,
        RegisterComponent,
        ForgotPasswordComponent,
        PayByInstallments,
        TransportPricingComponent,
        UserRolesComponent,
        ContactUsComponent,
        AboutUsComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: 'book-tour', component: BookTourComponent },
            { path: 'deal-pricing', component: DealsPricingComponent },
            { path: 'hotel-pricing', component: HotelPricingComponent },
            { path: 'laguage-pricing', component: LaguagePricingComponent },
            { path: 'meal-pricing', component: MealPricingComponent },
            { path: 'schedules-pricing', component: SchedulesPricingComponent },
            { path: 'add-hotel', component: AddHotelComponent },
            { path: 'add-location', component: AddLocationComponent },
            { path: 'add-vehicle', component: AddVehicleComponent },
            { path: 'login', component: LoginComponent },
            { path: 'register', component: RegisterComponent },
            { path: 'forgot-password', component: ForgotPasswordComponent },
            { path: 'login', component: LoginComponent },
            { path: 'make-Payments', component: PayByInstallments },
            { path: 'transport-pricing', component: TransportPricingComponent },
            { path: 'logout', component: HomeComponent },
            { path: 'manage-roles', component: UserRolesComponent },
            { path: 'contactus', component: ContactUsComponent },
            { path: 'aboutus', component: AboutUsComponent }, 


            { path: '**', redirectTo: 'home' }
        ])
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
