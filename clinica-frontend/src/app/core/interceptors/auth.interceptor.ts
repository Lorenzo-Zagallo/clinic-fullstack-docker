import { HttpInterceptorFn } from '@angular/common/http';

export const authInterceptor: HttpInterceptorFn = (req, next) => {

    // 1. Pega o token salvo no navegador
    const token = localStorage.getItem('access_token');

    // 2. Se o token existir, clonamos a requisição original e adicionamos o cabeçalho
    if (token) {
        const reqComToken = req.clone({
            setHeaders: {
                Authorization: `Bearer ${token}` // O padrão JWT que o Backend espera
            }
        });

        // Passa a requisição alterada para frente
        return next(reqComToken);
    }

    // 3. Se não tiver token (ex: tela de login), passa a requisição original
    return next(req);
};
