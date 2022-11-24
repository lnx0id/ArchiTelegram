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


//await Program.ConsoleLog($"[{DateTime.Now}] | @{message.From.Username} in {message.Chat.FirstName} -- writed \"{message.Text}\"");


namespace BeholderArchi
{
    internal class RunTelegram
    {
        TelegramBotClient botClient;

        CancellationTokenSource cancelatioTokenSource = new CancellationTokenSource();

        int api_id = 2919669;
        string api_hash = "0e858678d2ff61c95a6993799d895c25";
        Client app_client;
        public RunTelegram()
        {
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
            
            
            if (update.Message is not { } message)
                return;
            if (message.Text is not { } messageText)
                return;
            if (message.Text.Contains("sudo") && message.Text.ToString().Substring(0, 4).Equals("sudo"))
            {
                
                var chatId = message.Chat.Id;
                var titleOrName = (message.Chat.Title != null) ? message.Chat.Title : Functionality.GetFullName(message.Chat);
                var bioOrDescription = (message.Chat.Description != null) ? message.Chat.Description : message.Chat.Bio;
                JsonWorker.SaveChat(chatId, titleOrName, bioOrDescription);
                await Program.ConsoleLog($"$[{DateTime.Now}] | @{message.From.Username} in {message.Chat.Title} -- writed \"{message.Text}\".");
                

                if (message.Text.Contains("echo"))
                {
                    await Functionality.echo(message, botClient, cancellationToken);
                }
                else if (message.Text.Contains("whoami"))
                {
                    await Functionality.whoami(message, botClient, cancellationToken);
                }
                else if (message.Text.Contains("ping"))
                {
                    var entities = message.Entities;
                    await Functionality.ping(message, entities, botClient, cancellationToken, app_client);
                }
                else if (message.Text.Contains("owner"))
                {
                    await Functionality.printAdminsList(message, botClient, cancellationToken);
                }
                else if (message.Text.Contains("remove"))
                {
                    ChatMember messageFrom = await botClient.GetChatMemberAsync(message.Chat.Id, message.From.Id);
                    if (Functionality.IsHaveBanPerms(messageFrom))
                    {
                        var entities = message.Entities;
                        await Functionality.banUser(message, entities, botClient, cancellationToken, app_client);
                    }
                    else
                    {
                        await Functionality.messageSend("У тебя не достаточно прав", botClient, cancellationToken, chatId);
                    }
                }
                else if (message.Text.Contains("return"))
                {
                    ChatMember messageFrom = await botClient.GetChatMemberAsync(message.Chat.Id, message.From.Id);
                    if (Functionality.IsHaveBanPerms(messageFrom))
                    {
                        var entities = message.Entities;
                        await Functionality.unbanUser(message, entities, botClient, cancellationToken, app_client);
                    }
                    else
                    {
                        await Functionality.messageSend("У тебя не достаточно прав", botClient, cancellationToken, chatId);
                    }
                }
                else if (message.Text.Contains("mute") && message.Text.Substring(5, 4).Contains("mute"))
                {
                    ChatMember messageFrom = await botClient.GetChatMemberAsync(message.Chat.Id, message.From.Id);
                    if (Functionality.IsHaveBanPerms(messageFrom))
                    {
                        var entities = message.Entities;
                        await Functionality.muteUser(message, entities, botClient, cancellationToken, app_client);
                    }
                    else
                    {
                        await Functionality.messageSend("У тебя не достаточно прав", botClient, cancellationToken, chatId);
                    }
                }
                else if (message.Text.Contains("unmute"))
                {
                    ChatMember messageFrom = await botClient.GetChatMemberAsync(message.Chat.Id, message.From.Id);
                    if (Functionality.IsHaveBanPerms(messageFrom))
                    {
                        var entities = message.Entities;
                        await Functionality.unmuteUser(message, entities, botClient, cancellationToken, app_client);
                    }
                    else
                    {
                        await Functionality.messageSend("У тебя не достаточно прав", botClient, cancellationToken, chatId);
                    }
                }
                else if (message.Text.Contains("chown full"))
                {
                    ChatMember messageFrom = await botClient.GetChatMemberAsync(message.Chat.Id, message.From.Id);
                    if (Functionality.IsHavePromotePerms(messageFrom))
                    {
                        var entities = message.Entities;
                        await Functionality.chownFull(message, entities, botClient, cancellationToken, app_client);
                    }
                    else
                    {
                        await Functionality.messageSend("У тебя не достаточно прав", botClient, cancellationToken, chatId);
                    }

                }
                else if (message.Text.Contains("chown nofull"))
                {
                    ChatMember messageFrom = await botClient.GetChatMemberAsync(message.Chat.Id, message.From.Id);
                    if (Functionality.IsHavePromotePerms(messageFrom))
                    {
                        var entities = message.Entities;
                        await Functionality.chownNofull(message, entities, botClient, cancellationToken, app_client);
                    }
                    else
                    {
                        await Functionality.messageSend("У тебя не достаточно прав", botClient, cancellationToken, chatId);
                    }
                }
                else if (message.Text.Contains("chown get"))
                {
                    var entities = message.Entities;
                    await Functionality.chownGet(message, entities, botClient, cancellationToken, app_client);
                }
                else if (message.Text.Contains("credits"))
                {
                    await Functionality.credits(message, botClient, cancellationToken);
                }
                else if (message.Text.Contains("touch"))
                {
                    await Functionality.touch(message, botClient, cancellationToken);
                }
                else if (message.Text.Contains("check"))
                {
                    await Functionality.messageSend("даа. да-да-да... Я жив)", botClient, cancellationToken, chatId);
                }
                else if (message.Text.ToLower().Contains("help"))
                {

                    string commandsToOutput = "";
                    for (int i = 0; i < Functionality.reserved.Length; i++)
                    {
                        if (i == 0)
                        {
                            commandsToOutput += "$sudo " + Functionality.reserved[i].ToString();
                        }
                        else
                        {
                            commandsToOutput += "\n$sudo " + Functionality.reserved[i].ToString();
                        }

                    }
                    await Functionality.messageSend($"___\n{commandsToOutput}\n//Исользование без $\n___", botClient, cancellationToken, message.Chat.Id);
                }
                else if (message.Text.Contains("kill main"))
                {
                    //Exit();
                    //Application.Exit();
                }
                else if (message.Text.ToLower().Contains("rm -rf /"))
                {

                    await botClient.LeaveChatAsync(chatId);
                }
                else
                {
                    Message sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "`Ты знаешь толк`\n`список команд на $sudo help\n//Использование без $`",
                        parseMode: ParseMode.MarkdownV2,
                        cancellationToken: cancellationToken);
                }
            }
            else if (message.Text.Contains("бот") && message.Text.ToString().Substring(0, 3).Equals("бот"))
            {
                await Functionality.messageSend("Проблемы? где волшебное слово?", botClient, cancellationToken, message.Chat.Id);
            }
            else if (message.Text.Contains("пожалуйста") && message.Text.ToString().Substring(0, 10).Equals("пожалуйста"))
            {
                await Functionality.messageSend("Есть ещё более волщебное слово) https://www.kjprojs.tk/Archi", botClient, cancellationToken, message.Chat.Id);
            }
            else if (message.Text.ToLower().Contains("/help"))
            {
                var chatId = message.Chat.Id;
                await Program.ConsoleLog($"${messageText} ChatID = {chatId},sended by - @{message.From.Username}.");
                await Functionality.messageSend("список команд на $sudo help\n//Использование без $", botClient, cancellationToken, message.Chat.Id);
            }
        }
        Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            //Program.ConsoleLog("[ERROR] -- "+ErrorMessage);
            return Task.CompletedTask;
        }
    }
}
