﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TelegaEventsBotDotNet
{
    class BotInput
    {
        private BotCallbacks _callbacks;
        private Telegram.Bot.TelegramBotClient _bot;
        private SettingsWrapper _settingsWrapper;
        public BotInput(Telegram.Bot.TelegramBotClient BotClient)
        {
            _settingsWrapper = new SettingsWrapper("Messages.xml");
            _bot = BotClient;
            _callbacks = new BotCallbacks(this, _bot);
        }

        private void GreetMessage(long ChatID, int ReplyMessageId = 0)
        {
            _bot.SendTextMessageAsync(ChatID, "тест");
        }

        private void StartSearchMessage(long ChatID, int ReplyMessageId = 0)
        {
            var message = _settingsWrapper.StartSearchMessage();
            var buttonLayout = new Telegram.Bot.Types.InlineKeyboardButtons.InlineKeyboardButton[message.Buttons.Count][];
            for (int i = 0; i < message.Buttons.Count; ++i)
            {
                buttonLayout[i] = new[] { new Telegram.Bot.Types.InlineKeyboardButtons.InlineKeyboardCallbackButton(message.Buttons[i].Text, message.Buttons[i].Callback) };
            }
            var keyboard = new Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup(buttonLayout);
            _bot.SendTextMessageAsync(ChatID, message.Text, Telegram.Bot.Types.Enums.ParseMode.Markdown, false, false, 0, keyboard);
            Console.WriteLine("Client connected and asked to start.");
        }

        public void SearchNerbyDateEventsMessage(long ChatID, int ReplyMessageId = 0)
        {
            var message = _settingsWrapper.SearchNearbyDate();
            var buttonLayout = new Telegram.Bot.Types.InlineKeyboardButtons.InlineKeyboardButton[message.Buttons.Count][];
            for (int i = 0; i < message.Buttons.Count; ++i)
            {
                buttonLayout[i] = new[] { new Telegram.Bot.Types.InlineKeyboardButtons.InlineKeyboardCallbackButton(message.Buttons[i].Text, message.Buttons[i].Callback) };
            }
            var keyboard = new Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup(buttonLayout);
            _bot.SendTextMessageAsync(ChatID, message.Text, Telegram.Bot.Types.Enums.ParseMode.Markdown, false, false, 0, keyboard);
            Console.WriteLine("Client asked for nearby events.");
        }

        public void SearchNerbyToday(long chatId, int messageId = 0)
        {
            DatabaseWrapper db = new DatabaseWrapper();
            var ids = db.GetTodayEventsIds();
            int SelectedId = -1;
            RLEvent rLEvent = new RLEvent();
            if (ids.Count > 0)
            {
                Random rand = new Random();
                SelectedId = rand.Next(ids.Count);
                rLEvent = db.GetEventById(ids[SelectedId]);
                _bot.SendTextMessageAsync(chatId, _settingsWrapper.ParseEvent(rLEvent), Telegram.Bot.Types.Enums.ParseMode.Markdown);
                Console.WriteLine("Sent event to client, id: {0}.", rLEvent.EventId);
            }
        }

        public void HandleCommand(String Command, long ChatID, int ReplyMessageId = 0)
        {
            if (Command == "/say")
            {
                GreetMessage(ChatID, ReplyMessageId);
            }
            if (Command == "/start")
            {
                StartSearchMessage(ChatID);
            }
        }

        public void HandleCallback(String Callback, long ChatID, int ReplyMessageId = 0)
        {
            _callbacks.HandleCallback(Callback, ChatID, ReplyMessageId);
        }
    }
}