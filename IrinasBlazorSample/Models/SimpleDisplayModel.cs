using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IrinasBlazorSample.Models
{
    public class SimpleDisplayModel
    {
        public string DisplayMessage { get; set; } = string.Empty;
        public int RandomInt => new Random().Next();
        public MessageType MessageType { get; set; } = MessageType.Default;
    }

    public enum MessageType
    {
        Default,
        Success,
        Failure,
        Warning,
    }
}
