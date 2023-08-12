using System.Windows.Forms;

namespace TMC.Tools.CoreLib.Core.Ui;

public static class FileHelper
{
    public static void DisplayOpenDialog(string filter, string title, DialogResult expectedResult, Action<string> callback, string? initialDirectory = null)
    {
        var openDialog = new OpenFileDialog
        {
            Filter = filter,
            Title = title,
        };

        if (initialDirectory != null)
            openDialog.InitialDirectory = initialDirectory;
        
        var result = openDialog.ShowDialog();
        if (result != expectedResult) return;

        callback.Invoke(openDialog.FileName);
    }

    public static void DisplaySaveDialog(string filter, string title, string filename, DialogResult expectedResult, Action<string> callback, string? initialDirectory = null)
    {
        var saveFileDialog = new SaveFileDialog
        {
            Filter = filter,
            Title = title,
            FileName = filename,
        };
        
        if (initialDirectory != null)
            saveFileDialog.InitialDirectory = initialDirectory;

        var result = saveFileDialog.ShowDialog();

        if (result != expectedResult) return;

        callback.Invoke(saveFileDialog.FileName);
    }

    public static string BuildFileTypeFilter(string typeName, string typeExtension)
    {
        return $"{typeName}|{typeExtension}";
    }

    public static string AppendFileTypeToFilter(string filter, string typeName, string typeExtension)
    {
        return $"{filter}|{BuildFileTypeFilter(typeName, typeExtension)}";
    }
}