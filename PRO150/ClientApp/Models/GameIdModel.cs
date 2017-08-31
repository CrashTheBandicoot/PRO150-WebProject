using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientApp.Models
{
    public class GameIdModel
    {
        public int gameId { get; set; }
        public List<SelectListItem> GameChoices { get; set; }
        public GameIdModel()
        {

        }
    }
}