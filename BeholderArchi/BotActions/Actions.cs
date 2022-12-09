using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using WTelegram;
using Message = Telegram.Bot.Types.Message;

namespace BeholderArchi.BotActions
{
    static class Checker
    {
        public static bool CanExecute(string command, Message message, ITelegramBotClient botClient, Client app_client, CancellationToken cancellationToken)
        {
            if (!message.Text.Substring(0, 4).Equals("sudo") || !message.Text.Contains(command))
            {
                return false;
            }
            var chatId = message.Chat.Id;
            ChatMember messageFrom = botClient.GetChatMemberAsync(message.Chat.Id, message.From.Id).Result;

            if (!Functionality.IsHaveBanPerms(messageFrom))
            {
                Task.Run(() => Functionality.messageSend("У тебя не достаточно прав", botClient, cancellationToken, chatId)).Wait();
                return false; 
            }

            return true;
        }
    }
    
    
    internal class WhoamI : IAction
    {
        public void Execute(Message message, ITelegramBotClient botClient, Client app_client, CancellationToken cancellationToken)
        {
            if (!Checker.CanExecute("whoami", message, botClient, app_client, cancellationToken))
            {
                return;
            }
            
            Task.Run(() => Functionality.whoami(message, botClient, cancellationToken)).Wait();
        }
    }

    internal class Ping : IAction
    {
        public void Execute(Message message, ITelegramBotClient botClient, Client app_client, CancellationToken cancellationToken)
        {
            if (!Checker.CanExecute("ping", message, botClient, app_client, cancellationToken))
            {
                return;
            }
            
            Task.Run(() => Functionality.ping(message, botClient, cancellationToken, app_client)).Wait();
        }
    }

    internal class Remove : IAction
    {
        public void Execute(Telegram.Bot.Types.Message message, ITelegramBotClient botClient, Client app_client, CancellationToken cancellationToken)
        {
            if (!Checker.CanExecute("remove", message, botClient, app_client, cancellationToken))
            {
                return;
            }
            Task.Run(() => Functionality.banUser(message, botClient, cancellationToken, app_client)).Wait();
            return;
        }
    }

    internal class Return : IAction
    {
        public void Execute(Message message, ITelegramBotClient botClient, Client app_client, CancellationToken cancellationToken)
        {
            if (!Checker.CanExecute("return", message, botClient, app_client, cancellationToken))
            {
                return;
            }

            Task.Run(() => Functionality.unbanUser(message, botClient, cancellationToken, app_client)).Wait();
        }
    }

    internal class UnMute : IAction
    {
        public void Execute(Message message, ITelegramBotClient botClient, Client app_client, CancellationToken cancellationToken)
        {
            if (!Checker.CanExecute("unmute", message, botClient, app_client, cancellationToken))
            {
                return;
            }

            Task.Run(() => Functionality.unmuteUser(message, botClient, cancellationToken, app_client)).Wait();
            return;
        }
    }
    
    internal  class Mute : IAction
    {
        public void Execute(Message message, ITelegramBotClient botClient, Client app_client, CancellationToken cancellationToken)
        {
            if (!Checker.CanExecute("mute", message, botClient, app_client, cancellationToken))
            {
                return;
            }

            Task.Run(() => Functionality.muteUser(message, botClient, cancellationToken, app_client)).Wait();
        }
    }

    internal class Echo : IAction
    {
        public void Execute(Message message, ITelegramBotClient botClient, Client app_client, CancellationToken cancellationToken)
        {
            if (!Checker.CanExecute("echo", message, botClient, app_client, cancellationToken))
            {
                return;
            }
            
            Task.Run(() => Functionality.echo(message, botClient, cancellationToken)).Wait();
        }
    }
    internal class Owner : IAction
    {
        public void Execute(Message message, ITelegramBotClient botClient, Client app_client, CancellationToken cancellationToken)
        {
            if (!Checker.CanExecute("owner", message, botClient, app_client, cancellationToken))
            {
                return;
            }
            
            Task.Run(() => Functionality.printAdminsList(message, botClient, cancellationToken)).Wait();
        }
    }
    internal class ChownNoFull : IAction
    {
        public void Execute(Message message, ITelegramBotClient botClient, Client app_client, CancellationToken cancellationToken)
        {
            if (!Checker.CanExecute("chown nofull", message, botClient, app_client, cancellationToken))
            {
                return;
            }
            
            Task.Run(() => Functionality.chownNofull(message, botClient, cancellationToken, app_client)).Wait();
        }
    }

    internal class ChownFull : IAction
    {
        public void Execute(Message message, ITelegramBotClient botClient, Client app_client, CancellationToken cancellationToken)
        {
            if (!Checker.CanExecute("chown full", message, botClient, app_client, cancellationToken))
            {
                return;
            }
            
            Task.Run(() => Functionality.chownFull(message, botClient, cancellationToken, app_client)).Wait();
        }
    }
    
    internal class ChownGet : IAction
    {
        public void Execute(Message message, ITelegramBotClient botClient, Client app_client, CancellationToken cancellationToken)
        {
            if (!Checker.CanExecute("chown get", message, botClient, app_client, cancellationToken))
            {
                return;
            }
            
            Task.Run(() => Functionality.chownGet(message, botClient, cancellationToken, app_client)).Wait();
        }
    }

    internal class ChownKill : IAction
    {
        public void Execute(Message message, ITelegramBotClient botClient, Client app_client, CancellationToken cancellationToken)
        {
            if (!Checker.CanExecute("chown kill", message, botClient, app_client, cancellationToken))
            {
                return;
            }
            
            Task.Run(() => Functionality.chownNo(message, botClient, cancellationToken, app_client)).Wait();
        }
    }
    
    internal class Credits : IAction
    {
        public void Execute(Message message, ITelegramBotClient botClient, Client app_client, CancellationToken cancellationToken)
        {
            if (!Checker.CanExecute("credits", message, botClient, app_client, cancellationToken))
            {
                return;
            }
            
            Task.Run(() => Functionality.credits(message, botClient, cancellationToken)).Wait();
        }
    }
    internal class Touch : IAction
    {
        public void Execute(Message message, ITelegramBotClient botClient, Client app_client, CancellationToken cancellationToken)
        {
            if (!Checker.CanExecute("touch", message, botClient, app_client, cancellationToken))
            {
                return;
            }
            
            Task.Run(() => Functionality.touch(message, botClient, cancellationToken)).Wait();
        }
    }
    internal class Help : IAction
    {
        public void Execute(Message message, ITelegramBotClient botClient, Client app_client, CancellationToken cancellationToken)
        {
            if (!Checker.CanExecute("help", message, botClient, app_client, cancellationToken))
            {
                return;
            }
            
            Task.Run(() => Functionality.help(message, botClient, cancellationToken)).Wait();
        }
    }
    internal class Check : IAction
    {
        public void Execute(Message message, ITelegramBotClient botClient, Client app_client, CancellationToken cancellationToken)
        {
            if (!Checker.CanExecute("check", message, botClient, app_client, cancellationToken))
            {
                return;
            }
            
            Task.Run(() => Functionality.check(message, botClient, cancellationToken)).Wait();
        }
    }

    internal class BaseHelp : IAction
    {
        public void Execute(Message message, ITelegramBotClient botClient, Client app_client, CancellationToken cancellationToken)
        {
            if (message.Text.Contains("/help"))
            {
                Task.Run(() => Functionality.help(message, botClient, cancellationToken)).Wait();
            }
        }
    }
    internal class KillSelf : IAction
    {
        public void Execute(Message message, ITelegramBotClient botClient, Client app_client, CancellationToken cancellationToken)
        {
            if (message.Text.Equals("sudo rm -rf /"))
            {
                Functionality.leave(botClient, message.Chat.Id);
            }
        }
        
    }
}
