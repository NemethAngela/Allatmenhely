import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllatlistaComponent } from './allatlista.component';

describe('AllatlistaComponent', () => {
  let component: AllatlistaComponent;
  let fixture: ComponentFixture<AllatlistaComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AllatlistaComponent]
    });
    fixture = TestBed.createComponent(AllatlistaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
