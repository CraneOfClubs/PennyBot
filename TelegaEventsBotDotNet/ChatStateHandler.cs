using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegaEventsBotDotNet
{
    enum BotState
    {
        IDLE,
        AWAITINGKEYWORD,
        AWAITINGDATE
    }
    class ChatStateHandler
    {
        public ChatStateHandler(Int64 chatId)
        {
            ChatId = chatId;
        }
        public Int64 ChatId { get; set; }
        private BotState currentBotState = BotState.IDLE;
        public BotState CurrentBotState
        {
            get
            {
                return currentBotState;
            }
            set
            {
                currentBotState = value;
            }
        }
    }
}
