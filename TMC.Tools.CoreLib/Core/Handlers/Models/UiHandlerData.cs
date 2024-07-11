namespace TMC.Tools.CoreLib.Core.Handlers.Models;

public class UiHandlerData
{
    /// <summary>
    /// The feature toggles associated with this handler. These should be in the order they are displayed on the UI.
    /// </summary>
    public List<FeatureToggle> OrderedFeatureToggles { get; set; }
    
    // /// <summary>
    // /// The tab pages associated with this handler. These should be in the order they are displayed on the UI.
    // /// </summary>
    // public List<TabPage> OrderedTabPages { get; set; }

    /// <summary>
    /// The preferred spot for this page to be inserted. If this windows preference is already taken it will be added
    /// immediately after the window that claimed it.
    ///
    /// Negative numbers will be placed after all windows with positive numbers. Negatives are inserted in ascending order.
    /// Leave this value at -1 to insert at the end of the list.
    /// </summary>
    public int PreferredInsertionIndex { get; set; } = -1;
}