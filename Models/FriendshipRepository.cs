using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatManager.Models
{
    public class FriendshipRepository : Repository<Friendship>
    {
        public void AddFriendship(int idUser1, int idUser2)
        {
            try
            {
                Friendship friendship = new Friendship(idUser1, idUser2);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Add friendship failed : Message - {ex.Message}");
            }
        }
    }
}