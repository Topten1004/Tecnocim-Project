using FluentValidation;
using Tecnocim.Alia.Application.Request;

namespace Tecnocim.Alia.Application.Validators;

public class EmpresaValidator : AbstractValidator<CreateEmpresaRequest>
{
    public EmpresaValidator()
    {
        RuleFor(empresa => empresa.CIF).NotNull().NotEmpty().MaximumLength(50).WithMessage("El CIF es obligatorio");
        RuleFor(empresa => empresa.Nombre).NotNull().NotEmpty().MaximumLength(50).WithMessage("El Nombre es obligatorio"); ;
        RuleFor(empresa => empresa.Contacto).MaximumLength(50);
        RuleFor(empresa => empresa.Telefono).MaximumLength(20);
        RuleFor(empresa => empresa.Email).MaximumLength(50).EmailAddress().WithMessage("El email no tiene el formato correcto");
        RuleFor(empresa => empresa.UsuarioId).GreaterThan(0).WithMessage("El identificador del usuario no es válido");
    }
}
