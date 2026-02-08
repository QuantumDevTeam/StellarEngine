// ReSharper disable RedundantNameQualifier

namespace Stellar.Core.Tests;

public class EntryPointTest
{
    [Fact]
    public void EntryPoint_ShouldImplementStellarEntryPoint()
    {
        // Arrange & Act & Assert
        typeof(Stellar.Core.EntryPoint).Should().BeAssignableTo<Stellar.Kernel.EntryPoint.StellarEntryPoint>(
            "EntryPoint must be child of StellarEntryPoint from Kernel."
        );
    }

    [Fact]
    public void EntryPoint_ShouldImplementIDisposable()
    {
        typeof(Stellar.Core.EntryPoint).Should().BeAssignableTo<System.IDisposable>(
            "EntryPoint must be Disposable."
        );
    }

    [Fact]
    public void Constructor_WithInvalidConfig_ShouldThrow()
    {
        // Arrange
        var mockConfig = new Mock<Stellar.Core.Build.RuntimeConfig>();

        // Act
        var action = () => new Stellar.Core.EntryPoint(mockConfig.Object);

        // Assert
        action.Should().Throw<System.ArgumentException>(
            "EntryPoint must not process invalid configuration."
        );
    }

    [Fact]
    public void Constructor_WithNullConfig_ShouldThrow()
    {
        // Arrange
        Stellar.Core.Build.RuntimeConfig nullConfig = null!;

        // Act
        var action = () => new Stellar.Core.EntryPoint(nullConfig);

        // Assert
        action.Should().Throw<System.ArgumentNullException>(
            "RuntimeConfiguration parameter must not be null."
        );
    }

    [Fact]
    public void Run_ShouldReturnInteger()
    {
        // Arrange
        var mockConfig = new Mock<Stellar.Core.Build.RuntimeConfig>();
        var entryPoint = new Stellar.Core.EntryPoint(mockConfig.Object);

        // Act
        var result = entryPoint.Run();

        // Assert
        result.Should().BeOfType(typeof(int),
            "Run method should return exit code."
        );
    }

    [Fact]
    public void Dispose_ShouldNotThrow_WhenCalledMultipleTimes()
    {
        // Arrange
        var mockConfig = new Mock<Stellar.Core.Build.RuntimeConfig>();
        var entryPoint = new Stellar.Core.EntryPoint(mockConfig.Object);

        // Act
        System.Action act = () =>
        {
            entryPoint.Dispose();
            entryPoint.Dispose();
        };

        // Assert
        act.Should().NotThrow(
            "EntryPoint should be safety to dispose."
        );
    }
}