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
        AWAITINGDATE,
        AWAITINGPAGE
    }
    class ChatStateHandler
    {
        public ChatStateHandler(Int64 chatId)
        {
            ChatId = chatId;
        }
        public Int64 ChatId { get; set; }
        public Int64 PreviewPage { get; set; }
        public Int32 CurrentPreviewMessageId { get; set; }
        public Int32 MessagesInResult { get; set; }
        public List<String> messagBlocks { get; set; }
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
