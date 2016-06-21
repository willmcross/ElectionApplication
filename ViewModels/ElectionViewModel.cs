using ElectionApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectionApplication.ViewModels
{
    public class ElectionViewModel
    {


        public int CandidateId { get; set; }
        [Display(Name = "Candidate Name")]
        public string CandidateName { get; set; }

        public string Party { get; set; }
        [Display(Name = "Election Name")]
        public string ElectionName { get; set; }

        public int ElectionId { get; set; }

        public IEnumerable<SelectListItem> Elections { get; set; }

        public SelectList ElectionList { get; set; }

    }
}