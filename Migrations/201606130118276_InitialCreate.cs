namespace ElectionApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Party = c.String(),
                        HasVotedIn = c.Int(nullable: false),
                        UserEmail = c.String(),
                        Election_ElectionId = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Elections", t => t.Election_ElectionId)
                .Index(t => t.Election_ElectionId);
            
            CreateTable(
                "dbo.Elections",
                c => new
                    {
                        ElectionId = c.Int(nullable: false, identity: true),
                        ElectionName = c.String(),
                        IsEnded = c.Boolean(nullable: false),
                        IsSelected = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ElectionId);
            
            CreateTable(
                "dbo.Candidates",
                c => new
                    {
                        CandidateId = c.Int(nullable: false, identity: true),
                        CandidateName = c.String(),
                        ElectionName = c.String(),
                        ElectionId = c.Int(nullable: false),
                        Party = c.String(),
                        PartyId = c.Int(nullable: false),
                        Votes = c.Int(nullable: false),
                        LeanAmt = c.Int(nullable: false),
                        PredictedVoteCount = c.Double(nullable: false),
                        HasWon = c.Boolean(nullable: false),
                        Platform = c.String(),
                        IsSelected = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CandidateId);
            
            CreateTable(
                "dbo.Parties",
                c => new
                    {
                        PartyId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsSelected = c.Boolean(nullable: false),
                        Election_ElectionId = c.Int(),
                    })
                .PrimaryKey(t => t.PartyId)
                .ForeignKey("dbo.Elections", t => t.Election_ElectionId)
                .Index(t => t.Election_ElectionId);
            
            CreateTable(
                "dbo.CandidateElections",
                c => new
                    {
                        Candidate_CandidateId = c.Int(nullable: false),
                        Election_ElectionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Candidate_CandidateId, t.Election_ElectionId })
                .ForeignKey("dbo.Candidates", t => t.Candidate_CandidateId, cascadeDelete: true)
                .ForeignKey("dbo.Elections", t => t.Election_ElectionId, cascadeDelete: true)
                .Index(t => t.Candidate_CandidateId)
                .Index(t => t.Election_ElectionId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.CandidateElections", new[] { "Election_ElectionId" });
            DropIndex("dbo.CandidateElections", new[] { "Candidate_CandidateId" });
            DropIndex("dbo.Parties", new[] { "Election_ElectionId" });
            DropIndex("dbo.UserProfile", new[] { "Election_ElectionId" });
            DropForeignKey("dbo.CandidateElections", "Election_ElectionId", "dbo.Elections");
            DropForeignKey("dbo.CandidateElections", "Candidate_CandidateId", "dbo.Candidates");
            DropForeignKey("dbo.Parties", "Election_ElectionId", "dbo.Elections");
            DropForeignKey("dbo.UserProfile", "Election_ElectionId", "dbo.Elections");
            DropTable("dbo.CandidateElections");
            DropTable("dbo.Parties");
            DropTable("dbo.Candidates");
            DropTable("dbo.Elections");
            DropTable("dbo.UserProfile");
        }
    }
}
