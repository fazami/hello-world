using System;

namespace Farzin.Infrastructure.CrossCutting.EventHandling
{
    public interface IEventHandler<T> : IDisposable where T : IEvent
    {
        void Handle(T e);
    }
}
