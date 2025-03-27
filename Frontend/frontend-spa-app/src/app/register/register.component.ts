import { Component } from '@angular/core';
import { DynamicFormComponent } from '../shared/dynamic-form/dynamic-form.component';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  standalone: true,
  imports: [DynamicFormComponent],
})
export class RegisterComponent {
  registerFormConfig = [
    { label: 'Username', name: 'username', type: 'text', required: true },
    { label: 'Email', name: 'email', type: 'email', required: true },
    { label: 'Password', name: 'password', type: 'password', required: true },
  ];

  onSubmit(formData: object) {
    console.log('User registered:', formData);
    // Add registration logic here
  }
}
