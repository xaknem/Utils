using System.Threading.Tasks;

namespace Utils.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITelegramBot
    {
        Task SendMessageAsync(string message, int chatId);
    }
}