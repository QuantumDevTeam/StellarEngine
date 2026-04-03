using Stellar.Kernel.Failures;

namespace Stellar.Core.Failures;

public class Catch : ICatch
{
    public void TryAction(Action action)
    {
        throw new NotImplementedException();
    }

    public void TryAction(Action<ICatch> action)
    {
        throw new NotImplementedException();
    }

    public ICatch Try(Action<ICatch> action)
    {
        throw new NotImplementedException();
    }

    public ICatch Try(Func<ICatch> func)
    {
        throw new NotImplementedException();
    }

    public ICatch Except<T>(Action<ICatch, T> action) where T : Exception
    {
        throw new NotImplementedException();
    }

    public ICatch Except<T>(Func<ICatch, T> func) where T : Exception
    {
        throw new NotImplementedException();
    }

    public ICatch Finally(Action<ICatch> action)
    {
        throw new NotImplementedException();
    }

    public ICatch Finally(Func<ICatch> func)
    {
        throw new NotImplementedException();
    }

    public ICatch Handle()
    {
        throw new NotImplementedException();
    }
}