namespace Clinica.Domain.Entities
{
    public class Paciente
    {
        // O EF Core entende que "Id" é a chave primária (PK) automaticamente
        public int Id { get; private set; }
        public string Nome { get; private set; } = string.Empty;
        public string? CPF { get; private set; }
        public decimal ValorConsulta { get; private set; }
        public DateTime DataCadastro { get; private set; }

        // Construtor protegido exigido pelo Entity Framework Core
        protected Paciente() { }

        // O construtor impõe que é impossível criar um Paciente num estado inválido
        public Paciente(string nome, string? cpf, decimal valorConsulta)
        {
            if (valorConsulta < 0)
                throw new ArgumentException("O valor da consulta não pode ser negativo.", nameof(valorConsulta));

            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome é obrigatório.", nameof(nome));

            Nome = nome;
            CPF = cpf;
            ValorConsulta = valorConsulta;
            DataCadastro = DateTime.UtcNow; // UTC é mais seguro para sistemas distribuídos
        }

        // Método que expressa a intenção de negócio (Ubiquitous Language)
        public void AtualizarDados(string nome, string? cpf, decimal valorConsulta)
        {
            if (valorConsulta < 0)
                throw new ArgumentException("O valor da consulta não pode ser negativo.", nameof(valorConsulta));

            Nome = nome;
            CPF = cpf;
            ValorConsulta = valorConsulta;
        }
    }
}