using System.Runtime.InteropServices;

namespace MammaaksaApp.Models
{
    public class Mamaaksa
    {
        public int Id { get; set; }
        //public string username { get; set; }    
        public string gaaffii { get; set; }
        //[Optional]
        public string deebii { get; set; }
    }
    public class Account
    {
        public int Id { get; set; }
        public string maqaa { get; set; }
        public string jecha_icciti { get; set; }
    }
}
