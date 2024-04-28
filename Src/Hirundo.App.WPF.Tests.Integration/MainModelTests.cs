using Autofac;
using Hirundo.App.WPF.Components;
using NUnit.Framework;

namespace Hirundo.App.WPF.Tests.Integration;

[TestFixture]
public class MainModelTests
{
    private MainModel model = null!;

    [SetUp]
    public void Initialize()
    {
        var builder = new ContainerBuilder();
        builder.AddViewModel();
        var container = builder.Build();

        model = container.Resolve<MainModel>();
    }
}
