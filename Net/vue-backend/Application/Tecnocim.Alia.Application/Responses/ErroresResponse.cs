namespace Tecnocim.Alia.Application.Responses;

public class ErroresResponse
{
    public ErroresResponse()
    {
    }

    public ErroresResponse(string mensaje, string traza, int? bloqueo)
    {
        Mensaje = mensaje;
        Traza = traza;
        Bloqueo = bloqueo;
    }

    public long? Id { get; set; }
    public string Mensaje { get; set; }
    public string Traza { get; set; }
    public int? Bloqueo { get; set; }
    public string Tabla { get; set; }
    public string Campo { get; set; }
    public long? ExtraccionId { get; set; }
}
