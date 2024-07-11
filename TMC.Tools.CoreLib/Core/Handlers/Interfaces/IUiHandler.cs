using System.Windows.Forms;
using TMC.Tools.CoreLib.Core.Handlers.Models;

namespace TMC.Tools.CoreLib.Core.Handlers.Interfaces;

/// <summary>
/// This class should be inherited by the representative interface for any UI Handler in tools
///
/// Individual handlers should not inherit this class directly but instead inherit their interface which inherits this
/// </summary>
public interface IUiHandler
{
    /// <summary>
    /// Gets the data associated with this handler, see <code>UiHandlerData</code> for more details.
    /// </summary>
    /// <returns></returns>
    UiHandlerData GetHandlerData();

    /// <summary>
    /// This function is called in ToolFormBase.UpdateAfter on each frame
    /// </summary>
    void Update();

    /// <summary>
    /// This function is called on core reboot
    /// </summary>
    void Restart();

    /// <summary>
    /// This function is called when the window is closed. This should be used to clean up any lingering data or UI elements.
    /// </summary>
    void Close();
}