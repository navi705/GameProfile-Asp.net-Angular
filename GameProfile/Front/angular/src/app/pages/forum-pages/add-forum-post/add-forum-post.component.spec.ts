import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddForumPostComponent } from './add-forum-post.component';

describe('AddForumPostComponent', () => {
  let component: AddForumPostComponent;
  let fixture: ComponentFixture<AddForumPostComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddForumPostComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddForumPostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
