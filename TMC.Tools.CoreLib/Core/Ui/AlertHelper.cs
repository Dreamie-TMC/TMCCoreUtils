using System.Windows.Forms;

namespace TMC.Tools.CoreLib.Core.Ui;

public static class AlertHelper
{
    public static void DisplayAlert(
        string text,
        string caption,
        MessageBoxButtons buttons,
        MessageBoxIcon icon,
        DialogResult? callbackExecuteOnResult = null,
        Action? callback = null)
    {
        var result = MessageBox.Show(text, caption, buttons, icon);

        if ((callbackExecuteOnResult == null && callback != null) ||
            (callbackExecuteOnResult != null && callback != null && callbackExecuteOnResult == result))
        {
            callback.Invoke();
        }
    }
}