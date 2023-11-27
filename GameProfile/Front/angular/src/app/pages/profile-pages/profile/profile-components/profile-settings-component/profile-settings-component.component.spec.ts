import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileSettingsComponentComponent } from './profile-settings-component.component';

describe('ProfileSettingsComponentComponent', () => {
  let component: ProfileSettingsComponentComponent;
  let fixture: ComponentFixture<ProfileSettingsComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProfileSettingsComponentComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProfileSettingsComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
