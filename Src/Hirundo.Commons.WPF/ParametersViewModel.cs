using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hirundo.Commons.Repositories;
using System.ComponentModel;
using System.Windows.Input;

namespace Hirundo.Commons.WPF;

public abstract class ParametersViewModel(ParametersModel model) : ObservableObject, IRemovable
{
    private readonly ParametersModel model = model;
    public string Type { get; set; } = null!;
    public virtual string Name { get; set; } = string.Empty;
    public virtual string Description { get; set; } = string.Empty;
    public virtual string RemoveText => "Usuń warunek";
    public virtual string Explanation => model.GetExplanation();
    public virtual ILabelsRepository LabelsRepository => model.LabelsRepository;
    public virtual ISpeciesRepository SpeciesRepository => model.SpeciesRepository;

    public event EventHandler<ParametersEventArgs>? Removed;
    public virtual ICommand RemoveCommand => new RelayCommand(() => Remove(model.Parameters));

    protected virtual void Remove(object condition)
    {
        Removed?.Invoke(this, new ParametersEventArgs(condition));
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.PropertyName != nameof(Explanation))
        {
            OnPropertyChanged(nameof(Explanation));
        }
    }
}