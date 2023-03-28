using FluentValidation;
using Tecnocim.Alia.Application.Request;

namespace Tecnocim.Alia.Application.Validators
{
    public class UsuarioValidator : AbstractValidator<CreateUsuarioRequest>
    {
        public UsuarioValidator()
        {
            RuleFor(empresa => empresa.Nombre).NotNull().NotEmpty().MaximumLength(50).WithMessage("El Nombre es obligatorio");
            RuleFor(empresa => empresa.Apellidos).NotNull().NotEmpty().MaximumLength(100).WithMessage("Los apellidos son obligatorios");
            RuleFor(empresa => empresa.Email).NotNull().NotEmpty().WithMessage("El email es obligatorio").MaximumLength(100).EmailAddress().WithMessage("El email no tiene el formato correcto");
            RuleFor(empresa => empresa.Password).NotNull().NotEmpty().WithMessage("El password es obligatorio").MaximumLength(60);
            RuleFor(empresa => empresa.RolId).GreaterThan(0).LessThanOrEqualTo(4).WithMessage("El identificador del rol no es válido");
            //RuleFor(empresa => empresa.PuestoTrabajo).NotNull().NotEmpty().WithMessage("El puesto de trabajo es obligatorio").MaximumLength(100);
        }
    }
}
