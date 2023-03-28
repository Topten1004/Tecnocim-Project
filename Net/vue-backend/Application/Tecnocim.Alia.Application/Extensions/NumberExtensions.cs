using System.Globalization;

namespace Tecnocim.Alia.Application.Extensions
{
    public static class NumberExtensions
    {
        public static string ToTwoDecimalAndSymbolFormat(this decimal value, char numberType)
        {
            var culture = CultureInfo.CreateSpecificCulture("es-ES");

            var formatInfo = (NumberFormatInfo)culture.NumberFormat.Clone();
            formatInfo.CurrencyGroupSeparator = string.Empty;
            formatInfo.NumberGroupSeparator = string.Empty;
            formatInfo.PercentGroupSeparator = string.Empty;
            formatInfo.CurrencyDecimalSeparator = ".";
            formatInfo.NumberDecimalSeparator = ".";
            formatInfo.PercentDecimalSeparator = ".";
            formatInfo.PercentDecimalDigits = 2;

            if (string.Compare("c", numberType.ToString(), CultureInfo.InvariantCulture, CompareOptions.IgnoreCase) == 0)
            {
                return decimal.Round(value, 2, MidpointRounding.AwayFromZero).ToString(formatInfo);
            }

            if (string.Compare("p", numberType.ToString(), CultureInfo.InvariantCulture, CompareOptions.IgnoreCase) == 0)
            {
                return $"{decimal.Round(value, 2, MidpointRounding.AwayFromZero).ToString(formatInfo)} %";
            }

            return decimal.Round(value, 2, MidpointRounding.AwayFromZero).ToString(formatInfo);
        }
    }
}
