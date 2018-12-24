using System.Threading.Tasks;

namespace Utils.Interfaces
{
    public interface ITelegramBot
    {
        Task SendMessageAsync(string message, int chatId);
    }
}