namespace Hirundo.Processors.Specimens.WPF;

public class SpecimensModel
{
    public SpecimensParameters SpecimensProcessorParameters
    {
        get => new()
        {
            SpecimenIdentifier = SpecimenIdentifier,
            IncludeEmptyValues = IncludeEmptyValues
        };
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            SpecimenIdentifier = value.SpecimenIdentifier;
            IncludeEmptyValues = value.IncludeEmptyValues;
        }
    }

    public string SpecimenIdentifier { get; set; } = null!;
    public bool IncludeEmptyValues { get; set; }
}