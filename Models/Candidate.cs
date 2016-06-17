using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectionApplication.Models
{
    public class Candidate
    {

        public int CandidateId { get; set; }

        public string CandidateName { get; set; }

        public string ElectionName { get; set; }

        public int ElectionId { get; set; }

        public string Party { get; set; }

        public int PartyId { get; set; }

        public int Votes { get; set; }

        public virtual ICollection<Election> Elections { get; set; }

        public int LeanAmt { get; set; }

        public double PredictedVoteCount { get; set; }

        public bool HasWon { get; set; }

        public string Platform { get; set; }

        public bool IsSelected { get; set; }
    }
}