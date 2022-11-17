using System;
using WTelegram;
using TL;

namespace ArchiV1
{
    internal class DLC
    {
        private static int api_id = 2919669;
        private static string api_hash = "0e858678d2ff61c95a6993799d895c25";

        private static Client app_client = new Client(api_id, api_hash); //Инициализация клиента

        public static async Task<long> GetUserIdByUsernameAsync(string username, string token, Client client) //создаём метод, который будет принимать токен бота, клиента и юзернейм, а дальше в проекте просто вызываем этот метод
        {
            try
            {
                await client.LoginBotIfNeeded(token);
                var user = await client.Contacts_ResolveUsername(username);
                long userData = user.User.id;
                await client.Auth_LogOut();
                return userData;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
                return 0;
            }
        }
    }
}
