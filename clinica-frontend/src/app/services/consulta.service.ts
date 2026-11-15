import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Consulta, CreateConsultaRequest } from '../interfaces/consulta.interface';
import { environment } from '../../environments/environment';

@Injectable({
    providedIn: 'root',
})
export class ConsultaService {
    // lembre-se : no Docker, o front acessa o back pela porta 5137 (externa)
    private apiUrl = `${environment.apiUrl}/consultas`;

    constructor(private http: HttpClient) { }

    listarTodas(): Observable<Consulta[]> {
        return this.http.get<Consulta[]>(this.apiUrl);
    }

    agendar(dados: CreateConsultaRequest): Observable<Consulta> {
        return this.http.post<Consulta>(this.apiUrl, dados);
    }

}
