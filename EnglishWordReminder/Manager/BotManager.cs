using EnglishWordReminder.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using EnglishWordReminder.Extentions;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace EnglishWordReminder.Manager
{
    public class BotManager
    {
        private static List<WordModel> _wordList;
        private readonly TelegramBotClient _botClient;
        private readonly string _channelId;

        public BotManager(IConfiguration configuration)
        {
            _wordList = JsonSerializer.Deserialize<List<WordModel>>(File.ReadAllText("frequentlyWords.json"));
            _botClient = new TelegramBotClient(configuration.GetSection("BotKey").Value);
            _channelId = configuration.GetSection("ChannelId").Value;
        }

        public async Task SendWordQuestion(QuestionTypeEnum questionType)
        {
            var questionWordList = _wordList.PickRandom(5).ToList();
            
            var question = new QuestionModel();

            switch (questionType)
            {
                case QuestionTypeEnum.WordEnglishToTurkish:
                    question.Word = questionWordList[0].English;
                    question.Options.Add(questionWordList[0].Turkish);
                    question.Options.Add(questionWordList[1].Turkish);
                    question.Options.Add(questionWordList[2].Turkish);
                    question.Options.Add(questionWordList[3].Turkish);
                    break;
                case QuestionTypeEnum.WordTurkishToEnglish:
                    break;
                default:
                    question.Word = questionWordList[0].Turkish;
                    question.Options.Add(questionWordList[0].English);
                    question.Options.Add(questionWordList[1].English);
                    question.Options.Add(questionWordList[2].English);
                    question.Options.Add(questionWordList[3].English);
                    break;
            }

            question.Options = question.Options.OrderBy(x => Guid.NewGuid()).ToList();
            var correctOptionId = question.Options.FindIndex(x => x == questionWordList[0].English || x == questionWordList[0].Turkish);
            
            var result = await _botClient.SendPollAsync(_channelId, question.Word, question.Options, correctOptionId: correctOptionId, type: PollType.Quiz);

        }

    }
}