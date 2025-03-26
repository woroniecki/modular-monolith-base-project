import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../api-client/services';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  standalone: true,
  styleUrl: './login.component.css',
  imports: [
    MatFormFieldModule,
    MatToolbarModule,
    MatButtonModule,
    MatInputModule,
    FormsModule,
  ],
})
export class LoginComponent {
  username: string = '';
  password: string = '';

  constructor(private apiAccountService: AccountService) {}

  onSubmit() {
    this.apiAccountService
      .apiAccountLoginPost({
        body: { username: this.username, password: this.password },
      })
      .subscribe(() => console.log('User logged in:', this.username));
  }
}
