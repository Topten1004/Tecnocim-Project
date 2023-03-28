using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Tecnocim.Alia.Application.Validators;

public class FileValidator : AbstractValidator<IFormFile>
{
    public FileValidator()
    {
        RuleFor(x => x.Length).NotNull().LessThanOrEqualTo(20 * 1024 * 1024)
            .WithMessage("El tamaño del fichero es mas grande que el permitido (20 MB)");

        RuleFor(x => x.ContentType).NotNull().Must(x => x.Equals("application/vnd.ms-excel") 
        || x.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") || x.Equals("application/pdf"))
            .WithMessage("El tipo de fichero no está permitido, debe ser (xls, xlsx o pdf)");
    }
}