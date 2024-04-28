using Serilog;

namespace Hirundo.Commons.Helpers;

public static class FileHelpers
{
    public static IFileDialogHelper File { get; set; } = new FileDialogHelper();

    public static string GetUniqueFilename(string fileName, string directory)
    {
        try
        {
            var filepath = Path.Combine(directory, fileName);

            if (!File.Exists(filepath))
            {
                return fileName;
            }

            var newFilename = fileName;

            var i = 1;

            while (File.Exists(filepath))
            {
                newFilename = $"{Path.GetFileNameWithoutExtension(fileName)} ({i}){Path.GetExtension(fileName)}";
                filepath = Path.Combine(directory, newFilename);
                i += 1;
            }

            return newFilename;
        }
#pragma warning disable CA1031
        catch (Exception ex)
#pragma warning restore CA1031
        {
            Log.Warning("Błąd podczas tworzenia nazwy pliku: {ex}.", ex.Message);
            return fileName;
        }
    }
}