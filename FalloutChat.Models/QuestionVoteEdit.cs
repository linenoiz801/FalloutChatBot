using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalloutChat.Models
{
    public class QuestionVoteEdit
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public bool GoodQuestion { get; set; }
    }
}
