namespace SupBB.Model
{
    public class Messages
    {
        public string _recipient = "suppbaltbet@gmail.com"; //fcgbhbyBB
        public string _sender = "supbaltbet@gmail.com"; //fcgbhbyBB // sbaltbet@mail.ru // supbaltbet@gmail.com // sbaltbet@mail.ru
        public string TextMessage { get; set; }

        public string Recipient()
        {
            return _recipient;
        }
        public string Sender()
        {
            return _sender;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
