using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectionApplication.Models
{
    public class VoteTracker
    {
        public int VoteTrackerId { get; set; }

        public int ElectionId { get; set; }

        public int? VoterId { get; set; }

        public bool HasVoted { get; set; }

        public bool HasLeaned { get; set; }    
    }
}