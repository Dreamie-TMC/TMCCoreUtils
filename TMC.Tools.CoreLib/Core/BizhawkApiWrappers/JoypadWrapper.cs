namespace TMC.Tools.CoreLib.Core.BizhawkApiWrappers;

public interface IJoypadWrapper
{
    IReadOnlyDictionary<string, object> GetInputs(int? controllerNum = null);
    IReadOnlyDictionary<string, object> GetInputsWithMovie(int? controllerNum = null);

    void SetInputs(IReadOnlyDictionary<string, bool> inputs, int? controllerNum = null);

    IReadOnlyDictionary<string, object> GetInputsImmediate(int? controllerNum = null);
}

public class JoypadWrapper : IJoypadWrapper
{
    internal ApiContainerWrapper ApiContainerWrapper { get; set; }
    
    public JoypadWrapper(IApiContainerWrapper apiContainerWrapper)
    {
        ApiContainerWrapper = (ApiContainerWrapper)apiContainerWrapper;
    }

    public IReadOnlyDictionary<string, object> GetInputs(int? controllerNum = null) => ApiContainerWrapper.CurrentContainer.Joypad.Get(controllerNum);
    public IReadOnlyDictionary<string, object> GetInputsWithMovie(int? controllerNum = null) => ApiContainerWrapper.CurrentContainer.Joypad.GetWithMovie(controllerNum);
    public IReadOnlyDictionary<string, object> GetInputsImmediate(int? controllerNum = null) => ApiContainerWrapper.CurrentContainer.Joypad.GetImmediate(controllerNum);

    public void SetInputs(IReadOnlyDictionary<string, bool> inputs, int? controllerNum = null) => ApiContainerWrapper.CurrentContainer.Joypad.Set(inputs, controllerNum);
}