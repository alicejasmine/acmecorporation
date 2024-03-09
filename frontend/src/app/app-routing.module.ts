import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DrawLandingPageComponent } from './draw-landing-page/draw-landing-page.component';
import { FormSubmissionsComponent } from './form-submissions/form-submissions.component';

const routes: Routes = [{ path: '', component: DrawLandingPageComponent },{path: 'entries', component: FormSubmissionsComponent },{ path: 'entries/:page', component: FormSubmissionsComponent }, ];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
