using Stellar.Kernel.Quantization;

namespace Stellar.Kernel.EventSystem
{
    public interface IEventHandler : IQuant
    {
        /// <summary>
        /// просто обработка события
        /// </summary>
        /// <param name="eventContext">контекст события, тут же и сэндер и событие и дата события</param>
        void Handle(IEventContext eventContext);
    }
}