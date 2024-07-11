namespace TMC.Tools.CoreLib.Core.Handlers.Models;

public class FeatureToggle
{
    public CheckBox? Toggle { get; set; }
    
    public Action<bool>? OnToggleCheckedAction { get; set; }
    
    public TabPage ToggledPage { get; set; }
}