using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ElectionApplication.Models
{
    public class ElectionApplicationDb : DbContext
    {

        public ElectionApplicationDb() : base("name=DefaultConnection")
        {

        }


        public DbSet<VoterProfile> VoterProfiles { get; set; }

        public DbSet<Election> Elections { get; set; }

        public DbSet<Candidate> Candidates { get; set; }

        public DbSet<Party> Parties { get; set; }

        public DbSet<VoteTracker> Votes { get; set; }
    }
}