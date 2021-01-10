using Coravel.Invocable;
using EnglishWordReminder.Manager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EnglishWordReminder.Jobs
{
    public class BotJob : IInvocable
    {
        private readonly BotManager _botManager;
        public BotJob(BotManager botManager)
        {
            _botManager = botManager;
        }

        public async Task Invoke()
        {
            if (DateTime.Now.Hour % 2 == 0)
                await _botManager.SendWordQuestion(Models.QuestionTypeEnum.WordEnglishToTurkish);
            else
                await _botManager.SendWordQuestion(Models.QuestionTypeEnum.WordTurkishToEnglish);
        }
    }
}
