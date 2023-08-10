using BizHawk.Client.Common;

namespace TMC.Tools.CoreLib.Core.BizhawkApiWrappers;

/// <summary>
/// A wrapper for Bizhawk's ApiContainer meant to be used with DI.
/// This class should be used in place of ApiContainer in code if you are using DI. This allows you to only update
/// the ApiContainer reference in this class to have it updated everywhere.
/// </summary>
public interface IApiContainerWrapper
{
    /// <summary>
    /// Refreshes the API container that this wrapper holds.
    /// This should be the first thing called in your tool's "Restart" method.
    /// </summary>
    /// <param name="apiContainer"></param>
    void Restart(ApiContainer apiContainer);
}

public class ApiContainerWrapper : IApiContainerWrapper
{
    public ApiContainer CurrentContainer { get; set; }

    public ApiContainerWrapper(ApiContainer apiContainer)
    {
        CurrentContainer = apiContainer;
    }

    public void Restart(ApiContainer apiContainer)
    {
        CurrentContainer = apiContainer;
    }
}