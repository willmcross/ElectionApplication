using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElectionApplication.ViewModels
{
    public class CandidateViewModel
    {
        [Display(Name = "Candidate Name")]
        public string CandidateName { get; set; }
        [Display(Name = "Election Name")]
        public string ElectionName { get; set; }

        public string Party { get; set; }

        public string Platform { get; set; }

        public int CandidateId { get; set; }

        public int ElectionId { get; set; }

        public int VoterId { get; set; }

        public int Leans { get; set; }

        public int Votes { get; set; }

	public float TotalVotes { get; set; }
    }
}