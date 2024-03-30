using Serilog;

namespace Hirundo.Processors.Specimens;

public class SpecimensProcessorBuilder : ISpecimensProcessorBuilder
{
    private SpecimensParameters _parameters = null!;
    private CancellationToken? _cancellationToken;

    public ISpecimensProcessorBuilder WithSpecimensParameters(SpecimensParameters parameters)
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
        Log.Information("Budowanie procesora osobników.");
        return new SpecimensProcessor(_parameters, _cancellationToken);
    }

    public ISpecimensProcessorBuilder NewBuilder()
    {
        return new SpecimensProcessorBuilder();
    }
}