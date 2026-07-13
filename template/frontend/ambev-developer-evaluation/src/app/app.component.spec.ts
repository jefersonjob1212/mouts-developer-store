import { signal } from '@angular/core';
import { AppComponent } from './app.component';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { LoadingService } from '@shared/services/loading.service';

describe('AppComponent', () => {

  let component: AppComponent;
  let fixture: ComponentFixture<AppComponent>;

  let loadingServiceMock: {
    loading: ReturnType<typeof signal>;
    show: jasmine.Spy;
    hide: jasmine.Spy;
  };

  beforeEach(async () => {
    loadingServiceMock = {
      loading: signal(false),
      show: jasmine.createSpy('show'),
      hide: jasmine.createSpy('hide')
    };

    await TestBed.configureTestingModule({
      imports: [
        AppComponent
      ],
      providers: [
        {
          provide: LoadingService,
          useValue: loadingServiceMock
        }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(AppComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component)
      .toBeTruthy();
  });

  it('should have default title', () => {
    expect(component.title)
      .toBe('ambev-developer-evaluation');
  });

  it('should inject loading service', () => {
    expect(component.loadingService)
      .toBeTruthy();
  });
});