using Moq;
using Xunit;
using BrokenWheel.Core.Logging;
using System;

namespace BrokenWheel.Core.DependencyInjection.DI.Tests;

public class InitializeTests
{
    [Fact]
    public void InitializeTests_ThrowsNullArgumentException_WhenLoggerIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => Injection.Initialize(null));
    }

    [Fact]
    public void InitializeTests_LogsInitialization_WhenLoggerIsNotNull()
    {
        var logger = MoqLogger();
        Injection.Initialize(logger);
        Mock.Verify(Mock.Get(logger));
    }

    private static ILogger MoqLogger()
    {
        var logger = Mock.Of<ILogger>();
        Mock.Get(logger).Setup(_ => _.LogCategory(It.IsAny<string>(), It.IsAny<string>()));
        return logger;
    }
}
