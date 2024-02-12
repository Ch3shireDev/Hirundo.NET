namespace Hirundo.Processors.Specimens;

public class SpecimensProcessorBuilder : ISpecimensProcessorBuilder
{
    private SpecimensProcessorParameters _parameters = null!;

    public ISpecimensProcessorBuilder WithSpecimensParameters(SpecimensProcessorParameters parameters)
    {
        _parameters = parameters;
        return this;
    }

    public ISpecimensProcessor Build()
    {
        return new SpecimensProcessor(_parameters);
    }
}