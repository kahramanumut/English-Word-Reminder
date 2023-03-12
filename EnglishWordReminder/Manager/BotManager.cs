using EnglishWordReminder.Models;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using EnglishWordReminder.Extensions;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace EnglishWordReminder.Manager
{
    public class BotManager
    {
        private static List<WordModel> _wordList;
        private static List<QuestionModel> _fillInTheBlanks;
        private readonly TelegramBotClient _botClient;
        private readonly string _channelId;

        public BotManager(IConfiguration configuration)
        {
            _wordList = JsonSerializer.Deserialize<List<WordModel>>(File.ReadAllText("frequentlyWords.json"));
            _fillInTheBlanks = JsonSerializer.Deserialize<List<QuestionModel>>(File.ReadAllText("sampleFillBlanks.json"));
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
                    question.Question = questionWordList[0].English;
                    question.Options.Add(questionWordList[0].Turkish);
                    question.Options.Add(questionWordList[1].Turkish);
                    question.Options.Add(questionWordList[2].Turkish);
                    question.Options.Add(questionWordList[3].Turkish);
                    break;
                case QuestionTypeEnum.WordTurkishToEnglish:
                    question.Question = questionWordList[0].Turkish;
                    question.Options.Add(questionWordList[0].English);
                    question.Options.Add(questionWordList[1].English);
                    question.Options.Add(questionWordList[2].English);
                    question.Options.Add(questionWordList[3].English);
                    break;
                default:
                    break;
            }

            question.Options = question.Options.OrderBy(x => Guid.NewGuid()).ToList();
            question.AnswerId = question.Options.FindIndex(x => x == questionWordList[0].English || x == questionWordList[0].Turkish);

            await _botClient.SendPollAsync(_channelId, question.Question, question.Options, correctOptionId: question.AnswerId, type: PollType.Quiz, explanation: $"Hadi <a href=\"https://tureng.com/en/turkish-english/{question.Question}\">buradan</a> doğru cevaba bakalım", explanationParseMode: ParseMode.Html);

        }

        public async Task SendFillInTheQuestion()
        {
            QuestionModel question = _fillInTheBlanks.PickRandom(1).FirstOrDefault();

            int firstHyphenIndex = question.Question.IndexOf('_');
            int lastHyphenIndex = question.Question.LastIndexOf('_');

            string allSentence = question.Question.Remove(firstHyphenIndex, (lastHyphenIndex - firstHyphenIndex+1)).Insert(firstHyphenIndex, question.Options[question.AnswerId]);
            string translateUrl = $"https://translate.google.com/?hl=tr&sl=en&tl=tr&text={allSentence}&op=translate";

            await _botClient.SendPollAsync(_channelId, question.Question, question.Options, correctOptionId: question.AnswerId, type: PollType.Quiz, explanation: $"Hadi <a href=\"{translateUrl}\">buradan</a> çevirisine bakalım", explanationParseMode: ParseMode.Html);
        }
    }
}