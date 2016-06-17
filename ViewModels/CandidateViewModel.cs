using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectionApplication.ViewModels
{
    public class CandidateViewModel
    {

        public string CandidateName { get; set; }

        public string ElectionName { get; set; }

        public string Party { get; set; }

        public string Platform { get; set; }

        public int CandidateId { get; set; }

        public int ElectionId { get; set; }

        public int VoterId { get; set; }

        public int Leans { get; set; }

        public int Votes { get; set; }
    }
}