import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Viewer3d } from './viewer3d';

describe('Viewer3d', () => {
  let component: Viewer3d;
  let fixture: ComponentFixture<Viewer3d>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Viewer3d]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Viewer3d);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
