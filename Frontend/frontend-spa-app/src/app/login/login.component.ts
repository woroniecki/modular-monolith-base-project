import { Component } from '@angular/core';
import { AccountService } from '../api-client/services';
import { DynamicFormComponent } from '../shared/dynamic-form/dynamic-form.component';
import { ErrorModalComponent } from '../shared/error-modal/error-modal.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  standalone: true,
  imports: [DynamicFormComponent],
})
export class LoginComponent {
  loginFormConfig = [
    { label: 'Username', name: 'username', type: 'text', required: true },
    { label: 'Password', name: 'password', type: 'password', required: true },
  ];

  constructor(
    private apiAccountService: AccountService,
    private dialog: MatDialog,
  ) {}

  onSubmit(formData: { username: string; password: string }) {
    console.log('FormData type:', typeof formData);
    this.apiAccountService
      .apiUsermanagementAccountLoginPost({ body: formData })
      .subscribe({
        next: () => console.log('User logged in:', formData.username),
        error: (err) => {
          this.dialog.open(ErrorModalComponent, {
            data: {
              message: `Login failed. Please check your credentials.\n${err.message}`,
            },
            width: '400px',
          });
        },
      });
  }
}
