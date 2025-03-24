import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  standalone: true,
  styleUrl: './login.component.css',
  imports: [MatFormFieldModule, MatToolbarModule, MatButtonModule, MatInputModule],
})
export class LoginComponent {
  username: string = '';
  password: string = '';

  constructor() {}

  onLogin() {
    // Logic for handling login submission
    console.log('Login attempted with', this.username, this.password);
  }
}