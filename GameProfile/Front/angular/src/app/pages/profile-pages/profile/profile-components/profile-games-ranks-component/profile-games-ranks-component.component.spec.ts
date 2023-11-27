import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileGamesRanksComponentComponent } from './profile-games-ranks-component.component';

describe('ProfileGamesRanksComponentComponent', () => {
  let component: ProfileGamesRanksComponentComponent;
  let fixture: ComponentFixture<ProfileGamesRanksComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProfileGamesRanksComponentComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProfileGamesRanksComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
