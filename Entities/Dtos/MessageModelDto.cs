namespace WebAPI.Models
{
    public class MessageModelDto
    {

        public string UserName { get; set; }
        public string MessageText { get; set; }
        public string PhoneNumber { get; set; }
        public int EventId { get; set; }
        public int EventRequestId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
