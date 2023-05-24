import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GameFiltersCreatorsComponent } from './game-filters-creators.component';

describe('GameFiltersCreatorsComponent', () => {
  let component: GameFiltersCreatorsComponent;
  let fixture: ComponentFixture<GameFiltersCreatorsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GameFiltersCreatorsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GameFiltersCreatorsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
