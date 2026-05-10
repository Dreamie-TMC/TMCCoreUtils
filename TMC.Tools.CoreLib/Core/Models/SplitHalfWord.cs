namespace TMC.Tools.CoreLib.Core.Models;

public class SplitHalfWord(IReadOnlyList<byte> bytes)
{
    public static bool operator ==(SplitHalfWord shw1, SplitHalfWord shw2)
    {
        return shw1.Equals(shw2);
    }

    public static bool operator !=(SplitHalfWord shw1, SplitHalfWord shw2)
    {
        return !(shw1 == shw2);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not SplitHalfWord shw)
            return false;

        return LowerByte == shw.LowerByte && HigherByte == shw.HigherByte;
    }

    public byte LowerByte { get; set; } = bytes[0]; /* First byte */
    public byte HigherByte { get; set; } = bytes[1]; /* Second byte */

    public void Update(IReadOnlyList<byte> bytes)
    {
        if (LowerByte == bytes[0] && HigherByte == bytes[1])
            return;

        LowerByte = bytes[0];
        HigherByte = bytes[1];
    }

    public List<byte> ToByteArray()
    {
        return new List<byte> { LowerByte, HigherByte };
    }
}
