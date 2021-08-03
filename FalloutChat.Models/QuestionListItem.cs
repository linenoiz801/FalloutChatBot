using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalloutChat.Models
{
    public class QuestionListItem
    {
        public int Id { get; set; }
        [Required]
        [Display(Name="Question")]
        public string QuestionText { get; set; }
        [Required]
        public string Answer { get; set; }
        public Guid UserId { get; set; }
        [DefaultValue(false)]
        public bool QuestionAdded { get; set; }
        [Display(Name ="Up Votes")]
        public virtual int UpVoteCount { get; set;  }
        [Display(Name ="Down Votes")]
        public virtual int DownVoteCount { get; set; }
    }
}
