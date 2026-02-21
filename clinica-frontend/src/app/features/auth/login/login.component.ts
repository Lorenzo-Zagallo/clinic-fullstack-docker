import { Component, inject } from '@angular/core';
import { AuthService } from '../../../core/services/auth.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-login',
    standalone: true,

    // IMPORTANTE: Precisamos importar os módulos que usamos no HTML aqui
    imports: [ReactiveFormsModule],
    templateUrl: './login.component.html',
    styleUrl: './login.component.css',
})
export class LoginComponent {
    // Injeções de Dependência
    private fb = inject(FormBuilder);   // Para criar o formulário
    private authService = inject(AuthService); // Para chamar a API
    private router = inject(Router);    // Para mudar de página

    // Definição do Formulário
    loginForm: FormGroup = this.fb.group({
        nomeUsuario: ['', [Validators.required]], // Campo obrigatório
        senha: ['', [Validators.required]]        // Campo obrigatório
    })

    erroLogin: boolean = false; // Para mostrar mensagem de erro na tela

    onSubmit() {
        // 1. Se o formulário estiver inválido, não faz nada
        if (this.loginForm.invalid) return;

        // 2. Chama o serviço de autenticação
        this.authService.login(this.loginForm.value).subscribe({
            next: (response) => {
                console.log('Login Sucesso!', response);
                // 3. Se der certo, redireciona para a home (ou lista de pacientes)
                this.router.navigate(['/dashboard']);
            },
            error: (err) => {
                console.error('Erro no login', err);
                // 4. Se der errado, mostra o alerta vermelho
                this.erroLogin = true;
            }
        })
    }
}
