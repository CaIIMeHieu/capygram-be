namespace capygram.Chat.Domain.Model
{
    public class DeleteMessageRequest
    {
        public Guid UserRequest {  get; set; }
        public string Type { get; set; } 
        public Messages Message { get; set; }
    }

    public static class DeleteType
    {
        public static string deleteForEveryOne { get; set; } = nameof(deleteForEveryOne);
        public static string deleteForYourSelf { get; set; } = nameof(deleteForYourSelf);
    }
}
