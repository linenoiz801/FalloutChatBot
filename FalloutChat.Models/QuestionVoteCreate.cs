using FalloutChat.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalloutChat.Models
{
    public class QuestionVoteCreate
    {
        public int QuestionId { get; set; }
        public bool GoodQuestion { get; set; }
    }
}
