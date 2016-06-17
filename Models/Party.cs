using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectionApplication.Models
{
    public class Party
    {

        public int PartyId { get; set; }

        public string PartyName { get; set; }

        public bool IsSelected { get; set; }

    }
}