namespace Hirundo.Commons.Helpers;

public class FileDialogHelper : IFileDialogHelper
{
    public bool Exists(string filePath)
    {
        return File.Exists(filePath);
    }
}
