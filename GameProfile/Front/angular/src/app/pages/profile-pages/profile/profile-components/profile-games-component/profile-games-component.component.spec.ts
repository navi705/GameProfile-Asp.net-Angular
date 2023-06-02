import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileGamesComponentComponent } from './profile-games-component.component';

describe('ProfileGamesComponentComponent', () => {
  let component: ProfileGamesComponentComponent;
  let fixture: ComponentFixture<ProfileGamesComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProfileGamesComponentComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProfileGamesComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
