using System;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Failures
{
    /// <summary>
    /// Failure Catcher
    /// </summary>
    public interface IFailureCatch
        : IIdentifiableQuantumObject, IRegistrableQuantumObject
    {
        /// <summary>
        /// Try action in Failure Catch context
        /// </summary>
        /// <param name="action">An action</param>
        void TryAction(Action action);

        /// <summary>
        /// Try action in Failure Catch context
        /// </summary>
        /// <param name="action">An action</param>
        void TryAction(Action<IFailureCatch> action);

        /// <summary>
        /// Try code block in Catch context
        /// </summary>
        /// <param name="action">An action</param>
        /// <returns>Active Catch context</returns>
        IFailureCatch Try(Action<IFailureCatch> action);

        /// <summary>
        /// Try code block in Catch context
        /// </summary>
        /// <param name="func">An function</param>
        /// <returns>Active Catch context</returns>
        IFailureCatch Try(Func<IFailureCatch> func);

        /// <summary>
        /// Except code block in CatchContext
        /// </summary>
        /// <param name="action">An action</param>
        /// <typeparam name="T">Excepted Type</typeparam>
        /// <returns>Active Catch context</returns>
        IFailureCatch Except<T>(Action<IFailureCatch, T> action) where T : Exception;

        /// <summary>
        /// Except code block in Catch context
        /// </summary>
        /// <param name="func">An function</param>
        /// <typeparam name="T">Excepted Type</typeparam>
        /// <returns>Active Catch context</returns>
        IFailureCatch Except<T>(Func<IFailureCatch, T> func) where T : Exception;

        /// <summary>
        /// Finally code block in Catch context
        /// </summary>
        /// <param name="action">An action</param>
        /// <returns>Active Catch context</returns>
        IFailureCatch Finally(Action<IFailureCatch> action);

        /// <summary>
        /// Finally code block in Catch context
        /// </summary>
        /// <param name="func">An function</param>
        /// <returns>Active Catch context</returns>
        IFailureCatch Finally(Func<IFailureCatch> func);

        /// <summary>
        /// Handler Catch context
        /// </summary>
        /// <returns>Handled Catch context</returns>
        IFailureCatch Handle();
    }
}