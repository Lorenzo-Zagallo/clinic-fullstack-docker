import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

export const authGuard: CanActivateFn = (route, state) => {
    const authService = inject(AuthService);
    const router = inject(Router);

    // 1. Pergunta para o serviço: "Tem token válido?"
    if (authService.estaLogado()) {
        return true; // Pode passar!
    }

    // 2. Se não tiver, manda pro Login
    router.navigate(['/login']);
    return false; // Bloqueia a navegação atual
};
