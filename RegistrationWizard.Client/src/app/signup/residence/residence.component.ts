import { Component, EventEmitter, inject, Inject, OnInit, Output } from '@angular/core';
import { AbstractControl, ControlContainer, FormControl, FormGroup, FormGroupDirective, Validators } from '@angular/forms';
import { LocationService } from '../../shared/residence.service';
import { Country } from '../../shared/models/country';
import { Province } from '../../shared/models/province';
import { Residence } from '../models/residence';

@Component({
  selector: 'signup-residence',
  templateUrl: './residence.component.html',
  styleUrl: './residence.component.css',
  viewProviders: [
    {
      provide: ControlContainer,
      useExisting: FormGroupDirective
    }
  ]
})

export class ResidenceComponent implements OnInit  {
  @Output() residenceSubmited = new EventEmitter<Residence>();

  form: FormGroupDirective;
  residenceService: LocationService = inject(LocationService);

  countryList: Country[] = [];
  provinceList: Province[] = [];

  constructor(parent: FormGroupDirective) {
    this.form = parent;

    this.residenceService.getCountries().then((x: Country[]) => {
      this.countryList = x;
    });
  }
  
  ngOnInit() {
    this.form.form.addControl('residence', new FormGroup({
      countryId: new FormControl(null, Validators.required),
      provinceId: new FormControl(null, Validators.required),
    }));
  }

  get residence(): AbstractControl | null {
    return this.form.form.get('residence');
  }

  get countryId(): AbstractControl | undefined | null {
    return this.residence?.get('countryId');
  }

  get provinceId(): AbstractControl | undefined | null {
    return this.residence?.get('provinceId');
  }

  onCountryChanged() {
    const countryId = this.countryId;
    if (countryId) {
      this.residenceService.getProvinces(countryId.value).then((x: Province[]) => {
        this.provinceList = x;
        this.provinceId?.setValue(null);
      });
    }
  }

  submitResidence() {
    const residence = new Residence(this.countryId?.value, this.provinceId?.value);
    this.residenceSubmited.emit(residence);
  }
}
