using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalloutChat.Data
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string QuestionText { get; set; }
        [Required]
        public string Answer { get; set; }
        public Guid UserId { get; set; }
        [DefaultValue(false)]
        public bool QuestionAdded { get; set; }
    }
}
