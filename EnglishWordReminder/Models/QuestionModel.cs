namespace EnglishWordReminder.Models
{
    public class QuestionModel
    {
        public QuestionModel(List<WordModel> questionWordList)
        {
            Options = new List<string>
            {
                questionWordList[0].Word,
                questionWordList[1].Word,
                questionWordList[2].Word,
                questionWordList[3].Word
            };

            Options = Options.OrderBy(x => Guid.NewGuid()).ToList();
            AnswerId = Options.FindIndex(x => x == questionWordList[0].Word);
            Definition = questionWordList[0].Definition;
            Example = questionWordList[0].Example;
            Phonetic = questionWordList[0].Phonetic;
            Synonyms = string.Join(", ", questionWordList[0].Synonyms);
            Level = questionWordList[0].Level;
            Type = (TypeEnum)questionWordList[0].CategoryIds.Where(x => x == 7 || x == 8 || x == 9 || x == 10).First();
        }

        public string Definition { get; set; }
        public string Example { get; set; }
        public string Phonetic { get; set; }
        public string Synonyms { get; set; }
        public string Level { get; set; }
        public TypeEnum Type { get; set; }
        public List<string> Options { get; set; }
        public int AnswerId { get; set; }

    }
}
