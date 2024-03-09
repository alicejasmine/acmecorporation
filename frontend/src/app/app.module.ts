import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DrawLandingPageComponent } from './draw-landing-page/draw-landing-page.component';
import { ReactiveFormsModule} from '@angular/forms';
import {HttpClientModule} from "@angular/common/http";
import { FormSubmissionsComponent } from './form-submissions/form-submissions.component';



@NgModule({
  declarations: [
    AppComponent,
    DrawLandingPageComponent,
    FormSubmissionsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
