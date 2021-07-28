using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalloutChat.Models
{
    public class FNVBotContext
    {
        public bool isContextOnly { get; set; }
        public List<object> prompts { get; set; }
    }

    public class FNVBotAnswer
    {
        public List<string> questions { get; set; }
        public string answer { get; set; }
        public double score { get; set; }
        public int id { get; set; }
        public string source { get; set; }
        public bool isDocumentText { get; set; }
        public List<object> metadata { get; set; }
        public FNVBotContext context { get; set; }
    }

    public class FNVBotRoot
    {
        public List<FNVBotAnswer> answers { get; set; }
        public bool activeLearningEnabled { get; set; }
    }
}
