namespace Hirundo.Processors.Specimens;

public interface ISpecimensProcessorBuilder
{
    ISpecimensProcessorBuilder WithSpecimensParameters(SpecimensParameters parameters);
    ISpecimensProcessorBuilder WithCancellationToken(CancellationToken? cancellationToken);
    ISpecimensProcessor Build();

    /// <summary>
    ///     Tworzy nowego Budowniczego tego samego typu.
    /// </summary>
    /// <returns></returns>
    ISpecimensProcessorBuilder NewBuilder();
}