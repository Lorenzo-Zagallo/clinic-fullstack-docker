// DTOs iguais ao Backend
export interface LoginRequest {
    nomeUsuario: string;
    senha: string;
}

export interface LoginResponse {
    usuario: string;
    token: string;
}