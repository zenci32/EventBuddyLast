using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Repositories.MessageChatRepository;
using DataAccess.Repositories.UserRepository;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.ChatRepository
{
    public class ChatManager : IChatService
    {
        private readonly IUserDal _userDal;
        private readonly IEfMessageChatDal _chatDal;


        public ChatManager(IUserDal userDal, IEfMessageChatDal chatDal)
        {
            _userDal = userDal;
            _chatDal = chatDal;
        }
        public async Task<IResult> Add(string phone,string message,int eventId)
        {
            var user = _userDal.Get(x=>x.Phone == phone).Result;

            var messageChat = new MessageChat
            {
                CreateDate = DateTime.Now,
                EventId = eventId,
                MessageText = message,
                PhoneNumber = phone,
                UserName = user.Name
            };

            await _chatDal.Add(messageChat);

            return new SuccessResult("Başarılı", 200);
        }

        public async Task<List<MessageChat>> GetList()
        {
           return await _chatDal.GetAll();
        }
    }
}
