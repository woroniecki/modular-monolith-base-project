import { Component, Input, Output, EventEmitter } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-dynamic-form',
  templateUrl: './dynamic-form.component.html',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    CommonModule,
  ],
})
export class DynamicFormComponent {
  @Input() formConfig: {
    label: string;
    name: string;
    type: string;
    required: boolean;
  }[] = [];
  @Input() submitButtonLabel: string = 'Submit';
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  @Output() formSubmit = new EventEmitter<any>();

  form!: FormGroup;

  constructor(private fb: FormBuilder) {}

  ngOnInit() {
    const formControls = this.formConfig.reduce(
      (controls: { [key: string]: unknown }, field) => {
        controls[field.name] = field.required
          ? [null, Validators.required]
          : [null];
        return controls;
      },
      {},
    );
    this.form = this.fb.group(formControls);
  }

  onSubmit() {
    if (this.form.valid) {
      this.formSubmit.emit(this.form.value);
    }
  }
}
