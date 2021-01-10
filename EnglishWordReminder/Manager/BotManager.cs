using EnglishWordReminder.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace EnglishWordReminder.Manager
{
    public class BotManager
    {
        public static List<WordModel> wordList;
        private TelegramBotClient _botClient;

        public BotManager()
        {
            wordList = JsonSerializer.Deserialize<List<WordModel>>(File.ReadAllText("frequentlyWords.json"));
            _botClient = new TelegramBotClient("Bot Key"); 
        }

        public async Task SendWordQuestion(QuestionTypeEnum questionType)
        {
            Random rnd = new Random();

            List<WordModel> questionWordList = wordList.OrderBy(x => Guid.NewGuid()).Take(5).ToList();

            QuestionModel question = new QuestionModel();

            if (questionType == QuestionTypeEnum.WordEnglishToTurkish)
            {
                question.Word = questionWordList[0].English;
                question.Options.Add(questionWordList[0].Turkish);
                question.Options.Add(questionWordList[1].Turkish);
                question.Options.Add(questionWordList[2].Turkish);
                question.Options.Add(questionWordList[3].Turkish);
            }
            else
            {
                question.Word = questionWordList[0].Turkish;
                question.Options.Add(questionWordList[0].English);
                question.Options.Add(questionWordList[1].English);
                question.Options.Add(questionWordList[2].English);
                question.Options.Add(questionWordList[3].English);
            }

            question.Options = question.Options.OrderBy(x => Guid.NewGuid()).ToList();
            int correctOptionId = question.Options.FindIndex(x => x == questionWordList[0].English || x == questionWordList[0].Turkish);

            
            var result = await _botClient.SendPollAsync("Channel or Group ID", question.Word, question.Options, correctOptionId: correctOptionId, type: PollType.Quiz);

        }

    }
}
