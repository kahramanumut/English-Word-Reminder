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

        public string Question { get; set; }
        public List<string> Options { get; set; }
        public int AnswerId { get; set; }
    }
}
