import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AbstractControl, ControlContainer, FormControl, FormGroup, FormGroupDirective, Validators } from '@angular/forms';
import { passwordValidator } from '../../shared/password.validator';
import { passwordMatchValidator } from '../../shared/password-match.validator';
import { Credentials } from '../models/credentials';

@Component({
  selector: 'signup-credentials',
  templateUrl: './credentials.component.html',
  styleUrl: './credentials.component.css',
  viewProviders: [
    {
      provide: ControlContainer,
      useExisting: FormGroupDirective
    }
  ]
})

export class CredentialsComponent implements OnInit {
  @Output() credentialsSubmited = new EventEmitter<Credentials>();

  signupForm: FormGroupDirective;

  constructor(parent: FormGroupDirective) {
    this.signupForm = parent;
  }

  ngOnInit() {
    this.signupForm.form.addControl('credentials', new FormGroup({
      login: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, passwordValidator()]),
      password2: new FormControl('', [Validators.required,]),
      rewardAgreement: new FormControl(false, [Validators.requiredTrue,]),
    },
    { validators: passwordMatchValidator }));
  }

  get credentials(): AbstractControl | null {
    return this.signupForm.form.get('credentials');
  }

  get login(): AbstractControl | undefined | null {
    return this.credentials?.get('login');
  }

  get password(): AbstractControl | undefined | null {
    return this.credentials?.get('password');
  }

  get password2(): AbstractControl | undefined | null {
    return this.credentials?.get('password2');
  }

  get rewardAgreement(): AbstractControl | undefined | null {
    return this.credentials?.get('rewardAgreement');
  }

  submitCredentials() {
    const credentialsData = new Credentials(
      this.login?.value,
      this.password?.value,
      this.password2?.value,
      this.rewardAgreement?.value
    );
    this.credentialsSubmited.emit(credentialsData);
  }
}
