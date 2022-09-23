using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPattern_Asp.NetCore5
{
    public class ApplicationsHandler : IObservable<Application> // Provider
    {
        private readonly List<IObserver<Application>> _observers;
        public List<Application> Applications { get; set; }

        public ApplicationsHandler()
        {
            _observers = new();
            Applications = new();
        }

        public IDisposable Subscribe(IObserver<Application> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);

                foreach (var item in Applications)
                    observer.OnNext(item);
            }

            return new Unsubscriber(_observers, observer);
        }

        public void AddApplication(Application app)
        {
            Applications.Add(app);

            foreach (var observer in _observers)
                observer.OnNext(app);
        }
        public void CloseApplications()
        {
            foreach (var observer in _observers)
                observer.OnCompleted();

            _observers.Clear();
        }
    }

    public class Unsubscriber : IDisposable
    {
        private readonly List<IObserver<Application>> _observers;
        private readonly IObserver<Application> _observer;

        public Unsubscriber(List<IObserver<Application>> observers, IObserver<Application> observer)
        {
            _observers = observers;
            _observer = observer;
        }

        public void Dispose()
        {
            if (_observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}
