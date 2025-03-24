import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-landing-page',
  standalone: true,
  imports: [MatToolbarModule, MatButtonModule],
  templateUrl: './landing-page.component.html',
  styleUrl: './landing-page.component.css'
})
export class LandingPageComponent {
  title = 'frontend-spa-app';

  constructor(private router: Router) {}

  onRegister() {
    this.router.navigate(['/register']);
  }

  onLogin() {
    this.router.navigate(['/login']);
  }
}
