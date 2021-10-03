using System;
using System.ComponentModel.DataAnnotations;

namespace Branef.API.Contratos
{
    public class ClienteContrato
    {
        [Key]
        public Guid Id{ get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres", MinimumLength = 3)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Porte { get; set; }
    }
}