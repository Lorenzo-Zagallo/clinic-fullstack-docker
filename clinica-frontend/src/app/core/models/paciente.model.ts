export interface Paciente {
    id?: number; // Opcional porque ao criar não tem ID ainda
    nome: string;
    cpf: string;
    valorConsulta: number;
    dataCadastro?: string;
}