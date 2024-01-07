﻿using Hirundo.Commons;
using NUnit.Framework;

namespace Hirundo.Filters.Observations.Tests;

[TestFixture]
public class SkipValueFilterTests
{
    [Test]
    public void GivenOtherValue_WhenIsSelect_ReturnsTrue()
    {
        // Arrange
        var valueName = "SEX";
        var value = "F";

        var filter = new SkipValueFilter(valueName, value);

        var observation = new Observation(["SEX"], ["M"]);

        // Act
        var result = filter.IsSelected(observation);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void GivenValue_WhenIsSelect_ReturnsFalse()
    {
        // Arrange
        var valueName = "SPECIES";
        var value = "REG.REG";

        var filter = new SkipValueFilter(valueName, value);

        var observation = new Observation(["SPECIES"], ["REG.REG"]);

        // Act
        var result = filter.IsSelected(observation);

        // Assert
        Assert.That(result, Is.False);
    }
}