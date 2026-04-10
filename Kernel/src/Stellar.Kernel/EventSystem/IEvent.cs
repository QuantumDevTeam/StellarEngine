using System;
using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.EventSystem
{
    public interface IEvent
        : IQuantumObject 
    {
        /// <summary>
        /// тип события
        /// </summary>
        IEventType EventType { get; }
        
        /// <summary>
        /// время создания
        /// </summary>
        DateTime TimeStamp { get; }
        
        /// <summary>
        /// одноразовое ли событие
        /// </summary>
        bool CanBeReused { get; }

        /// <summary>
        /// Проверка на то выпускать ли событие сейчас, нужно переименовать
        /// </summary>
        /// <returns>выбрасывать ли при следующей обработке</returns>
        bool ShouldProcessNow { get; }
        
        // 2 последних могут быть полезны для части функционала, например для цикличеких событий,
        // в пример - анимация AFK. Один говорит не удалять из пула а второй говорит когда выбразывать это событие
    }
}