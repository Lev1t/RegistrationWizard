import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export const passwordMatchValidator: ValidatorFn = (
    control: AbstractControl,
  ): ValidationErrors | null => {
    const password = control.get('password');
    const password2 = control.get('password2');
    return password && password2 && password.value !== password2.value ? {passwordMismatch: true} : null;
  };