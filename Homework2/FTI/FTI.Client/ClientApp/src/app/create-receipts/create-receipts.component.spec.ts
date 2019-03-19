import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateReceiptsComponent } from './create-receipts.component';

describe('CreateReceiptsComponent', () => {
  let component: CreateReceiptsComponent;
  let fixture: ComponentFixture<CreateReceiptsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateReceiptsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateReceiptsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
