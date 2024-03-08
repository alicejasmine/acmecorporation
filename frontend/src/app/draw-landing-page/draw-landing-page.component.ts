import { Component } from '@angular/core';
import {FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-draw-landing-page',
  template:  `
   `

})
export class DrawLandingPageComponent {
  formGroup=new FormGroup({
    firstName: new FormControl('', [Validators.required, Validators.maxLength(50)]),
    lastName: new FormControl('', [Validators.required, Validators.maxLength(50)]),
    emailAddress: new FormControl('', [Validators.required, Validators.email]),
    serialNumber: new FormControl('', [Validators.required, Validators.maxLength(20)]),
    isOver18Confirmed: new FormControl(false, Validators.requiredTrue),
    });



}
