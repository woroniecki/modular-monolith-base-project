import { Component } from '@angular/core';
import { DynamicFormComponent } from '../shared/dynamic-form/dynamic-form.component';
import { ErrorModalComponent } from '../shared/error-modal/error-modal.component';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { AccountService } from '../api-client/services';
import { DialogResult } from '../shared/dialog-result.enum';
import { LoginComponent } from '../login/login.component';

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

  constructor(
    public dialogRef: MatDialogRef<RegisterComponent>,
    private dialog: MatDialog,
    private apiAccountService: AccountService
  ) {}

  onSubmit(formData: { username: string; email: string; password: string }) {
    this.apiAccountService
      .apiUsermanagementAccountRegisterPost({
        body: {
          username: formData.username,
          email: formData.email,
          password: formData.password,
        },
      })
      .subscribe({
        next: () => {
          this.dialogRef.close(DialogResult.SUCCESS);
          this.dialog.open(LoginComponent, {
                width: '350px'
              });
        },
        error: (err) => {
          console.log(err);
          this.dialog.open(ErrorModalComponent, {
            data: {
              message: `Register failed.\n${err.message}`,
            },
            width: '400px',
          });
        },
      });
  }
}
