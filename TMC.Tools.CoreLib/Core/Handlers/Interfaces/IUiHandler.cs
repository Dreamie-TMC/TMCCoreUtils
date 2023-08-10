using System.Windows.Forms;

namespace TMC.Tools.CoreLib.Core.Handlers.Interfaces;

/// <summary>
/// This class should be inherited by the representative interface for any UI Handler in tools
///
/// Individual handlers should not inherit this class directly but instead inherit their interface which inherits this
/// </summary>
public interface IUiHandler
{
    /// <summary>
    /// Gets the tab page for this handler. Each handler should only return one tab page
    /// If a given handler has multiple tab pages then the handler should be split along those pages
    /// </summary>
    TabPage GetUiPageForHandler();

    /// <summary>
    /// This function is called in ToolFormBase.UpdateAfter on each frame
    /// </summary>
    void Update();

    /// <summary>
    /// This function is called on core reboot
    /// </summary>
    void Restart();
}