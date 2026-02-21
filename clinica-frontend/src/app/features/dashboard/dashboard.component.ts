import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { BaseChartDirective } from 'ng2-charts';
import { PacientesService } from '../../core/services/pacientes.service';
import { DashboardData } from '../../core/models/dashboard.model';
import { ChartConfiguration, ChartOptions } from 'chart.js';
import { Router } from '@angular/router';
import { Consulta, CreateConsultaRequest } from '../../interfaces/consulta.interface';
import { ConsultaService } from '../../services/consulta.service';
import { FormsModule } from '@angular/forms';

@Component({
    selector: 'app-dashboard',
    standalone: true,
    imports: [CommonModule, BaseChartDirective, FormsModule],
    templateUrl: './dashboard.component.html',
    styleUrl: './dashboard.component.css',
})
export class DashboardComponent implements OnInit {
    private pacientesService = inject(PacientesService);
    private router = inject(Router);

    dados: DashboardData | null = null;

    // Configuração do Gráfico (Bar Chart)
    public barChartData: ChartConfiguration<'bar'>['data'] = {
        labels: ['Faturamento', 'Média por Paciente'],
        datasets: [
            { data: [0, 0], label: 'Indicadores Financeiros (R$)', backgroundColor: ['#2563EB',]}
        ]
    };
    public barChartOptions: ChartOptions<'bar'> = { 
        responsive: true,
        maintainAspectRatio: false,
    };
    public barChartLegend = true;

    ngOnInit() {
        this.carregarDashboard();
        this.carregarConsultas();
    }

    carregarDashboard() {
        this.pacientesService.getDashboard().subscribe(res => {
            this.dados = res;

            // Atualiza o gráfico com dados reais
            this.barChartData = {
                labels: ['Faturamento Total', 'Média por Consulta'],
                datasets: [
                    {
                        data: [res.faturamentoTotal, res.mediaValorConsulta],
                        label: 'Valores em Reais (R$)',
                        backgroundColor: ['#3B82F6', '#10B981']
                    }
                ]
            };
        });
    }

    irParaListaPacientes() {
        this.router.navigate(['/pacientes']);
    }


    private consultaService = inject(ConsultaService);

    // lista de consultas para mostrar na tela
    consultas: Consulta[] = [];

    // objeto que segura os dados do formulário
    novaConsulta: CreateConsultaRequest = {
        pacienteId: 1,
        dataHorario: '',
        tipo: 'Rotina',
        valor: 150.00,
        observacoes: ''
    };

    carregarConsultas() {
        this.consultaService.listarTodas().subscribe({
            next: (dados) => {
                this.consultas = dados;
                console.log('Consultas carregadas: ', dados);
            },
            error: (erro) => {
                console.error('Erro ao carregar: ', erro);
            }
        });
    }

    agendar() {
        // validação simples
        if (!this.novaConsulta.dataHorario) {
            alert('Por favor, escolha uma data e hora!');
            return;
        }

        this.consultaService.agendar(this.novaConsulta).subscribe({
            next: (consultaCriada) => {
                alert('Consulta agendada com sucesso!');
                this.carregarConsultas(); // atualiza a lista na hora
                this.carregarDashboard(); // atualiza a tabela e o gráfico de faturamento

                // limpeza dos campos do formulário
                this.novaConsulta.observacoes = '';
                this.novaConsulta.dataHorario = '';
            },
            error: (erro) => {
                console.error('Erro ao agendar:', erro);
                alert('Erro ao agendar. Verifique se o Paciente ID existe no banco!')
            }
        });
    }
}
