using EnglishWordReminder.Models;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using EnglishWordReminder.Extensions;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using EnglishWordReminder.Services;
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

        public async Task SendWordQuestion()
        {
            var questionWordList = _wordList
            .Where(w => w.Level == "A2" || w.Level == "B1" || w.Level == "B2" || w.Level == "C1" || w.Level == "C2")
            .PickRandom(5)
            .ToList();

            var question = new QuestionModel(questionWordList);

            var pollQuestion = $"📚 {question.Definition}";
            var pollMessage = await _botClient.SendPollAsync(
                chatId: _channelId,
                question: pollQuestion,
                options: question.Options,
                correctOptionId: question.AnswerId,
                type: PollType.Quiz
            );

            var detailedMessage =
                $"📊 Level: {question.Level}\n\n" +
                $"📌 Type: {question.Type}\n\n" +
                $"📝 Example: {question.Example}\n\n" +
                $"🔊 Phonetic: {question.Phonetic}\n\n" +
                $"🔄 Synonyms: {question.Synonyms}\n\n";

            await _botClient.SendTextMessageAsync(
                chatId: _channelId,
                text: detailedMessage,
                parseMode: ParseMode.Html,
                replyToMessageId: pollMessage.MessageId
            );

            var googleTTS = new GoogleTTS();

            var audioStream = await googleTTS.GetAudioStream(question.Options[question.AnswerId]);
            if (audioStream != null)
            {
                await _botClient.SendVoiceAsync(
                       chatId: _channelId,
                       voice: new Telegram.Bot.Types.InputFiles.InputOnlineFile(audioStream),
                       caption: "Word Pronunciation",
                       replyToMessageId: pollMessage.MessageId
                   );
            }

        }
    }
}