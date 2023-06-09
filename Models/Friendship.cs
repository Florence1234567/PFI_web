﻿using Newtonsoft.Json;

namespace ChatManager.Models
{
    public class Friendship
    {
        public Friendship()
        {

        }

        public Friendship(int idUser1, int idUser2)
        {
            IdUser1 = idUser1;
            IdUser2 = idUser2;
            Status = "Requested";
        }

        public Friendship Clone()
        {
            return JsonConvert.DeserializeObject<Friendship>(JsonConvert.SerializeObject(this));
        }
        public int Id { get; set; }
        public int IdUser1 { get; set; }
        public int IdUser2 { get; set; }
        public string Status { get; set; }
    }
}