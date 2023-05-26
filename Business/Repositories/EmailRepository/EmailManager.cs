using Business.Repositories.EmailRepository.Constans;
using Business.Repositories.EmailRepository.Validation;
using Core.Aspects.Validation;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Repositories.EmailRepository;
using Entities.Concrete;
using System.Net;
using System.Net.Mail;

namespace Business.Repositories.EmailRepository
{
    public class EmailManager : IEmailService
    {
        private readonly IEmailDal _emailParameterDal;

        public EmailManager(IEmailDal emailParameterDal)
        {
            _emailParameterDal = emailParameterDal;
        }

        [ValidationAspect(typeof(EmailParameterValidator))]
        public async Task<IResult> Add(Email emailParameter)
        {
            await _emailParameterDal.Add(emailParameter);
            return new SuccessResult(EmailParameterMessages.AddedEmailParameter,200);

        }

        public async Task<IResult> Delete(Email emailParameter)
        {
            await _emailParameterDal.Delete(emailParameter);
            return new SuccessResult(EmailParameterMessages.DeletedEmailParameter,200);
        }

        public async Task<IDataResult<Email>> GetById(int id)
        {
            return new SuccessDataResult<Email>(await _emailParameterDal.Get(p => p.Id == id));
        }

        public async Task<Email> GetFirst()
        {
            return await _emailParameterDal.GetFirst();
        }

        public async Task<IDataResult<List<Email>>> GetList()
        {
            return new SuccessDataResult<List<Email>>(await _emailParameterDal.GetAll());
        }

        public async Task<IResult> SendEmail(Email emailParameter, string body, string subject, string emails)
        {
            using (MailMessage mail = new MailMessage())
            {
                string[] setEmails = emails.Split(",");
                mail.From = new MailAddress(emailParameter.Mail);
                foreach (var email in setEmails)
                {
                    mail.To.Add(email);
                }
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = emailParameter.Html;
                //mail.Attachments.Add();
                using (SmtpClient smtp = new SmtpClient(emailParameter.Smtp))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(emailParameter.Mail, emailParameter.Password);
                    smtp.EnableSsl = emailParameter.SSL;
                    smtp.Port = emailParameter.Port;
                    await smtp.SendMailAsync(mail);
                }
            }
            return new SuccessResult(EmailParameterMessages.EmailSendSuccesiful,200);

        }

        [ValidationAspect(typeof(EmailParameterValidator))]
        public async Task<IResult> Update(Email emailParameter)
        {
            await _emailParameterDal.Update(emailParameter);
            return new SuccessResult(EmailParameterMessages.UpdatedEmailParameter,200);
        }
    }
}
