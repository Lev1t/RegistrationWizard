import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SignupComponent } from './signup.component';
import { CredentialsComponent } from './credentials/credentials.component';
import { ResidenceComponent } from './residence/residence.component';
import { FieldErrorComponent } from '../shared/component/field-error/field-error.component';

@NgModule({
  declarations: [
    SignupComponent,
    CredentialsComponent,
    ResidenceComponent,
    FieldErrorComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: []
})
export class SignupModule { }
