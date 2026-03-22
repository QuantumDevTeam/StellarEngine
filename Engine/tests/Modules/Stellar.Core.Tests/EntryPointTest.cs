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
    public void Constructor_WithNullConfig_ShouldThrow()
    {
        // Act
        var action = () => new Stellar.Core.EntryPoint(null!);

        // Assert
        action.Should().Throw<System.ArgumentNullException>(
            "RuntimeConfiguration parameter must not be null."
        );
    }
}