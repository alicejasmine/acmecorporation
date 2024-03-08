import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DrawLandingPageComponent } from './draw-landing-page.component';

describe('DrawLandingPageComponent', () => {
  let component: DrawLandingPageComponent;
  let fixture: ComponentFixture<DrawLandingPageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DrawLandingPageComponent]
    });
    fixture = TestBed.createComponent(DrawLandingPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
