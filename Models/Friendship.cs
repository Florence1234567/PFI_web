using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatManager.Models
{
    public class Friendship
    {
        public Friendship()
        {
            Status = 0;
        }

        public int IdUser1 { get; set; }
        public int IdUser2 { get; set; }
        public int Status { get; set; }
    }
}