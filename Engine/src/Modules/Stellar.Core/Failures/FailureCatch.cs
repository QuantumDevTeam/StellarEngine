using Stellar.Kernel.Failures;

namespace Stellar.Core.Failures;

public class FailureCatch
    : IFailureCatch
{
    public void TryAction(Action action)
    {
        throw new NotImplementedException();
    }

    public void TryAction(Action<IFailureCatch> action)
    {
        throw new NotImplementedException();
    }

    public IFailureCatch Try(Action<IFailureCatch> action)
    {
        throw new NotImplementedException();
    }

    public IFailureCatch Try(Func<IFailureCatch> func)
    {
        throw new NotImplementedException();
    }

    public IFailureCatch Except<T>(Action<IFailureCatch, T> action) where T : Exception
    {
        throw new NotImplementedException();
    }

    public IFailureCatch Except<T>(Func<IFailureCatch, T> func) where T : Exception
    {
        throw new NotImplementedException();
    }

    public IFailureCatch Finally(Action<IFailureCatch> action)
    {
        throw new NotImplementedException();
    }

    public IFailureCatch Finally(Func<IFailureCatch> func)
    {
        throw new NotImplementedException();
    }

    public IFailureCatch Handle()
    {
        throw new NotImplementedException();
    }
}