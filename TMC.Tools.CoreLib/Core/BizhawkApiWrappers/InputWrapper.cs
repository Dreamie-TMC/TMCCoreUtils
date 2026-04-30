namespace TMC.Tools.CoreLib.Core.BizhawkApiWrappers;

public interface IInputWrapper
{
    IReadOnlyDictionary<string, object> GetMouse();
    IReadOnlyList<string> GetInputs();
}

public class InputWrapper(IApiContainerWrapper containerWrapper) : IInputWrapper
{
    internal ApiContainerWrapper ApiContainerWrapper { get; set; } =
        (ApiContainerWrapper)containerWrapper;

    public IReadOnlyDictionary<string, object> GetMouse() =>
        ApiContainerWrapper.CurrentContainer.Input.GetMouse();

    public IReadOnlyList<string> GetInputs() =>
        ApiContainerWrapper.CurrentContainer.Input.GetPressedButtons();
}
