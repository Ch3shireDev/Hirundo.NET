namespace Hirundo.Processors.Specimens;

public interface ISpecimensProcessorBuilder
{
    ISpecimensProcessorBuilder WithSpecimensParameters(SpecimensProcessorParameters parameters);
    ISpecimensProcessor Build();
}