using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalloutChat.Models
{
    public class QuestionEdit
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string Answer { get; set; }
        [DefaultValue(false)]
        public bool QuestionAdded { get; set; }
    }
}
