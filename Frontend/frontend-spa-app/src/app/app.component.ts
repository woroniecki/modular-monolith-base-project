import { Component } from '@angular/core';
import { AuthService } from './services/auth.service';
import { CommonModule } from '@angular/common';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'frontend-spa-app';

  constructor(private dialog: MatDialog, public authService: AuthService) {}

  onRegister() {
    this.dialog.open(RegisterComponent, {
      width: '350px'
    });
  }

  onLogin() {
    this.dialog.open(LoginComponent, {
      width: '350px'
    });
  }

  onLogout() {
    this.authService.logout();
  }
}
