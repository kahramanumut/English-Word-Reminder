using Coravel.Invocable;
using EnglishWordReminder.Manager;
using System.Threading.Tasks;

namespace EnglishWordReminder.Jobs
{
    public class FillBlankJob : IInvocable
    {
        private readonly BotManager _botManager;
        public FillBlankJob(BotManager botManager)
        {
            _botManager = botManager;
        }

        public async Task Invoke()
        {
            await _botManager.SendFillInTheQuestion();
        }
    }
}
