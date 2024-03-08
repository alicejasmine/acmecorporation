import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DrawLandingPageComponent } from './draw-landing-page/draw-landing-page.component';

const routes: Routes = [{ path: '', component: DrawLandingPageComponent } ];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
