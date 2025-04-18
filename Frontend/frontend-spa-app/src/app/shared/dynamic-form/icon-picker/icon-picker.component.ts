import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AngularSvgIconModule } from 'angular-svg-icon';

@Component({
  selector: 'app-icon-picker',
  standalone: true,
  imports: [CommonModule, AngularSvgIconModule],
  templateUrl: './icon-picker.component.html',
  styleUrl: './icon-picker.component.css',
})
export class IconPickerComponent {
  @Input() icons: string[] = [];
  @Input() selectedIcon: string | null = null;
  @Output() iconChange = new EventEmitter<string>();

  selectIcon(icon: string) {
    this.selectedIcon = icon;
    this.iconChange.emit(icon);
  }
}
