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
            Date = DateTime.Now.ToString("dd MMMM yyyy - HH:mm");
        }

        public int Id { get; set; }
        public int Sender { get; set; }
        public int Receiver { get; set; }
        public string Message { get; set; }
        public string Date { get; set; }
    }
}