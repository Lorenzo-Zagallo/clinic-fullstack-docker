import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { PacientesService } from '../../../core/services/pacientes.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-form',
    standalone: true,
    imports: [ReactiveFormsModule],
    templateUrl: './form.component.html',
    styleUrl: './form.component.css',
})
export class FormComponent {
    private fb = inject(FormBuilder);
    private service = inject(PacientesService);
    private router = inject(Router);

    // Formulário igual ao DTO do Backend
    form: FormGroup = this.fb.group({
        nome: ['', [Validators.required, Validators.minLength(3)]],
        cpf: ['', [Validators.required]], // Poderíamos adicionar validador de CPF depois
        valorConsulta: [0, [Validators.required, Validators.min(0)]]
    });

    onSubmit() {
        if (this.form.invalid) return;

        // Envia para o serviço
        this.service.cadastrar(this.form.value).subscribe({
            next: () => {
                alert('Paciente cadastrado com sucesso!');
                // Volta para a lista
                this.router.navigate(['/pacientes']);
            },
            error: (erro) => {
                console.error(erro);
                alert('Erro ao cadastrar.');
            }
        })
    }

    // Botão cancelar volta para a lista
    cancelar() {
        this.router.navigate(['/pacientes']);
    }
}
