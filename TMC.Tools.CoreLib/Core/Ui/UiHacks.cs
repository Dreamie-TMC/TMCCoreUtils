using System.Reflection;

namespace TMC.Tools.CoreLib.Core.Ui;

public static class UiHacks
{
    public static void DoubleBuffering(this Control control, bool enable)
    {
        var method = typeof(Control).GetMethod("SetStyle", BindingFlags.Instance | BindingFlags.NonPublic);
        method.Invoke(control, new object[] { ControlStyles.OptimizedDoubleBuffer, enable });
    }
}