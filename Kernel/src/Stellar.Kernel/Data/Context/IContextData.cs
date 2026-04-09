using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Data.Context
{
    /// <summary>
    /// Маркерный интерфейс для данных, передаваемых внутри <see cref="IContext{TData}"/>.
    /// </summary>
    /// <remarks>
    /// <para>Сам интерфейс не добавляет методов, но позволяет ограничивать обобщённый параметр <typeparamref name="TData"/>
    /// в <see cref="IContext{TData}"/> только типами, предназначенными для контекстных данных.</para>
    /// <para>Реализации должны содержать конкретные поля и свойства, необходимые для определённой операции
    /// (например, <see cref="EntryPoint.IModuleRunContextData"/> или <see cref="EntryPoint.IStopContextData"/>).</para>
    /// </remarks>
    public interface IContextData
        : IQuantumObject
    {
    }
}