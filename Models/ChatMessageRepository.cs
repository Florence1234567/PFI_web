using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatManager.Models
{
    public class ChatMessageRepository : Repository<ChatMessage>
    {
        public ChatMessage Create(ChatMessage chatMessage)
        {
            try
            {
                chatMessage.Id = base.Add(chatMessage);
                return chatMessage;
            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine($"Add chatmessage failed : Message - {ex.Message}");
            }
            return null;
        }

        public override bool Update(ChatMessage chatMessage)
        {
            try
            {
                return base.Update(chatMessage);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Update chatMessage failed : Message - {ex.Message}");
            }
            return false;
        }

        public override bool Delete(int chatMessageId)
        {
            try
            {
                ChatMessage chatMessageToDelete = DB.ChatMessages.Get(chatMessageId);
                if (chatMessageToDelete != null)
                {
                    BeginTransaction();
                    base.Delete(chatMessageId);
                    EndTransaction();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Remove chatmessage failed : Message - {ex.Message}");
                EndTransaction();
                return false;
            }
        }
    }
}