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
  private formSubmitAttempted: boolean = false;

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

  get credentials(): FormGroup | null {
    return this.signupForm.form.get('credentials') as FormGroup;
  }

  get rewardAgreement(): AbstractControl | undefined | null {
    return this.credentials?.get('rewardAgreement');
  }

  getFromControl(name: string): AbstractControl | undefined | null {
    return this.credentials?.get(name);
  }

  isFieldInvalid(field: string): boolean {
    const isFieldValid = this.credentials?.get(field)?.valid as boolean;
    return this.formSubmitAttempted && !isFieldValid;
  }

  isPasswordMismatched(): boolean {
    const isMismatched = this.credentials?.getError('passwordMismatch') as boolean
    return this.formSubmitAttempted && isMismatched;
  }

  submitCredentials() {
    this.formSubmitAttempted = true;

    if (this.credentials?.valid) {
      const credentialsData = new Credentials(
        this.getFromControl('login')?.value,
        this.getFromControl('password')?.value,
        this.getFromControl('password2')?.value,
        this.rewardAgreement?.value);
      this.credentialsSubmited.emit(credentialsData);
    }    
  }
}
