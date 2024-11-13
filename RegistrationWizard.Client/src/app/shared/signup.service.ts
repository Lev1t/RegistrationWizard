import { Injectable } from '@angular/core';
import { SignupData } from '../signup/models/signupdata';
import { environment } from '../environment';

@Injectable({
  providedIn: 'root'
})
export class SignupService {
  url = environment.baseUrl;

  async createUser(signupData: SignupData) {
    //TODO: use httpClient
    await fetch(`${this.url}Authentication/signup`, {
      method: "POST",
      body: JSON.stringify(signupData),
      headers: {
        "Content-type": "application/json"
      }
    });
  }
}
