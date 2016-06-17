using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ElectionApplication.Models
{

        [Table("UserProfile")]
        public class VoterProfile
        {
            [Key]
            [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
            public int UserId { get; set; }
            public string UserName { get; set; }
            public string Party { get; set; }
            public int? HasVotedIn { get; set; }
            public string UserEmail { get; set; }
        }
    
}