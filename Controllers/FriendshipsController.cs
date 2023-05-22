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
        {
            return Json(DB.Users.FindUser(id).Status);
        }

        [OnlineUsers.UserAccess]
        public ActionResult Index()
        {
            /*if (Session["FriendshipFilter"] == null)
                Session["FriendshipFilter"] = "NotFriend";*/
            return View(DB.Users.ToList());
        }
        public ActionResult SetFriendshipFilter(int id)
        {
            Session["FriendshipFilter"] = id;
            return RedirectToAction("Index");
        }

        public ActionResult SendFriendshipRequest(int id)
        {
            User currentUser = OnlineUsers.GetSessionUser();
            currentUser.Status = 2;
            DB.Users.Update(currentUser);

            User user = DB.Users.FindUser(id);
            user.Status = 1;
            DB.Users.Update(user);

            return RedirectToAction("Index");
        }
    }
}