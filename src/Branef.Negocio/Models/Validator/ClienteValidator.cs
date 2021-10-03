using Branef.Negocio.Models.Enum;
using FluentValidation;

namespace Branef.Negocio.Models.Validator
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            RuleFor(cliente => cliente.Nome)
                .NotEmpty()
                .WithMessage("O campo {PropertyName} deve ser informado.")
                .MinimumLength(3)
                .WithMessage("O campo {PropertyName} deve ter pelo menos {MinLength} caracteres.")
                .MaximumLength(200)
                .WithMessage("O campo {PropertyName} deve ter no máximo {MaxLength} caracteres.");

            RuleFor(cliente => cliente.Porte)
                .NotEqual(EPorteEmpresa.Selecione)
                .WithMessage("O campo {PropertyName} deve ser fornecido.");
        }
    }
}