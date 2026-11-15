import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
    const router = inject(Router);
    const authService = inject(AuthService);

    return next(req).pipe(
        catchError((error: HttpErrorResponse) => {
            // Se o token expirou ou é inválido, a API retorna 401
            if (error.status === 401) {
                authService.logout();
                router.navigate(['/login']);
            }
            
            // Aqui poderíamos tratar 403 (Forbidden) ou formatar erros 400 (ProblemDetails)
            
            // Repassa o erro para que o componente também possa reagir (ex: exibir um Toast)
            return throwError(() => error);
        })
    );
};