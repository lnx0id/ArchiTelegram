using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TL;
using Message = Telegram.Bot.Types.Message;
using MessageEntity = Telegram.Bot.Types.MessageEntity;
using User = Telegram.Bot.Types.User;
using BeholderArchi;
using WTelegram;

namespace BeholderArchi
{
    internal class Functionality
    {
        internal static string[] reserved = { "whoami", "owner",
                                            "remove @user - чтобы забанить",
                                            "return @user - чтобы разбанить",
                                            "chown full @user - чтобы выдать админа со всеми правами",
                                            "chown nofull @user - чтобы выдать админа без права выбора админов",
                                            "chown get @user - список прав пользователя",
                                            "mute @user - замутить",
                                            "unmute @user - размутить",
                                            "ping @user","touch - Создать ссылку приглашение",
                                            "credits", "echo" ,
                                            "chown kill @user - чтобы забрать права админа"};
        async internal static Task credits(Message message, ITelegramBotClient botClient, CancellationToken cancellationToken)
        {
            await messageSend("Writed by @I_am_Linuxoid\nЯ даже не юзал stackoverfow. только один вопросик на хабре:)", botClient, cancellationToken, message.Chat.Id);
        }
        async internal static Task echo(Message message, ITelegramBotClient botClient, CancellationToken cancellationToken)
        {
            try
            {
                var echo = message.Text.Substring(10);
                await messageSend(echo, botClient, cancellationToken, message.Chat.Id);
            }
            catch {
                await messageSend("ты даже сдесь намудрил...", botClient, cancellationToken, message.Chat.Id);
            }
        }
        async internal static Task whoami(Message message, ITelegramBotClient botClient, CancellationToken cancellationToken)
        {
            ChatMember iMember = await botClient.GetChatMemberAsync(message.Chat.Id, message.From.Id);

            var FullInfoForOutput = GetFullInfoAboutUser(iMember);
            await messageSend(FullInfoForOutput + "\n\n -- Для списка админов $sudo owner", botClient, cancellationToken, message.Chat.Id);
        }
        async internal static Task ping(Message message, MessageEntity[] entities, ITelegramBotClient botClient,
                                  CancellationToken cancellationToken, Client app_client)
        {
            if (entities != null)
            {
                if (entities[0].User != null)
                {
                    var member = await botClient.GetChatMemberAsync(message.Chat.Id, entities[0].User.Id);
                    string fullInfo = GetFullInfoAboutUser(member);
                    await messageSend($"{fullInfo}", botClient, cancellationToken, message.Chat.Id);
                }
                else if (entities[0] != null && entities[0].Type.ToString().Contains("Mention"))
                {

                    long memberId = await DLC.GetUserIdByUsernameAsync(message.Text.Substring(11), "5702729864:AAEkEyynxSnRcS4pe7C7gcA0eiThSCcwzlA", app_client);
                    if (memberId != 0)
                    {
                        var member = await botClient.GetChatMemberAsync(message.Chat.Id, memberId);
                        string fullInfo = GetFullInfoAboutUser(member);
                        await messageSend($"{fullInfo}", botClient, cancellationToken, message.Chat.Id);
                    }
                    else
                    {
                        await messageSend($"Косяк! может ты не правильно упомянул? за репортами сюда - @ArchisErrors", botClient, cancellationToken, message.Chat.Id);
                    }
                }
            }
            else
            {
                await messageSend("Ты должен упомянуть через @ или на прямую", botClient, cancellationToken, message.Chat.Id);
            }
        }
        async internal static Task printAdminsList(Message message, ITelegramBotClient botClient, CancellationToken cancellationToken)
        {
            var members = await botClient.GetChatAdministratorsAsync(message.Chat.Id);
            string Perms = "";
            for (int index = 0; index < members.Length; index++)
            {
                string ChatStatusRu = members[index].Status.ToString().Contains("Administrator") ? "Администратор" : "Основатель";
                Perms += "\n @" + members[index].User.Username + " -- " + ChatStatusRu + ";";
            }
            await messageSend($"{Perms}\n -- эти ребята имеют рут доступ", botClient, cancellationToken, message.Chat.Id);

        }
        async internal static Task banUser(Message message, MessageEntity[] entities, ITelegramBotClient botClient,
                                  CancellationToken cancellationToken, Client app_client)
        {
            if (entities != null)
            {
                if (entities[0].User != null)
                {
                    await botClient.BanChatMemberAsync(message.Chat.Id, entities[0].User.Id);
                }
                else if (entities != null && entities[0].Type.ToString().Contains("Mention"))
                {
                    var userName = message.Text.Substring(13);
                    long memberId = await DLC.GetUserIdByUsernameAsync(userName, "5702729864:AAEkEyynxSnRcS4pe7C7gcA0eiThSCcwzlA", app_client);
                    if (memberId != 0)
                    {
                        if (memberId.Equals(message.From.Id))
                        {
                            await messageSend("Да! ты можешь забанить сам себя... Но поверь ты потом хрен объяснишь остальным как и почему тебя надо сново добавить", botClient, cancellationToken, message.Chat.Id);
                        }
                        else
                        {
                            try
                            {
                                var member = await botClient.GetChatMemberAsync(message.Chat.Id, memberId);
                                await botClient.BanChatMemberAsync(message.Chat.Id, memberId);
                                await messageSend("Успешно забанен", botClient, cancellationToken, message.Chat.Id);
                            }
                            catch
                            {
                                await messageSend("Может это админ? за репортами сюда @ArchisErrors", botClient, cancellationToken, message.Chat.Id);
                            }
                        }
                    }
                    else
                    {
                        await messageSend($"Косяк! может ты не правильно упомянул? за репортами сюда - @ArchisErrors", botClient, cancellationToken, message.Chat.Id);
                    }
                }
            }
            else
            {
                await messageSend("Ты должен упомянуть через @ или на прямую", botClient, cancellationToken, message.Chat.Id);
            }

        }
        async internal static Task unbanUser(Message message, MessageEntity[] entities, ITelegramBotClient botClient,
                                  CancellationToken cancellationToken, Client app_client)
        {
            if (entities != null)
            {
                if (entities[0].User != null)
                {
                    await botClient.UnbanChatMemberAsync(message.Chat.Id, entities[0].User.Id);
                    await messageSend("Успешно разбанен, теперь он может вернутся", botClient, cancellationToken, message.Chat.Id);
                }
                else if (entities != null && entities[0].Type.ToString().Contains("Mention"))
                {
                    var userName = message.Text.Substring(13);
                    long memberId = await DLC.GetUserIdByUsernameAsync(userName, "5702729864:AAEkEyynxSnRcS4pe7C7gcA0eiThSCcwzlA", app_client);

                    if (memberId != 0)
                    {
                        var member = await botClient.GetChatMemberAsync(message.Chat.Id, memberId);
                        var banedMember = member.Status.ToString();

                        if (banedMember.Contains("Kicked") || banedMember.Contains("Banned"))
                        {
                            try
                            {

                                await botClient.UnbanChatMemberAsync(message.Chat.Id, memberId);
                                await messageSend("Успешно разбанен, теперь он может вернутся", botClient, cancellationToken, message.Chat.Id);
                            }
                            catch
                            {
                                await messageSend("Я даже не представляю что пошло не так...", botClient, cancellationToken, message.Chat.Id);
                            }
                        }
                        else
                        {
                            await messageSend("Ну так он и так в чате, и разбанен.", botClient, cancellationToken, message.Chat.Id);
                        }
                    }
                    else
                    {
                        await messageSend($"Косяк! может ты не правильно упомянул? за репортами сюда - @ArchisErrors", botClient, cancellationToken, message.Chat.Id);
                    }
                }
            }
            else
            {
                await messageSend("Ты должен упомянуть через @ или на прямую", botClient, cancellationToken, message.Chat.Id);
            }
        }
        async internal static Task muteUser(Message message, MessageEntity[] entities, ITelegramBotClient botClient,
                                  CancellationToken cancellationToken, Client app_client)
        {
            var chatId = message.Chat.Id;
            var mutedPerms = new ChatPermissions
            {
                CanSendMessages = false,
                CanAddWebPagePreviews = false,
                CanSendMediaMessages = false,
                CanChangeInfo = false,
                CanSendPolls = false,
                CanSendOtherMessages = false,
                CanInviteUsers = false,
                CanPinMessages = false
            };
            if (entities != null)
            {
                if (entities[0] != null && entities[0].User != null)
                {
                    try
                    {
                        await botClient.RestrictChatMemberAsync(message.Chat.Id, entities[0].User.Id, mutedPerms);
                        await messageSend("Пользователь успешно был замучен", botClient, cancellationToken, message.Chat.Id);
                    }
                    catch 
                    {
                        await messageSend("Походу это админ... или какае-то другая ошибка", botClient, cancellationToken, message.Chat.Id);
                    }
                }
                else if (entities != null && entities[0].Type.ToString().Equals("Mention"))
                {
                    string userName = message.Text.Substring(11);
                    long UserInfo = await DLC.GetUserIdByUsernameAsync(userName, "5702729864:AAEkEyynxSnRcS4pe7C7gcA0eiThSCcwzlA", app_client);
                    var UserId = UserInfo;
                    try
                    {
                        if (UserId.Equals(message.From.Id))
                        {
                            await messageSend($"`Хе-Хе неплохо... Я бы мог предупредить но иначе вы бы не поняли что упускаете\n" +
                                $"И так прямо сейчас, на ваших глазах пользователь {GetFullName(message.From)} замутил сам себя\n" +
                                "Совет на будущее - больше так не делай. Давайте ребята, верните ему голос! Я не сделаю это за вас`\n", botClient, cancellationToken, UserId);
                            await messageSend("Скажи спасибо за это, создателю бота @I_am_Linuxoid", botClient, cancellationToken, chatId);
                        }
                        await botClient.RestrictChatMemberAsync(message.Chat.Id, UserId, mutedPerms);
                        await messageSend("Пользователь успешно был замучен", botClient, cancellationToken, chatId);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                        await messageSend("Походу это админ... Или какае-то другая ошибка", botClient, cancellationToken, chatId);
                    }
                }
            }
            else
            {
                await messageSend("Используй упоминания пользователя через @ или ссылкой", botClient, cancellationToken, chatId);
            }
        }
        async internal static Task unmuteUser(Message message, MessageEntity[] entities, ITelegramBotClient botClient,
                                  CancellationToken cancellationToken, Client app_client)
        {
            var chatId = message.Chat.Id;
            var unMutedPerms = new ChatPermissions
            {
                CanSendMessages = true,
                CanAddWebPagePreviews = true,
                CanSendMediaMessages = true,
                CanChangeInfo = false,
                CanSendPolls = true,
                CanSendOtherMessages = true,
                CanInviteUsers = true,
                CanPinMessages = false
            };
            if (entities != null)
            {
                if (entities[0] != null && entities[0].User != null)
                {
                    try
                    {
                        await botClient.RestrictChatMemberAsync(message.Chat.Id, entities[0].User.Id, unMutedPerms);
                        await messageSend("Пользователь успешно был раззамучен", botClient, cancellationToken, chatId);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        await messageSend("Бред какой-то", botClient, cancellationToken, chatId);
                    }
                }
                else if (entities != null && entities[0].Type.ToString().Equals("Mention"))
                {
                    string userName = message.Text.Substring(13);
                    long UserInfo = await DLC.GetUserIdByUsernameAsync(userName, "5702729864:AAEkEyynxSnRcS4pe7C7gcA0eiThSCcwzlA", app_client);
                    var UserId = UserInfo;
                    try
                    {
                        await botClient.RestrictChatMemberAsync(message.Chat.Id, UserId, unMutedPerms);
                        await messageSend("Пользователь успешно был раззамучен", botClient, cancellationToken, chatId);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                        await messageSend("Пользователь успешно был раззамучен", botClient, cancellationToken, chatId);
                    }
                }
            }
            else
            {
                await messageSend("Используй упоминания пользователя через @ или ссылкой", botClient, cancellationToken, chatId);
            }
        }
        async internal static Task chownFull(Message message, MessageEntity[] entities, ITelegramBotClient botClient,
                                  CancellationToken cancellationToken, Client app_client)
        {
            var chatId = message.Chat.Id;
            if (entities != null)
            {
                string userName = message.Text.Substring(17);
                if (entities[0] != null && entities[0].User != null)
                {
                    try
                    {
                        await botClient.PromoteChatMemberAsync(message.Chat.Id, entities[0].User.Id, canManageChat: true, canRestrictMembers: true, canDeleteMessages: true, canPromoteMembers: true, canManageVideoChats: true, canInviteUsers: true);
                        await messageSend("Пользователь был повышен", botClient, cancellationToken, chatId);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString() + "\nWORKING CONTINUE");
                        await messageSend("Что-то пошло не так, репорты сюда - @ArchisErrors", botClient, cancellationToken, chatId);
                    }
                }
                else if (entities != null && entities[0].Type.ToString().Equals("Mention"))
                {
                    long UserInfo = await DLC.GetUserIdByUsernameAsync(userName, "5702729864:AAEkEyynxSnRcS4pe7C7gcA0eiThSCcwzlA", app_client);
                    var UserId = UserInfo;
                    try
                    {
                        await botClient.PromoteChatMemberAsync(message.Chat.Id, UserId, canManageChat: true, canRestrictMembers: true, canDeleteMessages: true, canPromoteMembers: true);
                        await messageSend("Пользователь был повышен", botClient, cancellationToken, chatId);
                    }
                    catch
                    {
                        await messageSend("Фигня какае-то", botClient, cancellationToken, chatId);
                    }
                }
            }
            else
            {
                await messageSend("Используй упоминания пользователя через @ или ссылкой", botClient, cancellationToken, chatId);
            }
        }
        async internal static Task chownNofull(Message message, MessageEntity[] entities, ITelegramBotClient botClient,
                                  CancellationToken cancellationToken, Client app_client)
        {
            var chatId = message.Chat.Id;
            if (entities != null)
            {
                string userName = message.Text.Substring(19);
                if (entities[0] != null && entities[0].User != null)
                {
                    try
                    {
                        await botClient.PromoteChatMemberAsync(message.Chat.Id, entities[0].User.Id, canManageChat: true, canRestrictMembers: true, canDeleteMessages: true, canPromoteMembers: false, canChangeInfo: false);
                        await messageSend("пользователь был повышен", botClient, cancellationToken, chatId);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString() + "\nWORKING CONTINUE");
                        await messageSend("Что-то пошло не так, репорты сюда - @ArchisErrors", botClient, cancellationToken, chatId);
                    }
                }
                else if (entities != null && entities[0].Type.ToString().Equals("Mention"))
                {
                    long UserId = await DLC.GetUserIdByUsernameAsync(userName, "5702729864:AAEkEyynxSnRcS4pe7C7gcA0eiThSCcwzlA", app_client);
                    await botClient.PromoteChatMemberAsync(message.Chat.Id, UserId, canManageChat: true, canRestrictMembers: true, canDeleteMessages: true, canPromoteMembers: false, canChangeInfo: false);
                    await messageSend("Пользователь успешно был повышен", botClient, cancellationToken, chatId);
                }
            }
            else
            {
                await messageSend("Используй упоминания пользователя через @ или ссылкой", botClient, cancellationToken, chatId);
            }
        }
        async internal static Task touch(Message message, ITelegramBotClient botClient,
                                  CancellationToken cancellationToken)
        {

            var inviteLink = await botClient.CreateChatInviteLinkAsync(message.Chat.Id);
            await messageSend($"{inviteLink.InviteLink} - отправте эту ссылку кому-то чтообы пригласить его в этот чат", botClient, cancellationToken, message.Chat.Id);
        }
        async internal static Task chownGet(Message message, MessageEntity[] entities, ITelegramBotClient botClient,
                                  CancellationToken cancellationToken, Client app_client)
        {
            var chatId = message.Chat.Id;
            string userName = message.Text.Substring(16);
            if (entities != null)
            {
                if (entities[0] != null && entities[0].User != null)
                {
                    try
                    {
                        var member = await botClient.GetChatMemberAsync(chatId, entities[0].User.Id);
                        string userPerms = GetFullPermissionList(member);
                        await messageSend($"{userPerms}", botClient, cancellationToken, chatId);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString() + "\nWORKING CONTINUE");
                        await messageSend("Что-то пошло не так, репорты сюда - @ArchisErrors", botClient, cancellationToken, chatId);
                    }
                }
                else if (entities != null && entities[0].Type.ToString().Equals("Mention"))
                {
                    long UserInfo = await DLC.GetUserIdByUsernameAsync(userName, "5702729864:AAEkEyynxSnRcS4pe7C7gcA0eiThSCcwzlA", app_client);
                    var UserId = UserInfo;
                    try
                    {
                        var member = await botClient.GetChatMemberAsync(chatId, UserId);
                        string userPerms = GetFullPermissionList(member);
                        await messageSend(userPerms, botClient, cancellationToken, chatId);
                        Console.WriteLine(userPerms);
                    }
                    catch
                    {
                        await messageSend("Фигня каке-то, репорты сюда - @ArchisErrors", botClient, cancellationToken, chatId);
                    }
                }
            }
        }
        async internal static Task chownNo(Message message, MessageEntity[] entities, ITelegramBotClient botClient,
                                  CancellationToken cancellationToken, Client app_client)
        {
            var chatId = message.Chat.Id;
            if (entities != null)
            {
                string userName = message.Text.Substring(17);
                if (entities[0] != null && entities[0].User != null)
                {
                    try
                    {
                        await botClient.PromoteChatMemberAsync(message.Chat.Id, entities[0].User.Id, canManageChat: false, canRestrictMembers: false, canDeleteMessages: false, canPromoteMembers: false, canChangeInfo: false);
                        await messageSend("пользователь был понижен", botClient, cancellationToken, chatId);
                    }
                    catch 
                    {
                        await messageSend("Что-то пошло не так, репорты сюда - @ArchisErrors", botClient, cancellationToken, chatId);
                    }
                }
                else if (entities != null && entities[0].Type.ToString().Equals("Mention"))
                {
                    long UserId = await DLC.GetUserIdByUsernameAsync(userName, "5702729864:AAEkEyynxSnRcS4pe7C7gcA0eiThSCcwzlA", app_client);
                    await botClient.PromoteChatMemberAsync(message.Chat.Id, UserId, canManageChat: false, canRestrictMembers: false, canDeleteMessages: false, canPromoteMembers: false, canChangeInfo: false);
                    await messageSend("Пользователь успешно был понижен", botClient, cancellationToken, chatId);
                }
            }
            else
            {
                await messageSend("Используй упоминания пользователя через @ или ссылкой", botClient, cancellationToken, chatId);
            }
        }
        async internal static Task messageSend(string text, ITelegramBotClient botClient, CancellationToken cancellationToken, ChatId chatId)
        {
            Message sentMessage = await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: text,
                            cancellationToken: cancellationToken);
        }

        internal static string GetFullPermissionList(ChatMember member)
        {
            var fullList = "";
            if (member.Status.ToString().Contains("Administrator"))
            {
                var adminMember = member as ChatMemberAdministrator;
                string isFullAdmin = (adminMember.CanPromoteMembers && adminMember.CanRestrictMembers) ? "Имеет полную сборку" : "не имеет поолной сборки";
                string canBan = (adminMember.CanRestrictMembers) ? "Может банить" : "не может банить";
                string canPromote = (adminMember.CanPromoteMembers) ? "может повышать" : "не может повышать";
                string canManage = (adminMember.CanManageChat) ? "может смотреть логи чата" : "не имеет досткпа к менеджменту чата";
                string canChange = (adminMember.CanChangeInfo) ? "может менять инфо о групе" : "не может менять инфо о групе";
                string canDelete = (adminMember.CanDeleteMessages) ? "может удалять сообщения" : "не может удалять сообщения";
                fullList += $"$Администратор - {isFullAdmin};\nпо списку\n-- {canBan};\n-- {canPromote};\n-- {canManage};\n-- {canChange};\n-- {canDelete};\nЭто не вся информация, но вы задолбётесь читать:)";
            }
            else if (member.Status.ToString().Contains("Creator"))
            {
                fullList += $"$Владелец - имеет все права;";
            }
            else
            {
                fullList += "Участник или даже кикнутый";
            }
            return fullList;
        }
        internal static string GetFullInfoAboutUser(ChatMember member)
        {
            string ChatStatusRu = member.Status.ToString().Contains("Administrator") || member.Status.ToString().Contains("Creator") ? "Администратор либо владелец" : "Участник";
            string fullInfo = "Такс @" + member.User.Username + "\nПолное имя -- " + GetFullName(member);
            fullInfo += "\nПоложение в чате -- " + ChatStatusRu + $"\nid пользователя -- {member.User.Id}";
            return fullInfo;
        }
        internal static string GetFullName(ChatMember member)
        {
            string FullName = member.User.FirstName + member.User.LastName;
            return FullName;
        }
        internal static string GetFullName(User member)
        {
            string FullName = member.FirstName + member.LastName;
            return FullName;
        }
        internal static string GetFullName(Telegram.Bot.Types.Chat chat) {
            string FullName = chat.FirstName + chat.LastName;
            return FullName;
        }
        internal static bool IsHaveBanPerms(ChatMember member)
        {
            var adminMember = member as ChatMemberAdministrator;
            var ownerMember = member as ChatMemberOwner;
            if (adminMember == null && ownerMember == null) return false;
            if (ownerMember != null) return true;
            if (adminMember != null && adminMember.CanRestrictMembers) return true;
            else return false;
            return true;
        }
        internal static bool IsHavePromotePerms(ChatMember member)
        {
            var adminMember = member as ChatMemberAdministrator;
            var ownerMember = member as ChatMemberOwner;
            if (adminMember == null && ownerMember == null) return false;
            if (ownerMember != null) return true;
            if (adminMember != null && adminMember.CanPromoteMembers) return true;
            else return false;
        }

    }
}
