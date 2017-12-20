using System;
using System.Collections.Generic;
using System.Text;

namespace TelegaEventsBotDotNet
{
    class BotCallbacks
    {
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
                _botInput.SearchNerbyDateEventsMessage(ChatID, ReplyMessageId);

            }

            if (Callback == "SearchNearbyToday")
            {
                _botInput.SearchNerbyToday(ChatID, ReplyMessageId);

            }
        }
    }
}
