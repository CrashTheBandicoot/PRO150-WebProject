using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientApp.Models
{
    public class PlayerModel
    {
        public string Color { get; set; }
        public int GameId { get; set; }
        public List<SelectListItem> ColorChoice { get; set; }
        public PlayerModel()
        {

        }
    }
}