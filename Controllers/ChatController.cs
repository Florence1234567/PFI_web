using ChatManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace ChatManager.Controllers
{
    public class ChatController : Controller
    {
        [OnlineUsers.UserAccess]
        public ActionResult Index()
        {
            //var users = DB.Users.ToList().Where(user => user.Status == 3)
            //                           .OrderBy(user => user.Status)
            //                           .ToList();
            var users = DB.Users.ToList();

            return View(users);
        }


        [HttpGet]
        public ActionResult GetMessages(int id)
        {


            return View();
        }

        [HttpPost]
        public ActionResult SendMessage(int id, string message)
        {
            var currentUser = OnlineUsers.GetSessionUser();

            var user = DB.Users; 

            return View();
        }
    }
}