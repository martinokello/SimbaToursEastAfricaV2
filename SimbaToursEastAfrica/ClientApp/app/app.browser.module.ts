import { NgModule } from '@angular/core';
import { AppModule } from './app.module';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from '../app.component';


@NgModule({
    imports: [
        AppModule,
        BrowserModule
    ],
    providers: [
        { provide: 'BASE_URL', useFactory: getBaseUrl }
    ],
    bootstrap: [AppComponent]
})
export class AppClientModule {
}

export function getBaseUrl() {
    return document.getElementsByTagName('base')[0].href;
}