import { Component, inject } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { SignupService } from '../shared/signup.service';
import { SignupData } from './models/signupdata';
import { Credentials } from './models/credentials';
import { Residence } from './models/residence';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.css'
})
export class SignupComponent {
  signupService: SignupService = inject(SignupService);

  signupForm: FormGroup
  credentials?: Credentials;

  constructor() {
    this.signupForm = new FormGroup({});
  }

  get credentialsSubmited(): boolean {
    return Boolean(this.credentials);
  }

  goToResedency(credentials: Credentials) {
    this.credentials = credentials;
  }

  submitForm(residence: Residence) {
    if (!residence || !this.credentials) {
      return;
    }
    const signupData = new SignupData(this.credentials!, residence);    
    this.signupService.createUser(signupData);
  }
}
