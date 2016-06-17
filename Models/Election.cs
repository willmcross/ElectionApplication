using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectionApplication.Models
{
    public class Election
    {

        public int ElectionId { get; set; }

        public string ElectionName { get; set; }

        public DateTime TimeLimit { get; set; }

        public bool IsEnded { get; set; }

        public bool IsSelected { get; set; }

        public virtual ICollection<Candidate> Candidates { get; set; }

        public virtual ICollection<VoterProfile> Voters { get; set; }

        public virtual ICollection<Party> Parties { get; set; }


    }
}