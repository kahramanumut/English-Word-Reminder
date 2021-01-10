using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishWordReminder.Models
{
    public class QuestionModel
    {
        public QuestionModel()
        {
            Options = new List<string>();
        }

        public string Word { get; set; }
        public List<string> Options { get; set; }
    }
}
