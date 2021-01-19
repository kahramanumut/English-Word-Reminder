using EnglishWordReminder.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private string channelId;

        public BotManager(IConfiguration configuration)
        {
            wordList = JsonSerializer.Deserialize<List<WordModel>>(File.ReadAllText("frequentlyWords.json"));
            _botClient = new TelegramBotClient(configuration.GetSection("BotKey").Value);
            channelId = configuration.GetSection("ChannelId").Value;
        }

        public async Task SendWordQuestion(QuestionTypeEnum questionType)
        {

            Random rnd = new Random();

            List<WordModel> questionWordList = wordList.OrderBy(x => Guid.NewGuid()).Take(5).ToList();

            QuestionModel question = new QuestionModel();

            if (questionType == QuestionTypeEnum.WordEnglishToTurkish)
            {
                question.Question = questionWordList[0].English;
                question.Options.Add(questionWordList[0].Turkish);
                question.Options.Add(questionWordList[1].Turkish);
                question.Options.Add(questionWordList[2].Turkish);
                question.Options.Add(questionWordList[3].Turkish);

            }
            else
            {
                question.Question = questionWordList[0].Turkish;
                question.Options.Add(questionWordList[0].English);
                question.Options.Add(questionWordList[1].English);
                question.Options.Add(questionWordList[2].English);
                question.Options.Add(questionWordList[3].English);
            }

            question.Options = question.Options.OrderBy(x => Guid.NewGuid()).ToList();
            question.AnswerId = question.Options.FindIndex(x => x == questionWordList[0].English || x == questionWordList[0].Turkish);
            
            await _botClient.SendPollAsync(channelId, question.Question, question.Options, correctOptionId: question.AnswerId, type: PollType.Quiz, explanation: $"Hadi <a href=\"https://tureng.com/en/turkish-english/{question.Word}\">buradan</a> doğru cevaba bakalım", explanationParseMode: ParseMode.Html);

        }

    }
}