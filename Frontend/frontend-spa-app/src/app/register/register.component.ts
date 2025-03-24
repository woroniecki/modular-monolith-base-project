import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  standalone: true,
  styleUrl: './register.component.css',
  imports: [MatInputModule, MatFormFieldModule, MatToolbarModule, MatButtonModule],
})
export class RegisterComponent {
  username: string = '';
  password: string = '';
  email: string = '';

  register() {
    // Registration logic goes here
    console.log('User registered:', this.username, this.email);
  }
}