namespace Tecnocim.Alia.Application.Dtos;

public class TotalBalanceSituacionDto
{
    public string Concepto { get; set; }
    public decimal ValorActual { get; set; }
    public decimal Tendencia { get; set; }
    public decimal ValorAnterior { get; set; }
    public decimal TendenciaAnterior { get; set; }
    public decimal ValorAnterior2 { get; set; }
}
