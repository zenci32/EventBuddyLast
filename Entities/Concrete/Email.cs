namespace Entities.Concrete
{
    public class Email
    {
        public int Id { get; set; }
        public string Smtp { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public bool SSL { get; set; }
        public bool Html { get; set; }
    }
}
