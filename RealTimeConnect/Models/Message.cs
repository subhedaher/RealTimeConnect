namespace RealTimeConnect.Models
{
    public class Message
    {
        public int Id { get; set; }

        public string Content { get; set; } = string.Empty;

        public int SenderId { get; set; }
        public User Sender { get; set; } = default!;

        public int ReceiverId { get; set; }
        public User Receiver { get; set; } = default!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
