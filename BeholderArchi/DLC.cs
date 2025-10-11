using System;
using WTelegram;
using TL;

namespace BeholderArchi
{
    internal class DLC
    {
        private static int api_id = 0;
        private static string api_hash = "0";

        private static Client app_client = new Client(api_id, api_hash);

        public static async Task<long> GetUserIdByUsernameAsync(string username, string token, Client client) //uh-huh
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

