using IrinasBlazorSample.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IrinasBlazorSample.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly ILogger _logger;

        public NotificationHub(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger(GetType().FullName);
        }
        public async Task SendNotification(NotificationMessage message)
        {
            try
            {
                await Clients.Others.SendAsync("RecieveNotification", message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occued when attempting to send Signal R notification");
            }
        }
    }
}
