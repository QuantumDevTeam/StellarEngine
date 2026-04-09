using System;

namespace Stellar.Kernel.EntryPoint
{
    /// <summary>
    /// Marks a class as a discoverable entry point that can be started by the engine.
    /// </summary>
    /// <remarks>
    /// <para>Apply this attribute to a class derived from <see cref="StellarEntryPoint"/> to indicate that
    /// it is the main startup type for a module or application.</para>
    /// <para>The engine scans assemblies for this attribute during initialization and creates an instance
    /// of the marked class.</para>
    /// </remarks>
    /// <example>
    /// <code>
    /// [StellarEntryPoint]
    /// public class MyGameEntry : StellarEntryPoint { ... }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class StellarEntryPointAttribute
        : Attribute
    {
    }
}