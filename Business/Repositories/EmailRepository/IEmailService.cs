using Core.Utilities.Result.Abstract;
using Entities.Concrete;

namespace Business.Repositories.EmailRepository
{
    public interface IEmailService
    {
        Task<IResult> Add(Email emailParameter);
        Task<IResult> Update(Email emailParameter);
        Task<IResult> Delete(Email emailParameter);
        Task<IDataResult<List<Email>>> GetList();
        Task<IDataResult<Email>> GetById(int id);
        Task<Email> GetFirst();
        Task<IResult> SendEmail(Email emailParameter, string body, string subject, string emails);
    }
}
