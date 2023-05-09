import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AfterLoginSteamComponent } from './after-login-steam.component';

describe('AfterLoginSteamComponent', () => {
  let component: AfterLoginSteamComponent;
  let fixture: ComponentFixture<AfterLoginSteamComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AfterLoginSteamComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AfterLoginSteamComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
