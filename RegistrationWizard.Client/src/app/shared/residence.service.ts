import { Injectable } from '@angular/core';
import { Country } from './models/country';
import { Province } from './models/province';
import { environment } from '../environment';

@Injectable({
  providedIn: 'root'
})
export class LocationService {

  url = environment.baseUrl;
  constructor() { }

  async getCountries(): Promise<Country[]> {
    //TODO: use httpClient
    const data = await fetch(`${this.url}Location/countries`);

    return (await data.json()) ?? [];
  }

  async getProvinces(countryId: number): Promise<Province[]> {
    //TODO: use httpClient
    const data = await fetch(`${this.url}Location/country/${countryId}/provinces`);
    return (await data.json()) ?? [];
  }
}
