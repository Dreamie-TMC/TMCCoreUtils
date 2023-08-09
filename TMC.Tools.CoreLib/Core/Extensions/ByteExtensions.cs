namespace TMC.Tools.CoreLib.Core.Extensions;

/// <summary>
/// Useful extensions to convert integer values into an array of bytes
/// </summary>
public static class ByteExtensions
{
    /// <summary>
    /// Converts an unsigned short into a byte array (little endian)
    /// </summary>
    public static byte[] UshortToByteArrayLE(this ushort value)
    {
        return new byte[]
        {
            (byte)(value & 0xFF),
            (byte)((value >> 8) & 0xFF),
        };
    }

    /// <summary>
    /// Converts an signed short into a byte array (little endian)
    /// </summary>
    public static byte[] ShortToByteArrayLE(this short value)
    {
        return new byte[]
        {
            (byte)(value & 0xFF),
            (byte)((value >> 8) & 0xFF),
        };
    }

    /// <summary>
    /// Converts an unsigned integer into a byte array (little endian)
    /// </summary>
    public static byte[] UintToByteArrayLE(this uint value)
    {
        return new byte[]
        {
            (byte)(value & 0xFF),
            (byte)((value >> 8) & 0xFF),
            (byte)((value >> 16) & 0xFF),
            (byte)((value >> 24) & 0xFF),
        };
    }

    /// <summary>
    /// Converts an signed integer into a byte array (little endian)
    /// </summary>
    public static byte[] IntToByteArrayLE(this int value)
    {
        return new byte[]
        {
            (byte)(value & 0xFF),
            (byte)((value >> 8) & 0xFF),
            (byte)((value >> 16) & 0xFF),
            (byte)((value >> 24) & 0xFF),
        };
    }
}