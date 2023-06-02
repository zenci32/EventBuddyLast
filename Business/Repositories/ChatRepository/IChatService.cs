using Core.Utilities.Result.Abstract;
using Entities.Concrete;

namespace Business.Repositories.ChatRepository
{
    public interface IChatService
    {
        Task<IResult> Add(string phone, string message,int eventId);
        Task<List<MessageChat>> GetList();
    }
}
