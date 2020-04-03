using IrinasBlazorSample.Data;
using IrinasBlazorSample.Data.Models;
using IrinasBlazorSample.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Radzen;
using System;
using System.Threading.Tasks;

namespace IrinasBlazorSample.Pages
{
    public partial class ReactiveEvents
    {
        private ILogger _logger;

        [Inject] IDataService DataService { get; set; }
        [Inject] ILoggerFactory LoggerFactory { get; set; }
        [Inject] NotificationService NotificationService { get; set; }
        

        protected override async Task OnInitializedAsync()
        {
            _logger = LoggerFactory.CreateLogger("Events Blazor Page");
            InitializeDataServiceObserver();
        }

        void InitializeDataServiceObserver()
        {
            // Subscribe to the DataService Events
            // This get call when the data service raises an event.
            DataService.Subscribe(message =>
            {
                _logger.LogInformation("Message recieved from {sender} - {message}", message.Sender, message.Message);
                ShowNotificationMessage(message);
            });
        }

        async void ShowNotificationMessage(Models.NotificationMessage message)
        {
            NotificationService.Notify(new Radzen.NotificationMessage
            {
                Severity = message.Severity switch
                {
                    Severity.Info => NotificationSeverity.Info,
                    Severity.Success => NotificationSeverity.Success,
                    Severity.Error => NotificationSeverity.Error,
                },
                Detail = message.Message,
                Summary = $"{message.Sender} at {DateTime.Now.ToShortTimeString()}",
                Duration = message.Severity switch
                {
                    Severity.Info => 10000,
                    Severity.Success => 12000,
                    Severity.Error => 20000,
                    _ => 3000
                }
            });

            await InvokeAsync(() => { StateHasChanged(); });
        }

        private async Task SaveData()
        {
            DataService.SaveAsync(new User());
        }
    }
}
