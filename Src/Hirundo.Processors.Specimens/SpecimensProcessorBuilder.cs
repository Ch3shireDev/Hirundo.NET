﻿namespace Hirundo.Processors.Specimens;

public class SpecimensProcessorBuilder
{
    private SpecimensProcessorParameters _parameters = null!;

    public SpecimensProcessorBuilder WithSpecimensProcessorParameters(SpecimensProcessorParameters parameters)
    {
        _parameters = parameters;
        return this;
    }

    public SpecimensProcessor Build()
    {
        return new SpecimensProcessor(_parameters);
    }
}