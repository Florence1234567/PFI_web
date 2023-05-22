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
        public void GetFriendshipStatus(int id)
        {
            //return Json(DB.Friendships.Get().FindUser(id).Status);
        }

        [OnlineUsers.UserAccess]
        public ActionResult Index()
        {
            /*if (Session["FriendshipFilter"] == null)
                Session["FriendshipFilter"] = "NotFriend";*/
            ViewBag.Friendships = SelectListUtilities<Friendship>.Convert(DB.Friendships.ToList());

            return View(DB.Users.ToList());
        }
        public ActionResult SetFriendshipFilter(int id)
        {
            Session["FriendshipFilter"] = id;
            return RedirectToAction("Index");
        }

        public ActionResult SendFriendshipRequest(int id)
        {
            DB.Friendships.Add(new Friendship(OnlineUsers.GetSessionUser().Id, id));

            return View();
        }
    }
}