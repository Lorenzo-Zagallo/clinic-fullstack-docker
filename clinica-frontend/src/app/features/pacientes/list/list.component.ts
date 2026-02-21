import { Component, inject, OnInit } from '@angular/core';
import { PacientesService } from '../../../core/services/pacientes.service';
import { Paciente } from '../../../core/models/paciente.model';
import { Router, RouterLinkActive } from "@angular/router";
import { AuthService } from '../../../core/services/auth.service';

@Component({
    selector: 'app-list',
    standalone: true,
    imports: [],
    templateUrl: './list.component.html',
    styleUrl: './list.component.css',
})
export class ListComponent implements OnInit {
    
    private service = inject(PacientesService);
    private router = inject(Router);
    private authService = inject(AuthService);

    // Variável que guarda a lista da tela
    pacientes: Paciente[] = [];

    ngOnInit(): void {
        this.carregarPacientes();
    }

    carregarPacientes() {
        this.service.getPacientes().subscribe({
            next: (dados) => {
                this.pacientes = dados;
                console.log('Pacientes carregados: ', dados);
            },
            error: (erro) => console.error('Erro ao carregar', erro)
        });
    }

    deletar(id: number) {
        if(confirm("Tem certeza que deseja deletar?")) {
            this.service.deletar(id).subscribe(() => {
                // Atualizar a lista na tela removendo o item apagado
                this.pacientes = this.pacientes.filter(p => p.id !== id);
            })
        }
    }

    irParaCadastro() {
        this.router.navigate(['/pacientes/novo']);
    }

    logout() {
        this.authService.logout(); // Limpa o localStorage
        this.router.navigate(['/login']); // Vai pra tela de login
    }

    irParaDashboard() {
        this.router.navigate(['/dashboard']);
    }
}
