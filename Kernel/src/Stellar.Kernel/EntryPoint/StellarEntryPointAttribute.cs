using System;

namespace Stellar.Kernel.EntryPoint
{
    /// <summary>
    /// Marc a Type implements EntryPoint as main startable
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class StellarEntryPointAttribute
        : Attribute
    {
    }
}