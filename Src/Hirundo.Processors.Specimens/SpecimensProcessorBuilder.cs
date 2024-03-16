namespace Hirundo.Processors.Specimens;

public class SpecimensProcessorBuilder : ISpecimensProcessorBuilder
{
    private SpecimensProcessorParameters _parameters = null!;
    private CancellationToken? _cancellationToken;

    public ISpecimensProcessorBuilder WithSpecimensParameters(SpecimensProcessorParameters parameters)
    {
        _parameters = parameters;
        return this;
    }

    public ISpecimensProcessorBuilder WithCancellationToken(CancellationToken? cancellationToken)
    {
        _cancellationToken = cancellationToken;
        return this;
    }

    public ISpecimensProcessor Build()
    {
        return new SpecimensProcessor(_parameters, _cancellationToken);
    }

    public ISpecimensProcessorBuilder NewBuilder()
    {
        return new SpecimensProcessorBuilder();
    }
}