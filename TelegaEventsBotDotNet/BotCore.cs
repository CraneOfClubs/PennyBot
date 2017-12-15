using System;
using System.Collections.Generic;
using System.Text;

namespace TelegaEventsBotDotNet
{
    class BotCore
    {
        private String _apiKey;
        private Telegram.Bot.TelegramBotClient _bot;
        private BotInput _inputWrapper;

        async void BotInit()
        {
            await _bot.SetWebhookAsync("");
            _inputWrapper = new BotInput(_bot);
            _bot.OnUpdate += async (object su, Telegram.Bot.Args.UpdateEventArgs evu) =>
            {
                if (evu.Update.CallbackQuery != null || evu.Update.InlineQuery != null) return; // в этом блоке нам келлбэки и инлайны не нужны
                var update = evu.Update;
                var message = update.Message;
                if (message == null) return;
                if (message.Type == Telegram.Bot.Types.Enums.MessageType.TextMessage)
                {
                    _inputWrapper.HandleCommand(message.Text, message.Chat.Id, message.MessageId);
                }
            };

            _bot.OnCallbackQuery += async (object sc, Telegram.Bot.Args.CallbackQueryEventArgs ev) =>
            {
                var message = ev.CallbackQuery.Message;
                _inputWrapper.HandleCallback(ev.CallbackQuery.Data, message.Chat.Id, message.MessageId);
                await _bot.AnswerCallbackQueryAsync(ev.CallbackQuery.Id); // отсылаем пустое, чтобы убрать "частики" на кнопке
                //if (ev.CallbackQuery.Data == "callback1")
                //{
                //    await _bot.AnswerCallbackQueryAsync(ev.CallbackQuery.Id, "Хуй " + ev.CallbackQuery.Data, true);
                //}
                //else
                //if (ev.CallbackQuery.Data == "callback2")
                //{
                //    //await Bot.SendTextMessageAsync(message.Chat.Id, "Залупа", replyToMessageId: message.MessageId);
                //    // await Bot.AnswerCallbackQueryAsync(ev.CallbackQuery.Id); // отсылаем пустое, чтобы убрать "частики" на кнопке
                //}
            };
            _bot.StartReceiving();
        }
        public BotCore(String ApiKey)
        {
            _apiKey = ApiKey;
            _bot = new Telegram.Bot.TelegramBotClient(_apiKey);
            BotInit();
            Console.ReadLine();
        }
    }
}
