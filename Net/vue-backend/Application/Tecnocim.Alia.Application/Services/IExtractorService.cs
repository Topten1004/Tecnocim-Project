using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Services;

public interface IExtractorService
{
    Task<bool> Extract(UploadFicheroResponse fichero);

    Task<bool> Sincroniza(UploadFicheroResponse fichero);
}
