import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddSteamAccountComponent } from './add-steam-account.component';

describe('AddSteamAccountComponent', () => {
  let component: AddSteamAccountComponent;
  let fixture: ComponentFixture<AddSteamAccountComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddSteamAccountComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddSteamAccountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
