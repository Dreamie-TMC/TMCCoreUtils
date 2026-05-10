using System.Text.RegularExpressions;

namespace TMC.Tools.CoreLib.Core.Extensions;

public static class UintExtensions
{
    public static string ConvertToBinary(this uint value, int len)
    {
        return Regex.Replace(Convert.ToString(value, 2).PadLeft(len, '0'), ".{4}(?!$)", "$0 ");
    }

    public static string ToFixedPointHalfWordLE(this uint value, bool useHex)
    {
        return useHex
            ? $"{(value >> 8) & 0xFF:X}.{value & 0xFF:X}"
            : $"{(value >> 8) & 0xFF}.{value & 0xFF}";
    }

    public static string ToFixedPointWordLE(this uint value, bool useHex)
    {
        return useHex
            ? $"{((value & 0xFF0000) | (value & 0xFF000000)) >> 16:X}.{(value & 0xFF) | (value & 0xFF00):X}"
            : $"{((value & 0xFF0000) | (value & 0xFF000000)) >> 16}.{(value & 0xFF) | (value & 0xFF00)}";
    }
}
