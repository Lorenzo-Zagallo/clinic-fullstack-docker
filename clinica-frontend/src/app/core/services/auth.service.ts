import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { LoginRequest, LoginResponse } from '../models/login.model';

@Injectable({
    providedIn: 'root' // Singleton: Uma única instância para o app todo
})
export class AuthService {
    // Injeção de dependência moderna (Angular 16+)
    private http = inject(HttpClient);

    // URL da sua API .NET (ajuste a porta se necessário)
    private apiUrl = 'http://localhost:5137/api/auth';

    // Método de Login
    login(dados: LoginRequest): Observable<LoginResponse> {

        // Post: Envia dados e espera receber um LoginResponse
        return this.http.post<LoginResponse>(`${this.apiUrl}/login`, dados).pipe(
            
            // tap: É um "efeito colateral".
            // Executa algo com a resposta SEM modificar o dado que vai pra tela.
            tap((resposta) => {
                // Passo crucial: Salvar o Token no navegador
                localStorage.setItem('access_token', resposta.token);
            })
        )
    }

    // Método de Logout
    logout() {
        localStorage.removeItem('access_token');
        // Aqui poderíamos redirecionar para /login
    }

    // Verifica se o usuário tem um token salvo
    estaLogado(): boolean {
        // !! converte string para boolean (se tiver texto = true, se for null = false)
        return !!localStorage.getItem('access_token');
    }
}
