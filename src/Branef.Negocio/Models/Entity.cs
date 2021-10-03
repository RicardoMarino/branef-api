using FluentValidation.Results;
using System;

namespace Branef.Negocio.Models
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public abstract bool EhValido(out ValidationResult validationResult);
    }
}