using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalloutChat.Models
{
    public class ChatHistoryListItem
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string MessageSent { get; set; }
        public DateTimeOffset SentTimeUtc { get; set; }
        public string ResponseReceived { get; set; }
        public DateTimeOffset ReceviedTimeUtc { get; set; }
        [DefaultValue(false)]
        public bool BadResponse { get; set; }
    }
}
