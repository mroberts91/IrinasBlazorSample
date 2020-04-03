using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IrinasBlazorSample.Models;
using System.Reactive;
using System.Reactive.Subjects;

namespace IrinasBlazorSample.Data
{
    public interface IDataService
    {
        Task SaveAsync<T>(T entity) where T : class;
        void Subscribe(Action<NotificationMessage> notificationAction);
    }

    public class FakeDataService : IDataService
    {
        private readonly Subject<NotificationMessage> _subject;
        public FakeDataService()
        {
            _subject = new Subject<NotificationMessage>();
        }

        public void Subscribe(Action<NotificationMessage> notificationAction)
        {
            _subject.Subscribe(notificationAction);
        }

        // Designed to mock a Save action that notifies the subscriber that the process started and notifiy on error
        public async Task SaveAsync<T>(T entity) where T : class
        {
            // Notify of start of long process
            var startMessage = new NotificationMessage("Saving a new User to the Database", GetType().Name, Severity.Info);
            SendNotification(startMessage);
            await Task.Delay(1000);

            // Notify on error
            var errorMessage = new NotificationMessage($"Failed to save entity of type {entity.GetType().Name}", GetType().Name, Severity.Error);
            await Task.Delay(3000);
            SendNotification(errorMessage);
        }

        private void SendNotification(NotificationMessage message)
        {
            if (_subject.HasObservers) _subject.OnNext(message);
        }
    }
}
