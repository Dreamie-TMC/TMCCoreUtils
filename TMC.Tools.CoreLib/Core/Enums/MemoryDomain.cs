namespace TMC.Tools.CoreLib.Core.Enums;

/// <summary>
/// The memory domain to read from. Meant for use with <code>MemoryAccessor</code>
/// </summary>
public enum MemoryDomain
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable IdentifierTypo
    IWRAM = 0,
    EWRAM = 1,
    BIOS = 2,
    PALRAM = 3,
    VRAM = 4,
    OAM = 5,
    ROM = 6,
    SRAM = 7,
    CombinedWRAM = 8,
    SystemBus = 9,
}