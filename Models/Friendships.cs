using FileKeyReference;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ChatManager.Models
{
    public class Friendships
    {
        public Friendships()
        {
            IdUserConnected = OnlineUsers.GetSessionUser().Id;
        }

        #region Data Members
        public int IdUserConnected { get; set; }

        public int IdUser { get; set; }

        public string Status { get; set; }

        #endregion
    }
}