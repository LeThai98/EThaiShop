import { Component } from '@angular/core';
import { MatBadge } from '@angular/material/badge';
import { MatButton } from '@angular/material/button';
import { MatIcon} from '@angular/material/icon';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    MatIcon,
    MatButton,
    MatBadge,
    RouterOutlet, 
    RouterLink, 
    RouterLinkActive
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {

}
