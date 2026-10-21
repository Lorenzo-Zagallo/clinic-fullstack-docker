using Clinica.Application.DTOs;
using FluentValidation;

namespace Clinica.Application.Validators
{
    public class CreatePacienteValidator : AbstractValidator<CreatePacienteDTO>
    {
        public CreatePacienteValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .Length(3, 100).WithMessage("O nome deve ter entre 3 e 100 caracteres.");

            RuleFor(x => x.CPF)
                .NotEmpty().WithMessage("O CPF é obrigatório.")
                .Matches(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$").WithMessage("Formato de CPF inválido. Use 000.000.000-00.");

            RuleFor(x => x.ValorConsulta)
                .GreaterThanOrEqualTo(0).WithMessage("O valor da consulta não pode ser negativo.");
        }
    }
}