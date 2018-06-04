using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farzin.Infrastructure.CrossCutting.EventHandling
{
    public static class EventBus
    {
        static List<object> _observers = new List<object>();
        public static void Subscribe<T>(IEventHandler<T> observer) where T : IEvent
        {
            if (_observers.Contains(observer) == false)
                _observers.Add(observer);
        }
        public static void UnSubscribe<T>(IEventHandler<T> observer) where T : IEvent
        {
            if (_observers.Contains(observer))
                _observers.Remove(observer);
        }
        public static void RaiseEvent<T>(T e) where T : IEvent
        {
            _observers.OfType<IEventHandler<T>>().ToList().ForEach(a => a.Handle(e));
        }
        public static void RaiseEventAsync<T>(T e) where T : IEvent
        {
            var t = new Task(() => RaiseEvent(e));
            t.Start();
        }
    }
}
