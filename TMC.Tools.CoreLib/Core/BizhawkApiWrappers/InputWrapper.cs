namespace TMC.Tools.CoreLib.Core.BizhawkApiWrappers;

public interface IInputWrapper
{
    IReadOnlyDictionary<string, object> GetMouse();
    IReadOnlyList<string> GetInputs();
}

public class InputWrapper : IInputWrapper
{
    internal ApiContainerWrapper ApiContainerWrapper { get; set; }
    
    public InputWrapper(IApiContainerWrapper containerWrapper)
    {
        ApiContainerWrapper = (ApiContainerWrapper)containerWrapper;
    }

    public IReadOnlyDictionary<string, object> GetMouse() => ApiContainerWrapper.CurrentContainer.Input.GetMouse();
    public IReadOnlyList<string> GetInputs() => ApiContainerWrapper.CurrentContainer.Input.GetPressedButtons();
}