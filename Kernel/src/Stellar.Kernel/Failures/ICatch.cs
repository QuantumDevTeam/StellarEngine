using System;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Failures
{
    public interface ICatch : IQuantumObject
    {
        void TryAction(Action action);
        void TryAction(Action<ICatch> action);

        ICatch Try(Action<ICatch> action);
        ICatch Try(Func<ICatch> func);

        ICatch Except<T>(Action<ICatch, T> action) where T : Exception;
        ICatch Except<T>(Func<ICatch, T> func) where T : Exception;

        ICatch Finally(Action<ICatch> action);
        ICatch Finally(Func<ICatch> func);

        ICatch Handle();
    }
}