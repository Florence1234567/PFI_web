using ChatManager.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI;

namespace ChatManager.Controllers
{
    public class FriendshipsController : Controller
    {
        [OnlineUsers.UserAccess]
        public ActionResult Index()
        {
            int id = OnlineUsers.GetSessionUser().Id;
            var friendship = DB.Friendships.ToList().Where(m => m.IdUser1 == id || m.IdUser2 == id);
            ViewBag.Friendship = friendship;


            IEnumerable<User> users;
            if (Session["SearchText"] != null)
                if ((string)Session["SearchText"] == "")
                    users = DB.Users.ToList();
                else
                {
                    string searchText = Session["SearchText"]?.ToString()?.ToLower();
                    users = DB.Users.ToList().Where(m => m.FirstName.ToLower().Contains(searchText) || m.LastName.ToLower().Contains(searchText));
                }

            else
                users = DB.Users.ToList();

                return View(users);
        }
        [OnlineUsers.UserAccess]
        public ActionResult FriendshipsList()
        {
            int id = OnlineUsers.GetSessionUser().Id;
            var friendship = DB.Friendships.ToList().Where(m => m.IdUser1 == id || m.IdUser2 == id);
            ViewBag.Friendship = friendship;

            IEnumerable<User> users;

            if(Session["SearchText"] != null)
                if ((string)Session["SearchText"] == "")
                    users = DB.Users.ToList();
                else
                {
                    string searchText = Session["SearchText"]?.ToString()?.ToLower();
                    users = DB.Users.ToList().Where(m => m.FirstName.ToLower().Contains(searchText) || m.LastName.ToLower().Contains(searchText));

                }
            else
                users = DB.Users.ToList();

            return PartialView(users);
        }
        public ActionResult SetSearchText(string text)
        {
            Session["SearchText"] = text;
            return RedirectToAction("Index");
        }
        public ActionResult SetFilterNotFriend(bool isChecked)
        {
            Session["FilterNotFriend"] = isChecked;
            return RedirectToAction("Index");
        }

        public ActionResult SetFilterBlocked(bool isChecked)
        {
            Session["FilterBlocked"] = isChecked;
            return RedirectToAction("Index");
        }

        public ActionResult SendFriendshipRequest(int id)
        {
            var friendships = DB.Friendships.ToList().Where(m => m.IdUser1 == id && m.IdUser2 == id ||
            m.IdUser1 == OnlineUsers.GetSessionUser().Id && m.IdUser2 == OnlineUsers.GetSessionUser().Id);

            if (friendships.Count() != 0)
                DB.Friendships.Delete(friendships.First().Id);

            DB.Friendships.Create(new Friendship(OnlineUsers.GetSessionUser().Id, id));

            OnlineUsers.AddNotification(id, $"Vous avez reçu une demande d'amitié de {DB.Users.Get(id).FirstName} {DB.Users.Get(id).LastName}");


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

        public ActionResult RemoveFriendshipRequest1(int id)
        {
            var friendships = DB.Friendships.ToList().Where(m => m.IdUser1 == OnlineUsers.GetSessionUser().Id && m.IdUser2 == id);

            DB.Friendships.Delete(friendships.First().Id);

            return RedirectToAction("Index");
        }

        public ActionResult RemoveFriendshipRequest2(int id)
        {
            var friendships = DB.Friendships.ToList().Where(m => m.IdUser1 == id && m.IdUser2 == OnlineUsers.GetSessionUser().Id);

            DB.Friendships.Delete(friendships.First().Id);

            return RedirectToAction("Index");
        }
    }
}