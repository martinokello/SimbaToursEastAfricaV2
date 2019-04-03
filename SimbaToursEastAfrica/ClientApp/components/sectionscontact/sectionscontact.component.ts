import { Component, OnInit, ViewChild, ElementRef, Injectable } from '@angular/core';
import { ILaguage, SafariTourServices, IEmailMessage } from '../../services/safariTourServices';
import * as $ from "jquery";

@Component({
    selector: 'sections-contact',
    templateUrl: './sectionscontact.component.html',
    styleUrls: ['./sectionscontact.component.css'],
    providers: [SafariTourServices]
})
@Injectable()
export class SectionsContactComponent implements OnInit {
    private safariTourService: SafariTourServices | any;
    email: IEmailMessage | any;

    @ViewChild('emailFormView') emailFormView: HTMLElement | any; 
    public constructor(safariTourService: SafariTourServices) {

        this.safariTourService = safariTourService;
    }
    sendEmail(): void {

        let formView = this.emailFormView;
        let form = formView.nativeElement.querySelector("form");
        if (form.checkValidity())
        form.submit();
    }
    ngOnInit() {
        this.email = {
            emailBody: "",
            attachment: null,
            emailSubject: "",
            emailTo: "",
            emailFrom: ""
        }
        
        $(document).ready(function () {
           
            $('input[type="text"]').focus(function () {
                $(this).val("");
            });
        });
    }
}