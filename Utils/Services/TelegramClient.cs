using System;
using System.Net;
using MihaZupan;
using Telegram.Bot;

namespace Utils.Services
{
    public class TelegramClient
    {
        //todo add bot configurator from app.settings
        /// <summary>
        /// Telegram Bot Client
        /// </summary>
        private readonly ITelegramBotClient _botClient;

        public TelegramClient(string socks5Hostname, int socks5Port, string botApiKey, string socks5Username,
            string socks5Password)
        {
            if (botApiKey == null) throw new ArgumentNullException(nameof(botApiKey));
            var socksProxy = new HttpToSocks5Proxy(socks5Hostname, socks5Port)
            {
                Credentials = new NetworkCredential {UserName = socks5Username, Password = socks5Password}
            };
            _botClient = new TelegramBotClient(botApiKey, socksProxy);
        }

        public void SendTextMessageAsync(string chatId, string message)
        {
            try
            {
                _botClient.SendTextMessageAsync(chatId, message);
            }
            catch (Exception e)
            {
                //todo change to projectLevelExceptions and catcher for it* future
                Console.WriteLine("Fail");
            }
        }
    }
}