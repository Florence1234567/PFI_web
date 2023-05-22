using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatManager.Models
{
    public class FriendshipRepository : Repository<Friendship>
    {
        public Friendship Create(Friendship friendship)
        {
            try
            {
                friendship.Id = base.Add(friendship);
                return friendship;
            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine($"Add friendship failed : Message - {ex.Message}");
            }
            return null;
        }

        public override bool Update(Friendship friendship)
        {
            try
            {
                return base.Update(friendship);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Update friendship failed : Message - {ex.Message}");
            }
            return false;
        }

        public override bool Delete(int friendshipId)
        {
            try
            {
                Friendship friendshipToDelete = DB.Friendships.Get(friendshipId);
                if (friendshipToDelete != null)
                {
                    BeginTransaction();
                    base.Delete(friendshipId);
                    EndTransaction();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Remove friendship failed : Message - {ex.Message}");
                EndTransaction();
                return false;
            }
        }
    }
}