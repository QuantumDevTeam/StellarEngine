using System;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Failures
{
    public interface ICatch
        : IQuantumObject
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
        void TryAction(Action<ICatch> action);

        /// <summary>
        /// Try code block in Catch context
        /// </summary>
        /// <param name="action">An action</param>
        /// <returns>Active Catch context</returns>
        ICatch Try(Action<ICatch> action);

        /// <summary>
        /// Try code block in Catch context
        /// </summary>
        /// <param name="func">An function</param>
        /// <returns>Active Catch context</returns>
        ICatch Try(Func<ICatch> func);

        /// <summary>
        /// Except code block in CatchContext
        /// </summary>
        /// <param name="action">An action</param>
        /// <typeparam name="T">Excepted Type</typeparam>
        /// <returns>Active Catch context</returns>
        ICatch Except<T>(Action<ICatch, T> action) where T : Exception;

        /// <summary>
        /// Except code block in Catch context
        /// </summary>
        /// <param name="func">An function</param>
        /// <typeparam name="T">Excepted Type</typeparam>
        /// <returns>Active Catch context</returns>
        ICatch Except<T>(Func<ICatch, T> func) where T : Exception;

        /// <summary>
        /// Finally code block in Catch context
        /// </summary>
        /// <param name="action">An action</param>
        /// <returns>Active Catch context</returns>
        ICatch Finally(Action<ICatch> action);

        /// <summary>
        /// Finally code block in Catch context
        /// </summary>
        /// <param name="func">An function</param>
        /// <returns>Active Catch context</returns>
        ICatch Finally(Func<ICatch> func);

        /// <summary>
        /// Handler Catch context
        /// </summary>
        /// <returns>Handled Catch context</returns>
        ICatch Handle();
    }
}