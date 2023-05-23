using ChatManager.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatManager.Controllers
{
    public class FriendshipsController : Controller
    {
        [HttpPost]
        public JsonResult GetFriendshipStatus(int id)
        { //DB.Friendships.ToList().Where(m => m.IdUser1 == id || m.IdUser2 == id)
            return Json(DB.Users.ToList());
        }

        [OnlineUsers.UserAccess]
        public ActionResult Index()
        {
            int id = OnlineUsers.GetSessionUser().Id;
            var friendship = DB.Friendships.ToList().Where(m => m.IdUser1 == id || m.IdUser2 == id);
            ViewBag.Friendship = friendship;
            return View(DB.Users.ToList());
        }
        public ActionResult SetFriendshipFilter(int id)
        {
            Session["FriendshipFilter"] = id;
            return RedirectToAction("Index");
        }

        public ActionResult SendFriendshipRequest(int id)
        {
            var friendships = DB.Friendships.ToList().Where(m => m.IdUser1 == id && m.IdUser2 == id || m.IdUser1 == OnlineUsers.GetSessionUser().Id && m.IdUser2 == OnlineUsers.GetSessionUser().Id);

            if (friendships != null)
                DB.Friendships.Delete(friendships.First().Id);

            DB.Friendships.Create(new Friendship(OnlineUsers.GetSessionUser().Id, id));

            return RedirectToAction("Index");
        }

        public ActionResult AcceptFriendshipRequest(int id)
        {
            var friendships = DB.Friendships.ToList().Where(m => m.IdUser1 == id && m.IdUser2 == OnlineUsers.GetSessionUser().Id);
            var friendship = DB.Friendships.Get(friendships.First().Id);
            friendship.Status = "Friend";
            DB.Friendships.Update(friendship);

            return RedirectToAction("Index");
        }

        public ActionResult DeclineFriendshipRequest(int id)
        {
            var friendships = DB.Friendships.ToList().Where(m => m.IdUser1 == id && m.IdUser2 == OnlineUsers.GetSessionUser().Id);
            var friendship = DB.Friendships.Get(friendships.First().Id);
            friendship.Status = "Declined";
            DB.Friendships.Update(friendship);

            return RedirectToAction("Index");
        }

        public ActionResult RemoveFriendshipRequest(int id, int order)
        {
            IEnumerable<Friendship> friendships;

            if (order == 1)
                friendships = DB.Friendships.ToList().Where(m => m.IdUser1 == OnlineUsers.GetSessionUser().Id && m.IdUser2 == id);
            else
                friendships = DB.Friendships.ToList().Where(m => m.IdUser1 == id && m.IdUser2 == OnlineUsers.GetSessionUser().Id);

            DB.Friendships.Delete(friendships.First().Id);

            return RedirectToAction("Index");
        }
    }
}