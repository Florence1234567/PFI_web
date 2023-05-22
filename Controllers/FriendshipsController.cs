using ChatManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatManager.Controllers
{
    public class FriendshipsController : Controller
    {
        [HttpPost]
        public JsonResult GetFriendshipStatus(int Id)
        {
            return Json(DB.Users);
        }

        [OnlineUsers.UserAccess]
        public ActionResult Index()
        {
            return View(DB.Users.ToList().OrderBy(m => m.FirstName).ThenBy(m => m.LastName));
        }
    }
}