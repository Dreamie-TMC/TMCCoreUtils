namespace TMC.Tools.CoreLib.Core.Extensions;

/// <summary>
/// Extensions for List to convert to varying integers
/// </summary>
public static class ListExtensions
{
    /// <summary>
    /// Converts a list of bytes into an unsigned short (little endian)
    /// </summary>
    public static ushort BytesToUnsignedShortLE(this List<byte> bytes, int index)
    {
        if (index + 1 >= bytes.Count) throw new IndexOutOfRangeException();

        return (ushort)(bytes[index] + (bytes[index + 1] << 8));
    }
    
    /// <summary>
    /// Converts a list of bytes into a signed short (little endian)
    /// </summary>
    public static short BytesToSignedShortLE(this List<byte> bytes, int index)
    {
        if (index + 1 >= bytes.Count) throw new IndexOutOfRangeException();

        return (short)(bytes[index] + (bytes[index + 1] << 8));
    }
    
    /// <summary>
    /// Converts a list of bytes into a signed integer (little endian)
    /// </summary>
    public static int BytesToSignedIntLE(this List<byte> bytes, int index)
    {
        if (index + 3 >= bytes.Count) throw new IndexOutOfRangeException();

        return bytes[index] + (bytes[index + 1] << 8) + (bytes[index + 2] << 16) + (bytes[index + 3] << 24);
    }
    
    /// <summary>
    /// Converts a list of bytes into an unsigned integer (little endian)
    /// </summary>
    public static uint BytesToUnsignedIntLE(this List<byte> bytes, int index)
    {
        if (index + 3 >= bytes.Count) throw new IndexOutOfRangeException();

        return (uint)(bytes[index] + (bytes[index + 1] << 8) + (bytes[index + 2] << 16) + ((uint)bytes[index + 3] << 24));
    }
}