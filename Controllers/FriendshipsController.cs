﻿using ChatManager.Models;
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
            return Json(DB.Friendships.ToList().Where(m => m.IdUser1 == id || m.IdUser2 == id));
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
            DB.Friendships.AddFriendship(OnlineUsers.GetSessionUser().Id, id);

            return RedirectToAction("Index");
        }
    }
}