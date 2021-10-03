using Branef.Negocio.Models.Enum;
using Branef.Negocio.Models.Validator;
using FluentValidation.Results;

namespace Branef.Negocio.Models
{
    public class Cliente : Entity
    {
        public string Nome { get; set; }
        public EPorteEmpresa Porte { get; set; }

        public override bool EhValido(out ValidationResult validationResult)
        {
            validationResult = new ClienteValidator().Validate(this);
            return validationResult.IsValid;
        }
    }
}