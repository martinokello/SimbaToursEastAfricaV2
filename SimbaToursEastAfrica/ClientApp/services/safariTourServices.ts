import { Http, Request, RequestOptions, RequestMethod, RequestOptionsArgs, Headers, Response } from '@angular/http';
import 'rxjs/add/operator/map';
import { Subscription } from 'rxjs/Subscription';
import { Injectable, Inject, ReflectiveInjector } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Binary } from '@angular/compiler';

@Injectable()
export class SafariTourServices {
    public static actUserStatus: IUserStatus = {
        isUserLoggedIn: false,
        isUserAdministrator: false
    };
    public static isLoginPage: boolean = false;
    public actionResult: any;
    public httpClient: Http;
    public getAllRoles: string = "/SimbaSafariToursV2/Account/GetAllRoles";
    public postRmoveUserFromRole: string = "/SimbaSafariToursV2/Account/RemoveUserFromRole";
    public postAddUserToRole: string = "/SimbaSafariToursV2/Account/AddUserToRole";
    public twitterFeedsUrl: string = "/SimbaSafariToursV2/Home/TwitterProfileFeeds";
    public createTransportPricingUrl: string = "/SimbaSafariToursV2/api/Administration/CreateTransportPricing";
    public updateTransportPricingUrl: string = "/SimbaSafariToursV2/api/Administration/UpdateTransportPricing"
    public getTransportPricingById: string = "/SimbaSafariToursV2/Home/GetTransportPricingById";
    public postCurrentPaymentUrl: string = "/SimbaSafariToursV2/Home/MakePayment";
    public getTourClientUrl: string = "/SimbaSafariToursV2/Home/GetTourClientByEmail";
    public bookTourUrl: string = "/SimbaSafariToursV2/Home/BookTour";
    public getHotelDetailsUrl: string = "/SimbaSafariToursV2/Home/GetHotelDetails";
    public getAllHotelDetailsUrl: string = "/SimbaSafariToursV2/Home/GetAllHotelDetails";
    public hotelPricingUrl: string = "/SimbaSafariToursV2/Home/GetHotelPricing";
    public schedulesPricingsUrl: string = "/SimbaSafariToursV2/Home/GetSchedulesPricing";
    public dealsPricingsUrl: string = "/SimbaSafariToursV2/Home/GetDealsPricing";
    public transportPricingsUrl: string = "/SimbaSafariToursV2/Home/GetTransportPricing";
    public laguagePricingUrl: string = "/SimbaSafariToursV2/Home/GetLaguagePricing";
    public mealPricingUrl: string = "/SimbaSafariToursV2/Home/GetMealPricing";
    public postOrUpdateLocationUrl: string = "/SimbaSafariToursV2/api/Administration/PostLocation";
    public updateLocationUrl: string = "/SimbaSafariToursV2/api/Administration/UpdateLocation";
    public postOrUpdateVehicleUrl: string = "/SimbaSafariToursV2/api/Administration/PostVehicle";
    public updateVehicleUrl: string = "/SimbaSafariToursV2/api/Administration/UpdateVehicle";
    public postOrUpdateDealsPricingUrl: string = "/SimbaSafariToursV2/api/Administration/PostDealPricing";
    public updateDealsPricingUrl: string = "/SimbaSafariToursV2/api/Administration/UpdateDealPricing";
    public postOrUpdateLaguagePricingUrl: string = "/SimbaSafariToursV2/api/Administration/PostLaguagePricing";
    public updateLaguagePricingUrl: string = "/SimbaSafariToursV2/api/Administration/UpdateLaguagePricing";
    public updateMealPricingUrl: string = "/SimbaSafariToursV2/api/Administration/UpdateMealPricing";
    public postCreateMealPricingUrl: string = "/SimbaSafariToursV2/api/Administration/PostMealPricing";
    public postOrCreateSchedulesPricingUrl: string = "/SimbaSafariToursV2/api/Administration/PostSchedulesPricing";
    public updateSchedulesPricingUrl: string = "/SimbaSafariToursV2/api/Administration/UpdateSchedulesPricing";
    public postCreateHotelPricingUrl: string = "/SimbaSafariToursV2/api/Administration/PostHotelPricing";
    public postUpdateHotelPricingUrl: string = "/SimbaSafariToursV2/api/Administration/UpdateHotelPricing";
    public updateHotelPricingUrl: string = "/SimbaSafariToursV2/api/Administration/UpdateHotelPricing";
    public postCreateHotelUrl: string = "/SimbaSafariToursV2/api/Administration/PostHotel";
    public updateHotelUrl: string = "/SimbaSafariToursV2/api/Administration/UpdateHotel";
    public getHotelPricingById: string = "/SimbaSafariToursV2/api/Administration/GetHotelPricingById";
    public getHotelLocationByHotelIdUrl: string = "/SimbaSafariToursV2/Home/GetHotelLocationByHotelId"
    public getHotelLocationById: string = "/SimbaSafariToursV2/api/Administration/GetLocationByHotelId";
    public getHotelLocationsUrl: string = "/SimbaSafariToursV2/Home/GetHotelLocations";
    public getHotelAddressById: string = "/SimbaSafariToursV2/api/Administration/GetHotelAddressById";
    public getLaguagePricingById: string = "/SimbaSafariToursV2/api/Administration/GetLaguagePricingById";
    public getMealPricingById: string = "/SimbaSafariToursV2/api/Administration/GetMealPricingById";
    public getDealsPricingById: string = "/SimbaSafariToursV2/api/Administration/GetDealsPricingById";
    public postLoginUrl: string = "/SimbaSafariToursV2/Account/Login";
    public getVerifyLoggedInUser: string = "/SimbaSafariToursV2/Account/VerifyLoggedInUser";
    public getLogoutUrl: string = "/SimbaSafariToursV2/Account/Logout";
    public postRegisterUrl: string = "/SimbaSafariToursV2/Account/Register";
    public postForgotPasswordUrl: string = "/SimbaSafariToursV2/Account/ForgotPassword";
    public static tourClientModel: ITourClient;
    public static clientEmailAddress: string = "";
    public postSendEmail: string = " /SimbaSafariToursV2/Home/SendEmail";

    public constructor(httpClient: Http) {
        this.httpClient = httpClient;
        let model: ITourClient =  {
            tourClientId: 0,
            mealId: 0,
            laguageId:0,
            clientFirstName : "",
            clientLastName : "",
            nationality : "",
            hasRequiredVisaStatus : true,
            numberOfIndividuals : 0,
            vehicles : null,
            hotelBookings: null,
            costPerIndividual : 0,
            hotel : null,
            emailAddress : "",
            hasFullyPaid : false,
            paidInstallments: 0,
            currentPayment: 0,
            grossTotalCosts: 0,
            dateCreated: new Date(),
            dateUpdated: new Date(),
            combinedLaguage: null,
            combinedMeals: null,
            extraCharges:null
        };
        SafariTourServices.tourClientModel = model;
        SafariTourServices.tourClientModel.grossTotalCosts = 0;
    }
    public static SetUserEmail(userEmailAddress: string) {
        SafariTourServices.clientEmailAddress = userEmailAddress;
    } 
    public GetAllRoles(): Observable<any> {
        let headers = new Headers({ 'Content-Type': 'application/x-www-form-urlencoded' });
        let requestUrl = this.getAllRoles;
        let requestoptions: RequestOptions = new RequestOptions({
            url: requestUrl,
            method: RequestMethod.Get,
            headers: headers
        });

        return this.httpClient.request(new Request(requestoptions)).map((resp: Response) => {
            return resp.json();
        });
    }
    public AddUserToRole(email:string, role:string): Observable<any>{
        let body = JSON.stringify({ email: email,role:role});

        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });

        let requestoptions: RequestOptions = new RequestOptions({
            url: this.postAddUserToRole,
            method: RequestMethod.Post,
            headers: headers,
            body: body
        });

        return this.httpClient.request(new Request(requestoptions)).map((res: Response) => {
            console.log('Response received ' + res.json());
            return res.json();
        });
    }
    public SendEmail(body: FormData): Observable<any>
    {
        let headers = new Headers({'Content-Type': 'multipart/form-data' });
        return this.httpClient.post(this.postSendEmail, body, new RequestOptions({ headers: headers})).map((res: Response) => {
            console.log('Response received ' + res.json());
            return res.json();
        });
    }

    public RemoveUserFromRole(email: string, role: string): Observable<any> {
        let body = JSON.stringify({ email: email, role: role });

        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });

        let requestoptions: RequestOptions = new RequestOptions({
            url: this.postRmoveUserFromRole,
            method: RequestMethod.Post,
            headers: headers,
            body: body
        });

        return this.httpClient.request(new Request(requestoptions)).map((res: Response) => {
            console.log('Response received ' + res.json());
            return res.json();
        });
    }
    public BookTour(): any {
        try {
            SafariTourServices.tourClientModel.grossTotalCosts = SafariTourServices.tourClientModel.hotel.hotelPricing.price * SafariTourServices.tourClientModel.numberOfIndividuals;

            let body = JSON.stringify(SafariTourServices.tourClientModel);

            let headers = new Headers({ 'Content-Type': 'application/json', 'Access-Control-Allow-Origin': '*' });

            let requestoptions: RequestOptions = new RequestOptions({
                url: this.bookTourUrl,
                method: RequestMethod.Post,
                headers: headers,
                body: body
            });


            return this.httpClient.request(new Request(requestoptions)).map((resp: Response) => {
                return resp.json();
            });
        }
        catch (e) {
            console.log(SafariTourServices.tourClientModel);
            alert("failed to book tour" + e);
            return null;
        }
    }

    public GetTwitterFeeds(): Observable<any[]> {
        
        let headers = new Headers({ 'Content-Type': 'application/x-www-form-urlencoded' });
        let requestUrl = this.twitterFeedsUrl;
        let requestoptions: RequestOptions = new RequestOptions({
            url: requestUrl,
            method: RequestMethod.Get,
            headers: headers
        });

        return this.httpClient.request(new Request(requestoptions)).map((resp: Response) => {
            return resp.json();
        });
    }

    public VerifyLoggedInUser(): any {
        let headers = new Headers({ 'Content-Type': 'application/x-www-form-urlencoded' });
        let requestUrl = this.getVerifyLoggedInUser;
        let requestoptions: RequestOptions = new RequestOptions({
            url: requestUrl,
            method: RequestMethod.Get,
            headers: headers
        });

        return this.httpClient.request(new Request(requestoptions)).map((resp: Response) => {
            return resp.json();
        });
        
    }
    public LoginByPost(userDetail: IUserDetail): Observable<any> {
        let body = JSON.stringify(userDetail);

        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });

        let requestoptions: RequestOptions = new RequestOptions({
            url: this.postLoginUrl,
            method: RequestMethod.Post,
            headers: headers,
            body: body
        });

        return this.httpClient.request(new Request(requestoptions)).map((res: Response) => {
            console.log('Response received ' + res.json());
            return res.json();
        });
    }


    public LogOut(): Observable<any> {

        return this.httpClient.get(this.getLogoutUrl).map((res: Response) => {
            console.log('Response received ' + res.json());
            return res.json();
        });
    }
    public GetRequest(url): Observable<any> {

        return this.httpClient.get(url).map((res: Response) => {
            console.log('Response received ' + res.json());
            return res.json();
        });
    }
    public GetHotelDetails(hotelId: number): Observable<IHotel> {

        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });
        let requestUrl = this.getHotelDetailsUrl + "?hotelId=" + hotelId;
        let requestoptions: RequestOptions = new RequestOptions({
            url: requestUrl,
            method: RequestMethod.Get,
            headers: headers
        });

        return this.httpClient.request(new Request(requestoptions)).map((res: Response) => {
            return res.json().results.map((p: IHotel) => {
                let q: IHotel = {
                    address: p.address,
                    hasMealsIncluded: p.hasMealsIncluded,
                    hotelId: p.hotelId,
                    hotelName: p.hotelName,
                    hotelPricing: p.hotelPricing,
                    hotelPricingId: p.hotelPricingId,
                    location: p.location,
                    locationId: p.locationId
                };
                return this.actionResult = q;
            });
        });
    }
    public GetHotelPricingById(hotelPricingId: number): Observable<any> {

        let headers = new Headers({ 'Content-Type': 'application/x-www-form-urlencoded' });
        let requestUrl = this.getHotelPricingById + "?hotelPricingId=" + hotelPricingId;
        let requestoptions: RequestOptions = new RequestOptions({
            url: requestUrl,
            method: RequestMethod.Get,
            headers: headers
        });

        return this.httpClient.request(new Request(requestoptions)).map((resp: Response) => {
            return resp.json();
        });
    }
    public GetMealPricingById(mealPricingId: number): Observable<any> {

        let headers = new Headers({ 'Content-Type': 'application/x-www-form-urlencoded' });
        let requestUrl = this.GetMealPricingById + "?mealPricingId=" + mealPricingId;
        let requestoptions: RequestOptions = new RequestOptions({
            url: requestUrl,
            method: RequestMethod.Get,
            headers: headers
        });

        return this.httpClient.request(new Request(requestoptions)).map((resp: Response) => {
            return resp.json();
        });
    }

    public GetLaguagePricingById(laguagePricingId: number): Observable<any> {

        let headers = new Headers({ 'Content-Type': 'application/x-www-form-urlencoded' });
        let requestUrl = this.getLaguagePricingById + "?laguagePricingId=" + laguagePricingId;
        let requestoptions: RequestOptions = new RequestOptions({
            url: requestUrl,
            method: RequestMethod.Get,
            headers: headers
        });

        return this.httpClient.request(new Request(requestoptions)).map((resp: Response) => {
            return resp.json();
        });
    }

    public GetSchedulesPricingById(schedulesPricingId: number): Observable<any> {

        let headers = new Headers({ 'Content-Type': 'application/x-www-form-urlencoded' });
        let requestUrl = this.schedulesPricingsUrl + "?schedulesPricingId=" + schedulesPricingId;
        let requestoptions: RequestOptions = new RequestOptions({
            url: requestUrl,
            method: RequestMethod.Get,
            headers: headers
        });

        return this.httpClient.request(new Request(requestoptions)).map((resp: Response) => {
            return resp.json();
        });
    }
    public GetDealsPricingById(dealPricingId: number): Observable<any> {

        let headers = new Headers({ 'Content-Type': 'application/x-www-form-urlencoded' });
        let requestUrl = this.dealsPricingsUrl + "?dealPricingId=" + dealPricingId;
        let requestoptions: RequestOptions = new RequestOptions({
            url: requestUrl,
            method: RequestMethod.Get,
            headers: headers
        });

        return this.httpClient.request(new Request(requestoptions)).map((resp: Response) => {
            return resp.json();
        });
    } 
    public GetHotelLocationByHotelId(hotelId: number): Observable<any> {

        let headers = new Headers({ 'Content-Type': 'application/x-www-form-urlencoded' });
        let requestUrl = this.getHotelLocationByHotelIdUrl + "?hotelId=" + hotelId;
        let requestoptions: RequestOptions = new RequestOptions({
            url: requestUrl,
            method: RequestMethod.Get,
            headers: headers
        });

        return this.httpClient.request(new Request(requestoptions)).map((resp: Response) => {
            return resp.json();
        });
    } 
    public GetHotelLocations(): Observable<any> {

        let headers = new Headers({ 'Content-Type': 'application/x-www-form-urlencoded' });
        let requestUrl = this.getHotelLocationsUrl;
        let requestoptions: RequestOptions = new RequestOptions({
            url: requestUrl,
            method: RequestMethod.Get,
            headers: headers
        });

        return this.httpClient.request(new Request(requestoptions)).map((resp: Response) => {
            return resp.json();
        });
    }
    public GetHotelLocationById(locationId: number): Observable<any> {

        let headers = new Headers({ 'Content-Type': 'application/x-www-form-urlencoded' });
        let requestUrl = this.getHotelLocationById + "?locationId=" + locationId;
        let requestoptions: RequestOptions = new RequestOptions({
            url: requestUrl,
            method: RequestMethod.Get,
            headers: headers
        });

        return this.httpClient.request(new Request(requestoptions)).map((resp: Response) => {
            return resp.json();
        });
    }

    public GetHotelAddressById(addressId: number): Observable<any> {

        let headers = new Headers({ 'Content-Type': 'application/x-www-form-urlencoded' });
        let requestUrl = this.getHotelAddressById + "?addressId=" + addressId;
        let requestoptions: RequestOptions = new RequestOptions({
            url: requestUrl,
            method: RequestMethod.Get,
            headers: headers
        });

        return this.httpClient.request(new Request(requestoptions)).map((resp: Response) => {
            return resp.json();
        });
    }
    public GetAllHotelDetails(): Observable<IHotel[]> {

        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });
        let requestUrl = this.getAllHotelDetailsUrl;
        let requestoptions: RequestOptions = new RequestOptions({
            url: requestUrl,
            method: RequestMethod.Get,
            headers: headers
        });

        return this.httpClient.request(new Request(requestoptions)).map((res: Response) => {
            return res.json();
        });
    }
    public registerByPost(userDetail: IUserDetail): Observable<any> {
        let body = JSON.stringify(userDetail);

        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });

        let requestoptions: RequestOptions = new RequestOptions({
            url: this.postRegisterUrl,
            method: RequestMethod.Post,
            headers: headers,
            body: body
        });

        return this.httpClient.request(new Request(requestoptions)).map((res: Response) => {
            return res.json();
        });
    }
    public forgotPasswordByPost(userDetail: IUserDetail): Observable<any> {
        let body = JSON.stringify(userDetail);
        var actionResult: any;

        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });

        let requestoptions: RequestOptions = new RequestOptions({
            url: this.postForgotPasswordUrl,
            method: RequestMethod.Post,
            headers: headers,
            body: body
        });
        return this.httpClient.request(new Request(requestoptions)).map((res: Response) => {
            return res.json();
        });
    }
    public PostOrUpdateLocation(location: ILocation): Observable<any> {
        let body = JSON.stringify(location);
        var actionResult: any;

        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });

        let requestoptions: RequestOptions = new RequestOptions({
            url: this.postOrUpdateLocationUrl,
            method: RequestMethod.Post,
            headers: headers,
            body: body
        });
        return this.httpClient.request(new Request(requestoptions)).map((res: Response) => {
            return res.json();
        });
    }

    public UpdateLocation(location: ILocation): Observable<any> {
        let body = JSON.stringify(location);
        var actionResult: any;

        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });

        let requestoptions: RequestOptions = new RequestOptions({
            url: this.updateLocationUrl,
            method: RequestMethod.Post,
            headers: headers,
            body: body
        });
        return this.httpClient.request(new Request(requestoptions)).map((res: Response) => {
            return res.json();
        });
    }
    public PostOrUpdateVehicle(vehicle: IVehicle): Observable<any> {
        let body = JSON.stringify(vehicle);
        var actionResult: any;

        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });

        let requestoptions: RequestOptions = new RequestOptions({
            url: this.postOrUpdateVehicleUrl,
            method: RequestMethod.Post,
            headers: headers,
            body: body
        });

        return this.httpClient.request(new Request(requestoptions)).map((res: Response) => {
            return res.json();
        });
    }

    public UpdateVehicle(vehicle: IVehicle): Observable<any> {
        let body = JSON.stringify(vehicle);
        var actionResult: any;

        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });

        let requestoptions: RequestOptions = new RequestOptions({
            url: this.updateVehicleUrl,
            method: RequestMethod.Post,
            headers: headers,
            body: body
        });

        return this.httpClient.request(new Request(requestoptions)).map((res: Response) => {
            return res.json();
        });
    }
    public PostCreateMealPricing(mealPricing: IMealPricing): Observable<any> {

        let body = JSON.stringify(mealPricing);
        var actionResult: any;

        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });

        let requestoptions: RequestOptions = new RequestOptions({
            url: this.postCreateMealPricingUrl,
            method: RequestMethod.Post,
            headers: headers,
            body: body
        });

        return this.httpClient.request(new Request(requestoptions)).map((res: Response) => {
            return res.json();
        });
    }

    public UpdateMealPricing(mealPricing: IMealPricing): Observable<any> {

        let body = JSON.stringify(mealPricing);
        var actionResult: any;

        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });

        let requestoptions: RequestOptions = new RequestOptions({
            url: this.updateMealPricingUrl,
            method: RequestMethod.Post,
            headers: headers,
            body: body
        });

        return this.httpClient.request(new Request(requestoptions)).map((res: Response) => {
            return res.json();
        });
    }
    public UpdateSchedulesPricing(schedulesPricing: ISchedulesPricing): Observable<any> {

        let body = JSON.stringify(schedulesPricing);
        var actionResult: any;

        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });

        let requestoptions: RequestOptions = new RequestOptions({
            url: this.updateSchedulesPricingUrl,
            method: RequestMethod.Post,
            headers: headers,
            body: body
        });
        return this.httpClient.request(new Request(requestoptions)).map((res: Response) => {
            return res.json();
        });
    }

    public PostOrCreateSchedulesPricing(schedulesPricing: ISchedulesPricing): Observable<any> {

        let body = JSON.stringify(schedulesPricing);
        var actionResult: any;

        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });

        let requestoptions: RequestOptions = new RequestOptions({
            url: this.postOrCreateSchedulesPricingUrl,
            method: RequestMethod.Post,
            headers: headers,
            body: body
        });
        return this.httpClient.request(new Request(requestoptions)).map((res: Response) => {
            return res.json();
        });
    }
    public PostOrUpdateLaguagePricing(laguagePricing: ILaguagePricing): Observable<any> {

        let body = JSON.stringify(laguagePricing);
        var actionResult: any;

        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });

        let requestoptions: RequestOptions = new RequestOptions({
            url: this.postOrUpdateLaguagePricingUrl,
            method: RequestMethod.Post,
            headers: headers,
            body: body
        });
        return this.httpClient.request(new Request(requestoptions)).map((res: Response) => {
            return res.json();
        });
    }
    public UpdateLaguagePricing(laguagePricing: ILaguagePricing): Observable<any> {

        let body = JSON.stringify(laguagePricing);
        var actionResult: any;

        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });

        let requestoptions: RequestOptions = new RequestOptions({
            url: this.updateLaguagePricingUrl,
            method: RequestMethod.Post,
            headers: headers,
            body: body
        });
        return this.httpClient.request(new Request(requestoptions)).map((res: Response) => {
            return res.json();
        });
    }
    public CreateTransportPricing(transportPricing: ITransportPricing) {

        let body = JSON.stringify(transportPricing);
        let actionResult: any;

        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });

        let requestoptions: RequestOptions = new RequestOptions({
            url: this.createTransportPricingUrl,
            method: RequestMethod.Post,
            headers: headers,
            body: body
        });
        return this.httpClient.request(new Request(requestoptions)).map((res: Response) => {
            return res.json();
        });
    }

    public UpdateTransportPricing(transportPricing: ITransportPricing) {

        let body = JSON.stringify(transportPricing);
        let actionResult: any;

        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });

        let requestoptions: RequestOptions = new RequestOptions({
            url: this.updateTransportPricingUrl,
            method: RequestMethod.Post,
            headers: headers,
            body: body
        });
        return this.httpClient.request(new Request(requestoptions)).map((res: Response) => {
            return res.json();
        });
    }

    public GetTransportPricingById(transportPricingId: number): Observable<ISchedulesPricing[]> {

        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });
        let requestUrl = this.getTransportPricingById+"transportPricingId="+transportPricingId;
        let requestoptions: RequestOptions = new RequestOptions({
            url: requestUrl,
            method: RequestMethod.Get,
            headers: headers
        });

        return this.httpClient.request(new Request(requestoptions)).map((res: Response) => {
            return res.json();
        });
    }

    public GetTransportPricing(): Observable<ITransportPricing[]> {

        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });
        let requestUrl = this.transportPricingsUrl;
        let requestoptions: RequestOptions = new RequestOptions({
            url: requestUrl,
            method: RequestMethod.Get,
            headers: headers
        });

        return this.httpClient.request(new Request(requestoptions)).map((res: Response) => {
            return res.json();
        });
    }
    public PostCreateHotelPricing(hotelPricing: IHotelPricing): Observable<any> {


        let body = JSON.stringify(hotelPricing);
        let actionResult: any;

        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });

        let requestoptions: RequestOptions = new RequestOptions({
            url: this.postCreateHotelPricingUrl,
            method: RequestMethod.Post,
            headers: headers,
            body: body
        });
        return this.httpClient.request(new Request(requestoptions)).map((res: Response) => {
            return res.json();
        });
    }

    public UpdateHotelPricing(hotelPricing: IHotelPricing): Observable<any> {


        let body = JSON.stringify(hotelPricing);
        let actionResult: any;

        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });

        let requestoptions: RequestOptions = new RequestOptions({
            url: this.updateHotelPricingUrl,
            method: RequestMethod.Post,
            headers: headers,
            body: body
        });
        return this.httpClient.request(new Request(requestoptions)).map((res: Response) => {
            return res.json();
        });
    }
    public PostOrUpdateDealsPricing(dealsPricing: IDealsPricing): Observable<any> {

        let body = JSON.stringify(dealsPricing);
        var actionResult: any;

        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });

        let requestoptions: RequestOptions = new RequestOptions({
            url: this.postOrUpdateDealsPricingUrl,
            method: RequestMethod.Post,
            headers: headers,
            body: body
        });
        return this.httpClient.request(new Request(requestoptions)).map((res: Response) => {
            return res.json();
        });

    }
    public UpdateDealsPricing(dealsPricing: IDealsPricing): Observable<any> {

        let body = JSON.stringify(dealsPricing);
        var actionResult: any;

        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });

        let requestoptions: RequestOptions = new RequestOptions({
            url: this.updateDealsPricingUrl,
            method: RequestMethod.Post,
            headers: headers,
            body: body
        });
        return this.httpClient.request(new Request(requestoptions)).map((res: Response) => {
            return res.json();
        });

    }
    public MakePayment(currentPayment: number, emailAddress: string): Observable<any> {
        let load = { currentPayment: currentPayment, emailAddress: emailAddress };
        let body = JSON.stringify(load);

        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });

        let requestoptions: RequestOptions = new RequestOptions({
            url: this.postCurrentPaymentUrl,
            method: RequestMethod.Post,
            headers: headers,
            body: body
        });
        return this.httpClient.request(new Request(requestoptions)).map((res: Response) => {
            return res.json();
            });
    }
    public GetTourClientByEmail(emailAddress: string): Observable<ITourClient> {
        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });
        let requestUrl = this.getTourClientUrl + "?emailAddress=" + emailAddress;
        let requestoptions: RequestOptions = new RequestOptions({
            url: requestUrl,
            method: RequestMethod.Get,
            headers: headers
        });

        return this.httpClient.request(new Request(requestoptions)).map((res: Response) => {
            return res.json();
        });
    }
    public GetDealsPricing(): Observable<IDealsPricing[]> {
        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });
        let requestUrl = this.dealsPricingsUrl;
        let requestoptions: RequestOptions = new RequestOptions({
            url: requestUrl,
            method: RequestMethod.Get,
            headers: headers
        });

        return this.httpClient.request(new Request(requestoptions)).map((res: Response) => {
            return res.json();
        });
    }

    public GetLaguagePricing(): Observable<ILaguagePricing[]> {
        
        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });
        let requestUrl = this.laguagePricingUrl;
        let requestoptions: RequestOptions = new RequestOptions({
            url: requestUrl,
            method: RequestMethod.Get,
            headers: headers
        });

        return this.httpClient.request(new Request(requestoptions)).map((res: Response) => {
            return res.json();
        });
    }
    public GetSchedulesPricing(): Observable<ISchedulesPricing[]> {

        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });
        let requestUrl = this.schedulesPricingsUrl;
        let requestoptions: RequestOptions = new RequestOptions({
            url: requestUrl,
            method: RequestMethod.Get,
            headers: headers
        });

        return this.httpClient.request(new Request(requestoptions)).map((res: Response) => {
            return res.json();
        });
    }

    public GetMealsPricing(): Observable<IMealPricing[]> {

        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });
        let requestUrl = this.mealPricingUrl;
        let requestoptions: RequestOptions = new RequestOptions({
            url: requestUrl,
            method: RequestMethod.Get,
            headers: headers
        });

        return this.httpClient.request(new Request(requestoptions)).map((res: Response) => {
            return res.json();
        });
    }
    public GetHotelPricing(): Observable<IHotelPricing[]> {

        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });
        let requestUrl = this.hotelPricingUrl;
        let requestoptions: RequestOptions = new RequestOptions({
            url: requestUrl,
            method: RequestMethod.Get,
            headers: headers
        });

        return this.httpClient.request(new Request(requestoptions)).map((res: Response) => {
            return res.json();
        });
    }
    public PostCreateHotel(hotel: IHotel): Observable<any> {

        let body = JSON.stringify(hotel);
        let result = false;

        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });

        let requestoptions: RequestOptions = new RequestOptions({
            url: this.postCreateHotelUrl,
            method: RequestMethod.Post,
            headers: headers,
            body: body
        });
        return this.httpClient.request(new Request(requestoptions)).map((res: Response) => {
            return res.json();
        });
    }

    public UpdateHotel(hotel: IHotel): Observable<any> {

        let body = JSON.stringify(hotel);
        let result = false;

        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });

        let requestoptions: RequestOptions = new RequestOptions({
            url: this.updateHotelUrl,
            method: RequestMethod.Post,
            headers: headers,
            body: body
        });
        return this.httpClient.request(new Request(requestoptions)).map((res: Response) => {
            return res.json();
        });
    }
    public AddTourClient(tourClient: ITourClient): void {
        SafariTourServices.tourClientModel = tourClient;

    }
    public AddItemsToTourClient(hotel: IHotel, combinedMeals: any, combinedLaguage: any, vehicles: IVehicle[], extraCharges:IExtraCharges[], currentPayment:number) {
        SafariTourServices.tourClientModel.emailAddress = SafariTourServices.clientEmailAddress;
        SafariTourServices.tourClientModel.currentPayment = currentPayment;
        SafariTourServices.tourClientModel.hotel = hotel;
        SafariTourServices.tourClientModel.vehicles = vehicles;
        SafariTourServices.tourClientModel.combinedLaguage = combinedLaguage;
        SafariTourServices.tourClientModel.combinedMeals = combinedMeals;
        SafariTourServices.tourClientModel.extraCharges = extraCharges;

        SafariTourServices.tourClientModel.dateCreated = new Date();
        SafariTourServices.tourClientModel.dateUpdated = new Date();
        SafariTourServices.tourClientModel.hasFullyPaid = false;
        SafariTourServices.tourClientModel.paidInstallments = 0.00;
        SafariTourServices.tourClientModel.costPerIndividual = hotel.hotelPricing.price;
        SafariTourServices.tourClientModel.tourClientId = 0;
        SafariTourServices.tourClientModel.mealId = 0;
        SafariTourServices.tourClientModel.laguageId = 0;
    }

    public GetItemTypeNames(): ItemType[] {

        var itemtypeValues = [];
        itemtypeValues.push(ItemType.Laguage);
        itemtypeValues.push(ItemType.Meal);
        itemtypeValues.push(ItemType.MedicalTreatment);
        itemtypeValues.push(ItemType.TravelDocuments);
        return itemtypeValues;
    }

    public GetVehicleTypeNames(): VehicleType[] {
        var vehicletypeValues = [];
        vehicletypeValues.push(VehicleType.Taxi);
        vehicletypeValues.push(VehicleType.TourBus);
        vehicletypeValues.push(VehicleType.MiniBus);
        vehicletypeValues.push(VehicleType.FourWheelDriveCar);
        vehicletypeValues.push(VehicleType.PickUpTrack);
        return vehicletypeValues;
    }
}

export interface IHotelBooking {
    hotelBookingId: number;
    hotelName: string;
    accomodatonCost: number;
    hasMealsIncluded: boolean;
    combinedLaguage: ILaguage;
    tourClient: ITourClient;
    location: ILocation;
    meals: IMeal[];
    laguages: ILaguage[],
}
export interface ILaguage {
    laguageId: number;
    items: IItem[];
    quantity: number,
    itemType: ItemType,
    laguagePricing: ILaguagePricing,
    laguagePricingId:number
}
export interface IItem {
    itemId: number;
    itemTypeId: number;
    itemName: string;
    itemType: ItemType;
    quantity: number;
    invoice: IInvoice;
    InvoiceId: number;
    laguagePricingId:number
}
export interface ILocation {
    locationId: number;
    locationName: string;
    address: IAddress;
    addressId: number;
}
export interface IAddress {
    addressId: number;
    addressLine1: string;
    addressLine2: string;
    town: string;
    postCode: string;
    country: string;
}
export interface ITourClient {
    tourClientId: number;
    mealId: number;
    laguageId: number;
    clientFirstName: string;
    clientLastName: string;
    nationality: string;
    hasRequiredVisaStatus: boolean;
    numberOfIndividuals: number;
    vehicles: IVehicle[];
    hotelBookings: IHotelBooking[];
    costPerIndividual: number;
    hotel: IHotel;
    emailAddress: string;
    hasFullyPaid: boolean;
    paidInstallments: number;
    currentPayment: number;
    grossTotalCosts: number;
    dateCreated: Date;
    dateUpdated: Date;
    combinedLaguage: any,
    combinedMeals: any,
    extraCharges: IExtraCharges[]
}

export interface IExtraCharges {
    tourClientId: number,
    tourClientExtraChargesId: number,
    extraCharges: number,
    description:string
}
export interface IMeal {

    mealId: number;
    mealItems: IItem[];
    quantity: number;
}

export interface IInvoice {
    invoiceId: number;
    invoiceName: string;
    invoicedItems: IItem[];
    netCost: number;
    percentTaxAppliable: number;
    grossTotalCost: number;
}
export interface IHotelBooking {
    invoiceId: number;
    invoiceName: string;
    invoicedItems: IItem[];
    netCost: number;
    percentTaxApplicable: number;
    grossTotalCost: number;
    address: IAddress;
}
export interface IVehicle {
    vehicleid: number;
    vehicleType: VehicleType;
    vehicleRegistration: string;
    maxNumberOfPassengers: number;
    actualNumberOfPassengersAllocated: number;
}
export interface IVehicleType {
    vehicleTypeId: number;
    vehicleType: VehicleType;
}
export enum ItemType { Laguage = 1, Meal = 2, TravelDocuments = 3, MedicalTreatment = 4 }
export enum VehicleType { Taxi = 1, MiniBus = 2, TourBus = 3, FourWheelDriveCar = 4, PickUpTrack = 5 };

export interface IMealPricing {
    mealPricingId: number;
    mealDescription: string;
    mealName: string;
    price: number;
}

export interface ITransportPricing{
    transportPricingId: number;
    transportPricingName: string;
    description: string;
    fourByFourPricing: number; 
    miniBusPricing: number;
    taxiPricing: number;
    pickupTruckPricing: number;
    tourBusPricing: number;
}

export interface ILaguagePricing {
    laguagePricingId: number;
    laguageDescription: string;
    laguagePricingName: string;
    unitLaguagePrice: number;
    unitTravelDocumentPrice: number;
    unitMedicalPrice: number;
    unitMealPrice: number;
}


export interface ISchedulesPricing {
    schedulesPricingId: number;
    schedulesDescription: string;
    schedulesPricingName: string;
    price: number;
}
export interface IDealsPricing {
    dealsPricingId: number;
    description: string;
    dealName: string;
    price: number;
}
export interface IHotelPricing {
    hotelPricingId: number;
    description: string;
    name: string;
    price: number;
    model: any
}
export interface IHotel {
    hotelId: number,
    address: IAddress | any,
    location: ILocation | any,
    hotelPricing: IHotelPricing | any,
    hasMealsIncluded: boolean,
    hotelName: string,
    hotelPricingId: number,
    locationId:number
}
export interface IUserDetail {
    emailAddress: string | any,
    name: string | any,
    password: string | any,
    keepLoggedIn: boolean,
    repassword: string,
    role: string
}
export interface IEmailMessage {
    emailFrom: string,
    emailTo: string,
    attachment: Binary,
    emailSubject:string,
    emailBody:string
}
export interface IUserStatus {

    isUserLoggedIn: boolean,
    isUserAdministrator: boolean
}

export interface IUserRole {
    name: string
}
