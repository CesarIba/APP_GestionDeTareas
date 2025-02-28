using FluentValidation;
using BackGestionTareas.Models;

namespace BackGestionTareas.Models.Validators
{
    public class TareaDtoValidator : AbstractValidator<TareaDto>
    {
        public TareaDtoValidator()
        {
            RuleFor(t => t.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .MaximumLength(100).WithMessage("El nombre no puede tener más de 100 caracteres");

            RuleFor(t => t.Estado)
                .NotNull().WithMessage("El campo 'estado' es obligatorio, debe ser true o false.");
        }
    }
}