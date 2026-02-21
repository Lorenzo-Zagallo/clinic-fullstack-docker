import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Paciente } from '../models/paciente.model';
import { DashboardData } from '../models/dashboard.model';

@Injectable({
    providedIn: 'root',
})
export class PacientesService {
    private http = inject(HttpClient);
    private apiUrl = 'http://localhost:5137/api/pacientes';

    // GET: Listar todos
    getPacientes(): Observable<Paciente[]> {
        return this.http.get<Paciente[]>(this.apiUrl);
    }

    // POST: Criar
    cadastrar(paciente: Paciente): Observable<Paciente> {
        return this.http.post<Paciente>(this.apiUrl, paciente);
    }

    // DELETE: Apagar
    deletar(id: number): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/${id}`);
    }

    // PUT: Atualizar (faremos depois)

    // GET: Buscar dados para o Dashboard
    getDashboard(): Observable<DashboardData> {
        return this.http.get<DashboardData>(`${this.apiUrl}/dashboard`);
    }
}
