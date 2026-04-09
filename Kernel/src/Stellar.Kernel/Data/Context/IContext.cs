using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.Data.Context
{
    /// <summary>
    /// Представляет контекст выполнения операции в движке.
    /// </summary>
    /// <typeparam name="TData">Тип данных контекста, должен реализовывать <see cref="IContextData"/>.</typeparam>
    /// <remarks>
    /// <para>Контекст передаётся во все исполняемые методы движка: запуск/остановка точек входа (<see cref="EntryPoint.StellarEntryPoint"/>),
    /// обновление сцен, рендеринг, обработка событий и т.д.</para>
    /// <para>Содержит отправителя (инициатора вызова) и пользовательские данные, специфичные для операции.</para>
    /// <para>В зависимости от целевой платформы (<c>NETSTANDARD2_0</c> или новее) свойства могут быть nullable.</para>
    /// </remarks>
    /// <example>
    /// Использование контекста в методе <c>Run</c> точки входа:
    /// <code>
    /// public override int Run(IContext&lt;IModuleRunContextData&gt; context)
    /// {
    ///     var logger = context.Data?.Logger;
    ///     logger?.Info("Движок запущен");
    ///     return 0;
    /// }
    /// </code>
    /// </example>
    public interface IContext<out TData>
        : IQuantumObject
        where TData : IContextData
    {
#if NETSTANDARD2_0
        /// <summary>
        /// Квантовый объект, инициировавший выполнение данного контекста.
        /// </summary>
        /// <value>Отправитель (например, точка входа, системный поток или пользовательский код).</value>
        /// <remarks>Может быть <c>null</c>, если контекст создан системой без явного отправителя.</remarks>
        IQuantumObject Sender { get; }
        
        /// <summary>
        /// Данные контекста, специфичные для операции.
        /// </summary>
        /// <value>Экземпляр <typeparamref name="TData"/> или <c>null</c>, если данные не предоставлены.</value>
        TData Data { get; }
#else
#nullable enable
        /// <summary>
        /// Квантовый объект, инициировавший выполнение данного контекста.
        /// </summary>
        /// <value>Отправитель или <c>null</c> при отсутствии явного отправителя.</value>
        IQuantumObject? Sender { get; }
        
        /// <summary>
        /// Данные контекста, специфичные для операции.
        /// </summary>
        /// <value>Экземпляр <typeparamref name="TData"/> или <c>null</c>.</value>
        TData? Data { get; }
#endif
    }
}