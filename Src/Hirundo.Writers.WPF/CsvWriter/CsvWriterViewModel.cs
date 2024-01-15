namespace Hirundo.Writers.WPF.CsvWriter;

public class CsvWriterViewModel(CsvWriterModel model) : DataWriterViewModel
{
    public string Path
    {
        get => model.Path;
        set
        {
            model.Path = value;
            OnPropertyChanged();
        }
    }
}