import { NgModule } from '@angular/core';
import { AppModule } from './app.module';
import { ServerModule } from '@angular/platform-server';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
@NgModule({
    imports: [
        ServerModule,
        CommonModule,
        HttpModule,
        FormsModule,
        AppModule,
        RouterModule
    ],
    bootstrap: [AppComponent]
})
export class AppServerModule { }
