using TMC.Tools.CoreLib.Core.Extensions;

namespace TMC.Tools.CoreLib.Core.Models;

public class SplitWord(IReadOnlyList<byte> bytes)
{
    public static bool operator ==(SplitWord sw1, SplitWord sw2)
    {
        return sw1.Equals(sw2);
    }

    public static bool operator !=(SplitWord sw1, SplitWord sw2)
    {
        return !(sw1 == sw2);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not SplitWord sw)
            return false;

        return LowerBytes == sw.LowerBytes && HigherBytes == sw.HigherBytes;
    }

    public ushort LowerBytes { get; set; } = bytes.BytesToUnsignedShortLE(0); /* First 2 bytes */
    public ushort HigherBytes { get; set; } = bytes.BytesToUnsignedShortLE(2); /* Last 2 bytes */

    public void Update(IReadOnlyList<byte> bytes)
    {
        var lb = bytes.BytesToUnsignedShortLE(0);
        var hb = bytes.BytesToUnsignedShortLE(2);

        LowerBytes = lb;
        HigherBytes = hb;
    }

    public List<byte> ToByteArray()
    {
        var list = new List<byte>();

        list.AddRange(LowerBytes.UshortToByteArrayLE());
        list.AddRange(HigherBytes.UshortToByteArrayLE());

        return list;
    }
}
