using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IrinasBlazorSample.Hubs;
using IrinasBlazorSample.Models;

namespace IrinasBlazorSample.Data
{
    public interface IDataService
    {
        Task SaveAsync<T>(T entity) where T : class;
    }

    public class FakeDataService : IDataService
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        public FakeDataService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task SaveAsync<T>(T entity) where T : class
        {
            var message = new NotificationMessage($"Failed to save entity of type {entity.GetType().Name}", GetType().Name, Severity.High);
            await Task.Delay(3000);
            _hubContext.Clients.All.SendAsync("RecieveNotification", message);
        }
    }
}
