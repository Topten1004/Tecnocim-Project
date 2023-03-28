namespace Tecnocim.Alia.Application.Dtos;

public class TotalBalanceSituacionStringDto
{
    public string Concepto { get; set; }
    public string ValorActual { get; set; }
    public string Tendencia { get; set; }
    public string ValorAnterior { get; set; }
    public string TendenciaAnterior { get; set; }
    public string ValorAnterior2 { get; set; }
    public string Divisa { get; set; }
    public ContabilidadConfiguracionDto Configuracion { get; set; }
}

public class ContabilidadConfiguracionDto
{
    public string Grupo { get; set; }
    public int Prioridad { get; set; }
    public string Etiqueta { get; set; }
}