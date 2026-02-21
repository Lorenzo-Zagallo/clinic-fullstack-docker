export interface Consulta {
    id: number;
    dataHorario: string;
    tipo: string;
    status: string;
    valor: number;
    pacienteId: number;
    nomePaciente: string;
}

export interface CreateConsultaRequest {
    pacienteId: number;
    dataHorario: string;
    tipo: string;
    observacoes?: string;
    valor: number;
}