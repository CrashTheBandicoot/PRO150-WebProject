using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientApp.Models
{
    public class MyObject
    {
        public int gameId { get; set; }
        public int playerId { get; set; }
        public string error { get; set; }
        public bool success { get; set; }
    }
}