using Tecnocim.Alia.Application.Dtos;

namespace Tecnocim.Alia.Domain.Extensions;

public static class RatioMaestroExtensions
{
    public static RatioInterpretacionDto ToRatioInterpretacionDto(this Interpretacion interpretacion, decimal tendencia, decimal tendenciaAnterior)
    {
        return new RatioInterpretacionDto
        {
            Concepto = interpretacion.Concepto,
            Nombre = interpretacion.Nombre,
            ColorPositivo = interpretacion.ColorPositivo,
            ColorNegativo = interpretacion.ColorNegativo,
            IconoPositivo = interpretacion.IconoPositivo,
            IconoNegativo = interpretacion.IconoNegativo,
            TendenciaColor = tendencia > 0m ? interpretacion.ColorPositivo : (tendencia < 0m ? interpretacion.ColorNegativo : string.Empty),
            TendenciaIcono = tendencia > 0m ? interpretacion.IconoPositivo : (tendencia < 0m ? interpretacion.IconoNegativo : string.Empty),
            TendenciaAnteriorColor = tendenciaAnterior > 0m ? interpretacion.ColorPositivo : (tendenciaAnterior < 0m ? interpretacion.ColorNegativo : string.Empty),
            TendenciaAnteriorIcono = tendenciaAnterior > 0m ? interpretacion.IconoPositivo : (tendenciaAnterior < 0m ? interpretacion.IconoNegativo : string.Empty),
            Tipo = (int)interpretacion.Tipo
        };
    }
}
