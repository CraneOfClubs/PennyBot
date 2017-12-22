using System;
using System.Collections.Generic;
using System.Text;
using NLog;

namespace TelegaEventsBotDotNet
{
    class BotCallbacks
    {
        aprivate static Logger logger = LogManager.GetCurrentClassLogger();
        
        private BotInput _botInput;
        private Telegram.Bot.TelegramBotClient _bot;
        public BotCallbacks(BotInput BotInput, Telegram.Bot.TelegramBotClient Bot)
        {
            _botInput = BotInput;
            _bot = Bot;
        }

        public void HandleCallback(String Callback, long ChatID, int ReplyMessageId = 0)
        {
            if (Callback == "SearchNearbyDate")
            {
                logger.Info("calling SearchNearbyDate");
                _botInput.SearchNerbyDateEventsMessage(ChatID, ReplyMessageId);

            }

            if (Callback == "SearchNearbyToday")
            {
                logger.Info("calling SearchNearToday");
                _botInput.SearchNerbyToday(ChatID, ReplyMessageId);

            }
        }
    }
}
