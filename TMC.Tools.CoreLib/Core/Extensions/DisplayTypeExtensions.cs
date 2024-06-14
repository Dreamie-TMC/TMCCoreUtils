using TMC.Tools.CoreLib.Core.Enums;

namespace TMC.Tools.CoreLib.Core.Extensions;

public static class DisplayTypeExtensions
{
    public static string ConvertTypeToString(this DisplayType type) => type switch
    {
        DisplayType.Binary => "Binary",
        DisplayType.Byte => "Byte",
        DisplayType.HalfWord => "2 Bytes",
        DisplayType.Word => "4 Bytes",
        DisplayType.ByteArray => "Byte Array",
        DisplayType.FixedPointHalfWord => "Fixed (2 Bytes)",
        DisplayType.FixedPointWord => "Fixed (4 Bytes)",
        DisplayType.String => "String",
        _ => "Invalid Type"
    };
}