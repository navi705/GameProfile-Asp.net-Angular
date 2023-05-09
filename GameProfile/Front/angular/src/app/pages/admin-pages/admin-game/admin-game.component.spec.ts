import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminGameComponent } from './admin-game.component';

describe('AdminGameComponent', () => {
  let component: AdminGameComponent;
  let fixture: ComponentFixture<AdminGameComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminGameComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminGameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
