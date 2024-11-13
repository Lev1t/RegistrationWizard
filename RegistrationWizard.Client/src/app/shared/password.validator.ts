import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function passwordValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;

    if (!value) {
      return null;
    }
    
    const hasLetter = /\p{Letter}/gu.test(value);
    const hasNumber = /\d/.test(value);
    
    if (!hasLetter || !hasNumber) {
      return { passwordStrength: 'Password must contain at least one letter and one digit.' };
    }
    
    return null;
  };
}