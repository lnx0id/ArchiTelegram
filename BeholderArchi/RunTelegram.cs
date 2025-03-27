using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using Program = BeholderArchi.Program;
using Message = Telegram.Bot.Types.Message;
using Update = Telegram.Bot.Types.Update;
using WTelegram;
using BeholderArchi.BotActions;
using TL;


namespace BeholderArchi
{
    internal class RunTelegram
    {
        TelegramBotClient botClient;

        CancellationTokenSource cancelatioTokenSource = new CancellationTokenSource();

        int api_id = 0;
        string api_hash = "/";
        Client app_client;
        List<IAction> actions;
        private bool NowExit;

        public RunTelegram()
        {
            actions = new List<IAction>
            {
                new Remove(), new Return(), new Mute(), new UnMute(), new Ping(), new WhoamI(),
                new ChownFull(), new ChownNoFull(), new ChownGet(), new ChownKill(),
                new Owner(), new Check(), new BotActions.Help(), new BaseHelp(),
                new Credits(), new Echo(), new Touch(), new KillSelf()
            };

            app_client = new Client(api_id, api_hash);
            botClient = new TelegramBotClient("5702729864:AAEkEyynxSnRcS4pe7C7gcA0eiThSCcwzlA");

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cancelatioTokenSource.Token);
        }
        public void Exit()
        {
            cancelatioTokenSource.Cancel();
        }
       
        public async Task SendMessage(string text, long chatId) {
            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: text,
                cancellationToken: cancelatioTokenSource.Token
                );
        }
        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
               
            if (update.Message is not { Text: { } messageText } message)
                return;
            actions.ForEach(actionLocal => actionLocal.Execute(message, botClient, app_client, cancellationToken));

            await Program.ConsoleLog($"$[{message.Date}] | @{message.From.Username} in {message.Chat.Title} -- writed \"{message.Text}\".");

            var titleOrName = (message.Chat.Title != null) ? message.Chat.Title : Functionality.GetFullName(message.Chat);
            var bioOrDescription = (message.Chat.Description != null) ? message.Chat.Description : message.Chat.Bio;

            JsonWorker.SaveChat(message.Chat.Id, titleOrName, bioOrDescription);
            
        }
        Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };
          
            return Task.CompletedTask;
        }
    }
}
