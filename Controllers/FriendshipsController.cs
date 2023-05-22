﻿using ChatManager.Models;
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
        public JsonResult GetFriendshipStatus(int id)
        {
            return Json(DB.Users.FindUser(id).Status);
        }

        [OnlineUsers.UserAccess]
        public ActionResult Index()
        {
            if (Session["FriendshipFilter"] == null)
                Session["FriendshipFilter"] = "NotFriend";
            return View(DB.Users.ToList().OrderBy(m => m.FirstName).ThenBy(m => m.LastName).Where(m => m.Status == (string)Session["FriendshipFilter"]));
        }
        public ActionResult SetFriendshipStatus(int id)
        {
            Session["FriendshipFilter"] = id;
            return RedirectToAction("Index");
        }

    }
}