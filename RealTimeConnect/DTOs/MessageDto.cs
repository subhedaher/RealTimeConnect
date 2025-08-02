﻿namespace RealTimeConnect.DTOs
{
    public class MessageDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public int SenderId { get; set; }
        public string SenderName { get; set; } = string.Empty;
        public int ReceiverId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
