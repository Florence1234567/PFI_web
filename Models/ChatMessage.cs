using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatManager.Models
{
    public class ChatMessage
    {
        public ChatMessage() { }
        public ChatMessage(int idUser1, int idUser2, string message)
        {
            Sender = idUser1;
            Receiver = idUser2;
            Message = message;
        }

        public int Id { get; set; }
        public int Sender { get; set; }
        public int Receiver { get; set; }
        public string Message { get; set; }
    }
}