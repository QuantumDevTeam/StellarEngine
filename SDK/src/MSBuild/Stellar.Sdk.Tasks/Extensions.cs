using System;
using Microsoft.Build.Utilities;

namespace Stellar.Sdk.Tasks
{
    public static class Extensions
    {
        public static bool TryExecute(TaskLoggingHelper log, string errorMessage, Func<bool> func)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                log.LogError(
                    String.Format(
                        errorMessage,
                        ex
                    )
                );
                return false;
            }
        }
    }
}