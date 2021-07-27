using FalloutChat.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalloutChat.Models
{
    public class QuestionVoteListItem
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
        [Required]
        public bool GoodQuestion { get; set; }
        public Guid UserId { get; set; }
    }
}
