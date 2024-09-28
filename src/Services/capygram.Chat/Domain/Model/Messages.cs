namespace capygram.Chat.Domain.Model
{
    public class Messages
    {
        public Guid Id {  get; set; }
        public Guid Sender { get; set; }
        public Guid Receiver { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsSenderDeleted { get; set; } = false;
        public bool IsReceiverDeleted { get; set; } = false;
    }
}
