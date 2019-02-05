namespace Kali.Common.Domains
{
    public class Message
    {
        public Message(string text, MessageType type)
        {
            Text = text;

            Type = type;
        }

        public string Text { get; set; }

        public string Code { get; set; }

        public MessageType Type { get; set; }
    }
}
