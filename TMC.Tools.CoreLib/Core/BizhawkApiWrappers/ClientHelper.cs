namespace TMC.Tools.CoreLib.Core.BizhawkApiWrappers;

/// <summary>
/// A wrapper for Bizhawk's EmuClient to abstract away calls to the client
/// </summary>
public interface IClientHelper
{
    /// <summary>
    /// Initializes a core reboot.
    /// </summary>
    void CoreReboot();

    /// <summary>
    /// Checks if the emulator is currently paused or not.
    /// </summary>
    /// <returns>True if paused, false if not.</returns>
    bool IsPaused();

    /// <summary>
    /// Returns the current frame count of the emulator.
    /// </summary>
    /// <returns>The current frame count</returns>
    int FrameCount();
}

/// <summary>
/// This class takes all services that directly access memory in order to lock them during core reboots
/// </summary>
public class ClientHelper : IClientHelper
{
    internal ApiContainerWrapper ApiContainerWrapper { get; set; }
    
    public ClientHelper(ApiContainerWrapper containerWrapper)
    {
        ApiContainerWrapper = containerWrapper;
    }

    public void CoreReboot()
    {
        ApiContainerWrapper.CurrentContainer.EmuClient.RebootCore();
    }

    public bool IsPaused() => ApiContainerWrapper.CurrentContainer.EmuClient.IsPaused();

    public int FrameCount() => ApiContainerWrapper.CurrentContainer.Emulation.FrameCount();
}