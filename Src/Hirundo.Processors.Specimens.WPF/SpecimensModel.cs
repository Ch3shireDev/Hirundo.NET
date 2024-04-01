namespace Hirundo.Processors.Specimens.WPF;

public class SpecimensModel
{
    public SpecimensParameters SpecimensProcessorParameters
    {
        get => new()
        {
            SpecimenIdentifier = SpecimenIdentifier,
        };
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            SpecimenIdentifier = value.SpecimenIdentifier;
        }
    }

    public string SpecimenIdentifier { get; set; } = null!;
}