import {Component} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {DrawEntry} from '../models';
import {DataService} from '../data.service';
import {firstValueFrom} from 'rxjs';
import {HttpClient, HttpErrorResponse} from "@angular/common/http";

@Component({
    selector: 'app-draw-landing-page',
    templateUrl: 'draw-landing-page.component.html'
})
export class DrawLandingPageComponent {
    firstName = new FormControl('', [Validators.required, Validators.maxLength(50)]);
    lastName = new FormControl('', [Validators.required, Validators.maxLength(50)]);
    emailAddress = new FormControl('', [Validators.required, Validators.email]);
    serialNumber = new FormControl('', [Validators.required, Validators.maxLength(20)]);
    isOver18Confirmed = new FormControl(false, Validators.requiredTrue);

    entryFormGroup = new FormGroup({
        firstName: this.firstName,
        lastName: this.lastName,
        emailAddress: this.emailAddress,
        serialNumber: this.serialNumber,
        isOver18Confirmed: this.isOver18Confirmed
    });


    constructor(public dataService: DataService,
                public http: HttpClient) {
    }


    async submitEntry() {

        try {
            const call = this.http.post<DrawEntry>('http://localhost:5000/api/entry/', this.entryFormGroup.value);
            const result = await firstValueFrom<DrawEntry>(call);
            this.dataService.drawEntries.push(result);

        } catch (error: any) {
            console.log(error);
            let errorMessage = 'Error';

            if (error instanceof HttpErrorResponse) {
                //The backend returned an unsuccessful response code.
                errorMessage = error.error?.message || 'Server error';
            } else if (error.error instanceof ErrorEvent) {
                //A client-side or network error occurred.
                errorMessage = error.error.message;
            }

        }


    }
}
