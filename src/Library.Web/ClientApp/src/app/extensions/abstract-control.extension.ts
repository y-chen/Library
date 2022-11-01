import { AbstractControl, FormArray, FormControl, FormGroup } from '@angular/forms';

declare module '@angular/forms' {
  interface AbstractControl {
    getFormArray(controlName: string): FormArray;
    getFormControl(controlName: string): FormControl;
    getFormGroup(controlName: string): FormGroup;

    toFormArray(): FormArray;
    toFormControl(): FormControl;
    toFormGroup(): FormGroup;
  }
}

if (!AbstractControl.prototype.getFormArray) {
  AbstractControl.prototype.getFormArray = function (this: FormGroup, controlName: string): FormArray {
    return this.get(controlName) as FormArray;
  };
}

if (!AbstractControl.prototype.getFormControl) {
  AbstractControl.prototype.getFormControl = function (this: FormGroup, controlName: string): FormControl {
    return this.get(controlName) as FormControl;
  };
}

if (!AbstractControl.prototype.getFormGroup) {
  AbstractControl.prototype.getFormGroup = function (this: FormGroup, controlName: string): FormGroup {
    return this.get(controlName) as FormGroup;
  };
}

if (!AbstractControl.prototype.toFormArray) {
  AbstractControl.prototype.toFormArray = function (this: AbstractControl): FormArray {
    return this as FormArray;
  };
}

if (!AbstractControl.prototype.toFormControl) {
  AbstractControl.prototype.toFormControl = function (this: AbstractControl): FormControl {
    return this as FormControl;
  };
}

if (!AbstractControl.prototype.toFormGroup) {
  AbstractControl.prototype.toFormGroup = function (this: AbstractControl): FormGroup {
    return this as FormGroup;
  };
}
