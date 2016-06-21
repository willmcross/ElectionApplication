using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElectionApplication.Models
{
    public class Candidate
    {

        public int CandidateId { get; set; }
        [Display(Name = "Candidate Name")]
        public string CandidateName { get; set; }
        [Display(Name = "Election Name")]
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