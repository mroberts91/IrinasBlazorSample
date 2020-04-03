using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IrinasBlazorSample.Models
{
    public class NotificationMessage
    {
        public NotificationMessage(string message, string sender) : this(message, sender, Severity.Info) { }
        public NotificationMessage(string message, string sender, Severity severity)
        {
            Message = message;
            Sender = sender;
            Severity = severity;
        }


        public string Sender { get; set; }
        public string Message { get; set; }
        public Severity Severity { get; set; }
    }

    public enum Severity
    {
        Info,
        Success,
        Error,
    }
}
