using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPattern_Asp.NetCore5
{
    public class HRSpecialist : IObserver<Application> // Observer
    {
        public string Name { get; set; }
        public List<Application> Applications { get; set; }
        private IDisposable _cancellation;

        public HRSpecialist(string name)
        {
            Name = name;
            Applications = new();
        }

        public void ListApplications()
        {
            if (Applications.Any())
                foreach (var app in Applications)
                    Console.WriteLine($"Hey, {Name}! {app.ApplicantName} has just applied for job no. {app.JobId}");
            else
                Console.WriteLine($"Hey, {Name}! No applications yet.");
        }

        public virtual void Subscribe(ApplicationsHandler provider)
        {
            _cancellation = provider.Subscribe(this);
        }

        public virtual void Unsubscribe()
        {
            _cancellation.Dispose();
            Applications.Clear();
        }

        public void OnCompleted() // indicates that there are no more upcoming notifications
        {
            Console.WriteLine($"Hey, {Name}! We are not accepting any more applications");
        }

        public void OnError(Exception error) // handles any exception raised
        {
            // This is called by the provider if any exception is raised, no need to implement it here.
        }

        public void OnNext(Application value) // receives the notification
        {
            Applications.Add(value);
        }
    }
}
