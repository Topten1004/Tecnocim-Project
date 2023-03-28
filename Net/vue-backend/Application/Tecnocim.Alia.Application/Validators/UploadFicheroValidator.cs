using FluentValidation;
using Tecnocim.Alia.Application.Request;

namespace Tecnocim.Alia.Application.Validators;

public class UploadFicheroValidator : AbstractValidator<UploadFicheroRequest>
{
    public UploadFicheroValidator()
    {
        var origenList = new List<string> { "BSS", "Cirbe", "Modelo200"};

        RuleFor(fichero => fichero.Fichero).NotNull().NotEmpty().WithMessage("El fichero no puede estar vacío").SetValidator(new FileValidator());
        RuleFor(empresa => empresa.Origen).NotNull().NotEmpty().Must(x => origenList.Contains(x)).WithMessage("El origen no es válido"); ;
        RuleFor(empresa => empresa.EmpresaId).NotNull().NotEmpty().GreaterThan(0).WithMessage("El identificador de empresa no es válido");
        RuleFor(empresa => empresa.FechaContenido).NotNull().NotEmpty().WithMessage("La fecha de contenido es requerida");
    }
}
