namespace Tecnocim.Alia.Application.Dtos;

public class PoolBancarioEstadosDto
{
    public long PoolId { get; set; }
    public string Cuenta { get; set; }
    public string Concepto { get; set; }
    public string Dispuesto { get; set; }
    public long? ContratoId { get; set; }
    public bool Estado { get; set; }
}
