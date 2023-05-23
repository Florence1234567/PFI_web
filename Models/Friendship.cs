using Newtonsoft.Json;

namespace ChatManager.Models
{
    public class Friendship
    {
        public Friendship()
        {

        }

        public Friendship(int idUser1, int idUser2, int idSentRequest)
        {
            IdUser1 = idUser1;
            IdUser2 = idUser2;
            Status = "Request";
            if (idSentRequest == idUser1)
                PendingUser1 = true;
            else
                PendingUser2 = true;
        }

        public Friendship Clone()
        {
            return JsonConvert.DeserializeObject<Friendship>(JsonConvert.SerializeObject(this));
        }
        public int Id { get; set; }
        public int IdUser1 { get; set; }
        public int IdUser2 { get; set; }
        public string Status { get; set; }
        public bool PendingUser1 { get; set; }
        public bool PendingUser2 { get; set; }
    }
}