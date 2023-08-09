using System.Reflection;
using BizHawk.Emulation.Common;
using TMC.Tools.CoreLib.Core.Enums;
using TMC.Tools.CoreLib.Core.Extensions;
using MemoryDomain = TMC.Tools.CoreLib.Core.Enums.MemoryDomain;

namespace TMC.Tools.CoreLib.Core.BizhawkApiWrappers;

/// <summary>
/// A tool used to abstract away reading and writing memory in tools.
/// </summary>
public interface IMemoryAccessor
{
    /// <summary>
    /// Updates this control, unblocking memory if it is initialized.
    /// This should be called in your tool's "UpdateAfter" method.
    /// </summary>
    void Update();

    /// <summary>
    /// Restarts the control, blocking access to memory until it is verified to be available.
    /// This should be called in your tool's "Restart" method.
    /// </summary>
    /// <param name="forceBlock">True to forcibly block memory for a frame even if memory is accessible, false otherwise</param>
    void Restart(bool forceBlock);

    /// <summary>
    /// Loads a region of memory and returns it as a list of bytes.
    /// </summary>
    /// <param name="address">The memory address to begin reading from.</param>
    /// <param name="length">The length of data you want to read.</param>
    /// <param name="domain">The memory domain to read from</param>
    /// <returns>The bytes read from the given address, or an empty list if memory is blocked.</returns>
    IReadOnlyList<byte> LoadMemoryRegionAsListOfBytesFromAddress(long address, int length, MemoryDomain domain);

    /// <summary>
    /// Writes a list of bytes to a target address in memory.
    /// </summary>
    /// <param name="address">The address to write the first byte to</param>
    /// <param name="domain">The memory domain to write to</param>
    /// <param name="bytes">The list of bytes you want to write to the given address</param>
    /// <returns>True if the value was written successfully, false otherwise</returns>
    bool WriteBytesToAddress(long address, MemoryDomain domain, List<byte> bytes);

    /// <summary>
    /// Reads the data at a given address as a string
    /// </summary>
    /// <param name="address">The address to read data from</param>
    /// <param name="length">The length of data to read</param>
    /// <param name="domain">The memory domain to read from</param>
    /// <returns>The string parsed from the given address, or empty if memory could not be read</returns>
    string ReadStringFromAddress(long address, int length, MemoryDomain domain);

    /// <summary>
    /// Writes a value to the given address
    /// </summary>
    /// <param name="targetAddress">The address you want to write to</param>
    /// <param name="value">The value you want to write to the given address</param>
    /// <param name="type">The size of the data being written to memory</param>
    /// <param name="domain">The memory domain to write to</param>
    /// <returns>True if the value was written successfully, false otherwise</returns>
    /// <throws>@ArgumentOutOfRangeException if an invalid type is passed in</throws>
    bool WriteValueToAddress(long targetAddress, long value, ReadWriteType type, MemoryDomain domain);

    /// <summary>
    /// Reads a value of the given size from the given address
    /// </summary>
    /// <param name="address">The address to read data from</param>
    /// <param name="type">The size of the data that is being read</param>
    /// <param name="domain">The memory domain to read the data from</param>
    /// <returns>The value read from the given address, or 0 if the value could not be read</returns>
    uint LoadValueFromAddress(long address, ReadWriteType type, MemoryDomain domain);

    /// <summary>
    /// Reads the current area and room that Link is in
    /// </summary>
    /// <returns>The current area and room, or (255, 255) if the value could not be read</returns>
    (uint area, uint room) LoadCurrentAreaAndRoom();
}

public class MemoryAccessor : IMemoryAccessor
{
    internal ApiContainerWrapper ApiContainerWrapper { get; set; }
    internal ClientHelper ClientHelper { get; set; }
    internal bool BlockCallsToMemory { get; set; }

    public MemoryAccessor(ApiContainerWrapper containerWrapper, ClientHelper clientHelper)
    {
        ApiContainerWrapper = containerWrapper;
        ClientHelper = clientHelper;
    }

    public void Update()
    {
        BlockCallsToMemory = ClientHelper.FrameCount() == 0;
    }

    public void Restart(bool forceBlock = false)
    {
        if (forceBlock)
        {
            BlockCallsToMemory = true;
            return;
        }
        
        //This code is complete garbage but it prevents issues
        var emulator = (IEmulator)ApiContainerWrapper.CurrentContainer.Memory.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic).First(field => field.FieldType == typeof(IEmulator)).GetValue(ApiContainerWrapper.CurrentContainer.Memory)!;
        var core = (IntPtr)emulator.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic).First(field => field.FieldType == typeof(IntPtr)).GetValue(emulator)!;
        if (core == IntPtr.Zero)
            BlockCallsToMemory = true;
    }

    public IReadOnlyList<byte> LoadMemoryRegionAsListOfBytesFromAddress(long address, int length, MemoryDomain domain)
    {
        return BlockCallsToMemory ? new List<byte>() : ApiContainerWrapper.CurrentContainer.Memory.ReadByteRange(address, length, domain.GetDomainAsString());
    }

    public bool WriteBytesToAddress(long address, MemoryDomain domain, List<byte> bytes)
    {
        if (BlockCallsToMemory) return false;
        
        ApiContainerWrapper.CurrentContainer.Memory.WriteByteRange(address, bytes, domain.GetDomainAsString());
        return true;
    }
    
    public string ReadStringFromAddress(long address, int length, MemoryDomain domain)
    {
        return BlockCallsToMemory ? "" : System.Text.Encoding.ASCII.GetString(ApiContainerWrapper.CurrentContainer.Memory.ReadByteRange(address, length, domain.GetDomainAsString()).ToArray());
    }

    public bool WriteValueToAddress(long targetAddress, long value, ReadWriteType type, MemoryDomain domain)
    {
        if (BlockCallsToMemory) return false;
        
        var dom = domain.GetDomainAsString();

        switch (type)
        {
            case ReadWriteType.Byte:
                ApiContainerWrapper.CurrentContainer.Memory.WriteByte(targetAddress, (uint)value, dom);
                break;
            case ReadWriteType.Signed8:
                ApiContainerWrapper.CurrentContainer.Memory.WriteS8(targetAddress, (int)value, dom);
                break;
            case ReadWriteType.Signed16:
                ApiContainerWrapper.CurrentContainer.Memory.WriteS16(targetAddress, (int)value, dom);
                break;
            case ReadWriteType.Signed24:
                ApiContainerWrapper.CurrentContainer.Memory.WriteS24(targetAddress, (int)value, dom);
                break;
            case ReadWriteType.Signed32:
                ApiContainerWrapper.CurrentContainer.Memory.WriteS32(targetAddress, (int)value, dom);
                break;
            case ReadWriteType.Unsigned8:
                ApiContainerWrapper.CurrentContainer.Memory.WriteU8(targetAddress, (uint)value, dom);
                break;
            case ReadWriteType.Unsigned16:
                ApiContainerWrapper.CurrentContainer.Memory.WriteU16(targetAddress, (uint)value, dom);
                break;
            case ReadWriteType.Unsigned24:
                ApiContainerWrapper.CurrentContainer.Memory.WriteU24(targetAddress, (uint)value, dom);
                break;
            case ReadWriteType.Unsigned32:
                ApiContainerWrapper.CurrentContainer.Memory.WriteU32(targetAddress, (uint)value, dom);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        return true;
    }

    public uint LoadValueFromAddress(long address, ReadWriteType type, MemoryDomain domain)
    {
        if (BlockCallsToMemory) return 0;
        
        var dom = domain.GetDomainAsString();

        return type switch
        {
            ReadWriteType.Byte => ApiContainerWrapper.CurrentContainer.Memory.ReadByte(address, dom),
            ReadWriteType.Signed8 => (uint)ApiContainerWrapper.CurrentContainer.Memory.ReadS8(address, dom),
            ReadWriteType.Signed16 => (uint)ApiContainerWrapper.CurrentContainer.Memory.ReadS16(address, dom),
            ReadWriteType.Signed24 => (uint)ApiContainerWrapper.CurrentContainer.Memory.ReadS24(address, dom),
            ReadWriteType.Signed32 => (uint)ApiContainerWrapper.CurrentContainer.Memory.ReadS32(address, dom),
            ReadWriteType.Unsigned8 => ApiContainerWrapper.CurrentContainer.Memory.ReadU8(address, dom),
            ReadWriteType.Unsigned16 => ApiContainerWrapper.CurrentContainer.Memory.ReadU16(address, dom),
            ReadWriteType.Unsigned24 => ApiContainerWrapper.CurrentContainer.Memory.ReadU24(address, dom),
            ReadWriteType.Unsigned32 => ApiContainerWrapper.CurrentContainer.Memory.ReadU32(address, dom),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    public (uint area, uint room) LoadCurrentAreaAndRoom()
    {
        return BlockCallsToMemory ? ((uint area, uint room))(255, 255) : (area: ApiContainerWrapper.CurrentContainer.Memory.ReadU8(0x0BF4, "IWRAM"), room: ApiContainerWrapper.CurrentContainer.Memory.ReadU8(0x0BF5, "IWRAM"));
    }
}