using Tecnocim.Alia.Application;
using Tecnocim.Alia.Application.Dtos;

namespace Tecnocim.Alia.Domain.Extensions
{
    public static class DocumentExtensions
    {
        public static TotalRatiosDto GetTotalRatiosByConcepto(this IEnumerable<Documento> documentos, int anualidad, string concepto, bool extrapolar = true)
        {
            var documentTotalActual = GetDocumentoForTotals(documentos, anualidad, concepto);

            var magnitud = documentTotalActual?.Ratios?.FirstOrDefault(x => x.Concepto.ToLowerInvariant() == concepto)?.Magnitud ?? 0;

            var totalActual = extrapolar
                    ? documentTotalActual != null ? decimal.Round(magnitud * 365 / documentTotalActual.Fecha.DayOfYear, 2, MidpointRounding.AwayFromZero) : 0
                    : magnitud;

            var documentTotalAnterior = GetDocumentoForTotals(documentos, anualidad - 1, concepto);

            var totalAnterior = documentTotalAnterior?.Ratios?.FirstOrDefault(x => x.Concepto.ToLowerInvariant() == concepto)?.Magnitud ?? 0;

            var tendencia = totalAnterior != 0 ? ((totalActual - totalAnterior) / totalAnterior) * 100 : 0;

            var documentTotalAnterior2 = GetDocumentoForTotals(documentos, anualidad - 2, concepto);

            var totalAnterior2 = documentTotalAnterior2?.Ratios?.FirstOrDefault(x => x.Concepto.ToLowerInvariant() == concepto)?.Magnitud ?? 0;

            var tendenciaAnterior = totalAnterior2 != 0 ? ((totalAnterior - totalAnterior2) / totalAnterior2) * 100 : 0;

            return new TotalRatiosDto
            {
                TotalActual = decimal.Round(totalActual, 2, MidpointRounding.AwayFromZero),
                TotalAnterior = decimal.Round(totalAnterior, 2, MidpointRounding.AwayFromZero),
                TotalAnterior2 = decimal.Round(totalAnterior2, 2, MidpointRounding.AwayFromZero),
                Tendencia = decimal.Round(tendencia, 2, MidpointRounding.AwayFromZero),
                TendenciaAnterior = decimal.Round(tendenciaAnterior, 2, MidpointRounding.AwayFromZero)
            };
        }

        private static Documento? GetDocumentoForTotals(IEnumerable<Documento> documentos, int anualidad, string conceptoRatio)
        {
            List<string> origenes = new() { Origen.BSS.ToString(), Origen.Modelo200.ToString() };

            var documents = documentos.Where(x => origenes.Contains(x.Origen) && x.Fecha.Year == anualidad
                                && x.Ratios.Any(r => r.Concepto.ToLowerInvariant() == conceptoRatio)).OrderByDescending(x => x.Fecha);

            var document = documents.FirstOrDefault();

            if (documents.Any() && documents.Count() > 1)
            {
                var firstDocument = documents.First();
                var secondDocument = documents.Skip(1).First();

                if (firstDocument.Fecha == secondDocument.Fecha
                    && ((firstDocument.Origen == Origen.BSS.ToString() && secondDocument.Origen == Origen.Modelo200.ToString())
                    || (firstDocument.Origen == Origen.Modelo200.ToString() && secondDocument.Origen == Origen.BSS.ToString())))
                {
                    document = firstDocument.Origen == Origen.Modelo200.ToString() ? firstDocument : secondDocument;
                }
            }

            return document;
        }
    }
}
