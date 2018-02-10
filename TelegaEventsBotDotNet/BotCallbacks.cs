using System;
using System.Collections.Generic;
using System.Text;
using NLog;

namespace TelegaEventsBotDotNet
{
    class BotCallbacks
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        
        private BotInput _botInput;
        private Telegram.Bot.TelegramBotClient _bot;
        public BotCallbacks(BotInput BotInput, Telegram.Bot.TelegramBotClient Bot)
        {
            _botInput = BotInput;
            _bot = Bot;
        }

        public void HandleCallback(String Callback, long ChatID, int ReplyMessageId = 0)
        {
            if (Callback == "StartSearchMessage")
            {
                logger.Info("calling SearchNearbyDate");
                _botInput.StartSearchMessage(ChatID, ReplyMessageId);
            }

            if (Callback == "SearchNearbyDate")
            {
                logger.Info("calling SearchNearbyDate");
                _botInput.SearchNerbyDateEventsMessage(ChatID, ReplyMessageId);
            }

            if (Callback == "SearchNearbyToday")
            {
                logger.Info("calling SearchNearToday");
                _botInput.SearchNerbyRandomToday(ChatID, ReplyMessageId);
            }

            if (Callback == "SearchNearbyTomorrow")
            {
                logger.Info("calling SearchNearToday");
                _botInput.SearchNerbyRandomTomorrow(ChatID, ReplyMessageId);
            }

            if (Callback == "SearchNearbyThisWeek")
            {
                logger.Info("calling SearchNearToday");
                _botInput.SearchNerbyRandomThisWeek(ChatID, ReplyMessageId);
            }

            if (Callback == "SearchByKeywords")
            {
                logger.Info("calling SearchNearToday");
                _botInput.SearchByKeywordsMessage(ChatID, ReplyMessageId);
            }

            if (Callback == "SearchByDate")
            {
                logger.Info("calling SearchNearToday");
                _botInput.SearchByDate(ChatID, ReplyMessageId);
            }

            if (Callback == "RepeatSearchByKeywords")
            {
                logger.Info("calling SearchNearToday");
                _botInput.SearchByKeywordsMessage(ChatID, ReplyMessageId);
            }

            if (Callback == "RepeatSearchRandom")
            {
                logger.Info("calling SearchNearToday");
                _botInput.SearchNerbyDateEventsMessage(ChatID, ReplyMessageId);
            }
        }
    }
}
