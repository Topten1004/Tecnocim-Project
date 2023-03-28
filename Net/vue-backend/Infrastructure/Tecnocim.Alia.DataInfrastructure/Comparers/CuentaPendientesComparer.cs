using System.Collections;

namespace Tecnocim.Alia.DataInfrastructure.Comparers;

public class CuentaPendientesComparer : IComparer<string>, IComparer
{
    public int Compare(string a, string b)
    {
        if (a is null || b is null) return 0;

        // 17  23 => 17
        if (a.StartsWith("17") && !b.StartsWith("17")) return -1;

        // 52   17  => 17
        if (a.StartsWith("52") && b.StartsWith("17")) return 1;

        // 52  32 => 52
        if (a.StartsWith("52") && !b.StartsWith("52")) return -1;

        // 23  52  => 52
        if (!a.StartsWith("17") && b.StartsWith("52")) return 1;

        return a.CompareTo(b);
    }

    public int Compare(object? x, object? y)
    {
        throw new NotImplementedException();
    }
}
