import { Component } from '@angular/core';
import { DynamicFormComponent } from '../shared/dynamic-form/dynamic-form.component';
import { ErrorModalComponent } from '../shared/error-modal/error-modal.component';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

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
    private dialog: MatDialog,
    private router: Router,
    private auth: AuthService,
  ) {}

  onSubmit(formData: { username: string; password: string }) {
    this.auth.loginWithCredentials(formData).subscribe({
      next: () => {
        this.router.navigate(['/']);
      },
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
