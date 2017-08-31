using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientApp.Models
{
    public class MoveModel
    {
        public List<SelectListItem> Moves { get; set; }
        public string Move { get; set; }
        public List<SelectListItem> Pieces { get; set; }
        public string Piece { get; set; }
        public MoveModel()
        {

        }
    }
}