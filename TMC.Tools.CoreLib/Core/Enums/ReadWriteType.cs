namespace TMC.Tools.CoreLib.Core.Enums;

/// <summary>
/// Read/Write length enum meant for use with <code>MemoryAccessor</code>
/// </summary>
public enum ReadWriteType
{
    Byte = 0,
    Signed8 = 1,
    Signed16 = 2,
    Signed24 = 3,
    Signed32 = 4,
    Unsigned8 = 5,
    Unsigned16 = 6,
    Unsigned24 = 7,
    Unsigned32 = 8,
}