using FluentValidation;
using Tecnocim.Alia.Application.Request;

namespace Tecnocim.Alia.Application.Validators;

public class ContratoValidator : AbstractValidator<CreateContratoRequest>
{
    public ContratoValidator()
    {
        RuleFor(empresa => empresa.Cuenta).NotNull().WithMessage("La cuenta es obligatoria");
        RuleFor(empresa => empresa.Entidad).NotNull().WithMessage("La entidad es obligatoria");
        RuleFor(empresa => empresa.TipoProducto).NotNull().WithMessage("El tipo de producto es obligatorio");
        RuleFor(empresa => empresa.ImporteInicial).NotNull().WithMessage("El importe inicial es obligatorio");
        RuleFor(empresa => empresa.Moneda).NotNull().WithMessage("La moneda es obligatoria");
        RuleFor(empresa => empresa.FormaDePago).NotNull().WithMessage("La forma de pago es obligatoria");
        RuleFor(empresa => empresa.FechaInicio).NotNull().WithMessage("La fecha de inicio es obligatoria");
        RuleFor(empresa => empresa.FechaFin).NotNull().WithMessage("La fecha de finalización es obligatoria");
        RuleFor(empresa => empresa.FechaFin).GreaterThanOrEqualTo(empresa => empresa.FechaInicio).WithMessage("La fecha de finalización debe ser mayor que la fecha de inicio");
        RuleFor(empresa => empresa.Precio).NotNull().WithMessage("El precio es obligatorio");
        RuleFor(empresa => empresa.Cirbe).NotNull().WithMessage("El Cirbe es obligatorio");
    }
}

public class UpdateContratoValidator : AbstractValidator<UpdateContratoRequest>
{
    public UpdateContratoValidator()
    {
        RuleFor(empresa => empresa.ContratoId).NotNull().WithMessage("El identificador es obligatorio");
        RuleFor(empresa => empresa.Cuenta).NotNull().WithMessage("La cuenta es obligatoria");
        RuleFor(empresa => empresa.Entidad).NotNull().WithMessage("La entidad es obligatoria");
        RuleFor(empresa => empresa.TipoProducto).NotNull().WithMessage("El tipo de producto es obligatorio");
        RuleFor(empresa => empresa.ImporteInicial).NotNull().WithMessage("El importe inicial es obligatorio");
        RuleFor(empresa => empresa.Moneda).NotNull().WithMessage("La moneda es obligatoria");
        RuleFor(empresa => empresa.FormaDePago).NotNull().WithMessage("La forma de pago es obligatoria");
        RuleFor(empresa => empresa.FechaInicio).NotNull().WithMessage("La fecha de inicio es obligatoria");
        RuleFor(empresa => empresa.FechaFin).NotNull().WithMessage("La fecha de finalización es obligatoria");
        RuleFor(empresa => empresa.FechaFin).GreaterThanOrEqualTo(empresa => empresa.FechaInicio).WithMessage("La fecha de finalización debe ser mayor que la fecha de inicio");
        RuleFor(empresa => empresa.Precio).NotNull().WithMessage("El precio es obligatorio");
        RuleFor(empresa => empresa.Cirbe).NotNull().WithMessage("El Cirbe es obligatorio");
    }
}