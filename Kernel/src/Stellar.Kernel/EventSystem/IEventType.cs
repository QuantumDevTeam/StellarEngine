using Stellar.Kernel.Label;

namespace Stellar.Kernel.EventSystem;

// TODO: ADD DOCS (IMPORTANT)
public interface IEventType
    : ILabel
{
    /// <summary>
    /// Знаковый тип события меньше 0 - движковые и системные, больше 0 - кастомные. Identifier в ILabel нужен
    /// просто для идентефицирования самого типа в реестрах и т.п. это же поле нужно для идентефикаций в типах,
    /// парой айди будет не хватать
    /// </summary>
    short TypeValue { get; }
}