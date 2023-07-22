import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FindTeammeteSeacrhComponent } from './find-teammete-seacrh.component';

describe('FindTeammeteSeacrhComponent', () => {
  let component: FindTeammeteSeacrhComponent;
  let fixture: ComponentFixture<FindTeammeteSeacrhComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FindTeammeteSeacrhComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FindTeammeteSeacrhComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
