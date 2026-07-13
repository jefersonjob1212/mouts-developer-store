import { Component, inject } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { RouterOutlet } from '@angular/router';
import { MenuComponent } from "./shared/components/menu/menu.component";
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { LoadingService } from '@shared/services/loading.service';

@Component({
  selector: 'app-root',
  imports: [
    RouterOutlet,
    MatSidenavModule,
    MatListModule,
    MatIconModule,
    MenuComponent,
    MatProgressBarModule
],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'ambev-developer-evaluation';
  loadingService = inject(LoadingService);
}
