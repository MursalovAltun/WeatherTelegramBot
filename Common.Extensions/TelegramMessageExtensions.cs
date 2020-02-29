using Telegram.Bot.Types;

namespace Common.Extensions
{
    public static class TelegramMessageExtensions
    {
        public static long GetChatId(this Message message)
        {
            return message.Chat.Id;
        }

        public static User GetUser(this Message message)
        {
            return message.From;
        }

        public static string GetUserUsername(this Message message)
        {
            return message.From.Username;
        }

        public static int GetUserTelegramId(this Message message)
        {
            return message.From.Id;
        }
    }
}