using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalloutChat.Models
{
    public class ChatHistoryCreate
    {
        public Guid UserId { get; set; }
        [Required]
        public string MessageSent { get; set; }
        public DateTimeOffset SentTimeUtc { get; set; }
        public string ResponseReceived { get; set; }
        public DateTimeOffset ReceviedTimeUtc { get; set; }
        [DefaultValue(false)]
        public bool BadResponse { get; set; }
    }
}
