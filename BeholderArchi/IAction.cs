using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using WTelegram;
using Message = Telegram.Bot.Types.Message;

namespace BeholderArchi
{
    internal interface IAction
    {
        void Execute(Telegram.Bot.Types.Message message, ITelegramBotClient botClient,
                                   Client app_client, CancellationToken cancellationToken);
    }

    
}
