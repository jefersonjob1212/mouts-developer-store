import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

import { ConfirmCancelComponent } from './confirm-cancel.component';

describe('ConfirmCancelComponent', () => {
  let component: ConfirmCancelComponent;
  let fixture: ComponentFixture<ConfirmCancelComponent>;
  let dialogRefSpy: jasmine.SpyObj<MatDialogRef<ConfirmCancelComponent>>;

  beforeEach(async () => {
    dialogRefSpy = jasmine.createSpyObj('MatDialogRef', [
      'close'
    ]);

    await TestBed.configureTestingModule({
      imports: [
        ConfirmCancelComponent
      ],
      providers: [
        {
          provide: MatDialogRef,
          useValue: dialogRefSpy
        },
        {
          provide: MAT_DIALOG_DATA,
          useValue: 'Cancel sale confirmation'
        }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(ConfirmCancelComponent);
    component = fixture.componentInstance;

    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should receive dialog data', () => {
    expect(component.data).toBe('Cancel sale confirmation');
  });

  it('should initialize cancel model with dialog data', () => {
    expect(component.cancel()).toBe('Cancel sale confirmation');
  });

  it('should close dialog when clicking no', () => {
    component.onNoClick();

    expect(dialogRefSpy.close).toHaveBeenCalled();
  });

  it('should close dialog without returning value', () => {
    component.onNoClick();

    expect(dialogRefSpy.close).toHaveBeenCalledWith();
  });
});