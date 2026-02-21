import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
    selector: 'app-root',
    standalone: true,
    imports: [RouterOutlet], // importante para o <router-outlet> funcionar
    template: '<router-outlet></router-outlet>'
})
export class AppComponent {
    title = 'clinica-front';
}