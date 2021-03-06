﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElectionApplication.Models;
using ElectionApplication.ViewModels;
using WebMatrix.WebData;

namespace ElectionApplication.Controllers
{
    public class CandidateController : Controller
    {
        private ElectionApplicationDb db = new ElectionApplicationDb();

        //
        // GET: /Candidate/

        public ActionResult Index([Bind(Prefix = "id")] int ElectionId, VoteTracker votetracker)
        {

            var CheckTimeLimit = db.Elections
                .Where(o => o.ElectionId == ElectionId)
                .All(o => o.TimeLimit <= DateTime.Now);

            if (CheckTimeLimit.Equals(false))
            {


                // assign userid and electionid for query
                var voterId = WebSecurity.GetUserId(User.Identity.Name);
                var electionId = ElectionId;

                // cross reference entire VoteTracker db to see if there is a match
                var querytracker = db.Votes.All(x => x.ElectionId == electionId && x.VoterId == voterId);

                //If no match, create entry with voterId and ElectionId to prevent multiple votes- only ONE per each ElectionId / VoterId combo should be made!
                if (querytracker.Equals(false))
                {
                    votetracker.VoterId = voterId;
                    votetracker.ElectionId = electionId;
                    db.Votes.Add(votetracker);
                    db.SaveChanges();
                }
                //If match, procceed without 


                var model =
                    (from c in db.Candidates
                     join e in db.Elections on c.ElectionId equals ElectionId
                     select new CandidateViewModel
                     {
                         //map data from query to viewmodel
                         CandidateId = c.CandidateId,
                         CandidateName = c.CandidateName,
                         ElectionName = c.ElectionName,
                         ElectionId = c.ElectionId,
                         Party = c.Party,
                         Platform = c.Platform,
                         Votes = c.Votes
                     }).Distinct();
                return View(model);
            }
            else
            {
                return RedirectToAction("ResultsFirstPlace", "Candidate", new { id = ElectionId });

            }

        }

        public ActionResult Results()
        {
            return View();
        }

        //TODO: This wont display properly. Check it out.
        public ActionResult ResultsFirstPlace([Bind(Prefix = "id")] int ElectionId, Candidate candidate)
        {
            // we will assume that no two candidates can have equal votes. If that occurs, I'll just write something up to flip a digital coin like they do in real elections.

            var SumVotes = db.Candidates
                .Where(r => r.ElectionId == ElectionId)
                .Sum(r => r.Votes);

            var model = db.Candidates
                .Where(r => r.ElectionId == ElectionId)
                .OrderByDescending(r => r.Votes)
                .Select(r => new CandidateViewModel
                {
                    CandidateName = r.CandidateName,
                    ElectionName = r.ElectionName,
                    Votes = r.Votes,
                    Party = r.Party,
                    Platform = r.Platform,
                    TotalVotes = ( (r.Votes / SumVotes) *100)
                }).Take(1);

            return View(model);
        }

        public ActionResult ResultsSecondPlace([Bind(Prefix = "id")] int ElectionId, Candidate candidate)
        {
            // we will assume that no two candidates can have equal votes. If that occurs, I'll just write something up to flip a digital coin like they do in real elections.

            var SumVotes = db.Candidates
                .Where(r => r.ElectionId == ElectionId)
                .Sum(r => r.Votes);

            var model = db.Candidates
                .Where(r => r.ElectionId == ElectionId)
                .OrderByDescending(r => r.Votes)
                .Select(r => new CandidateViewModel
                {
                    CandidateName = r.CandidateName,
                    ElectionName = r.ElectionName,
                    Votes = r.Votes,
                    Party = r.Party,
                    Platform = r.Platform,
                    TotalVotes = ((r.Votes / SumVotes) * 100)
                }).Skip(1).Take(1);

            return View(model);
        }

        public ActionResult ResultsThirdPlace([Bind(Prefix = "id")] int ElectionId, Candidate candidate)
        {
            // we will assume that no two candidates can have equal votes. If that occurs, I'll just write something up to flip a digital coin like they do in real elections.

            var SumVotes = db.Candidates
                .Where(r => r.ElectionId == ElectionId)
                .Sum(r => r.Votes);

            var model = db.Candidates
                .Where(r => r.ElectionId == ElectionId)
                .OrderByDescending(r => r.Votes)
                .Select(r => new CandidateViewModel
                {
                    CandidateName = r.CandidateName,
                    ElectionName = r.ElectionName,
                    Votes = r.Votes,
                    Party = r.Party,
                    Platform = r.Platform,
                    TotalVotes = ((r.Votes / SumVotes) * 100)
                }).Skip(2).Take(1);

            return View(model);
        }


        // Be aware these all might be shit, and could possibly not work if caching is implemented!(?)

        public ActionResult CastVote([Bind(Prefix = "id")] int candidateId, int electionId, VoteTracker votetracker, Candidate candidate)
        {
            //get current user Id for VoteTracker entity
            var voterId = WebSecurity.GetUserId(User.Identity.Name);

            //cross reference identity on VT entity, only last lambda truly matters
            var HasVoted = db.Votes.All(x => x.ElectionId == electionId && x.VoterId == voterId && x.HasVoted == false);

            //If HasVoted = false (AKA all queries on previous method return true), then this user hasnt voted for this particular election
            if (HasVoted.Equals(false))
            {
                //get candidate entity
                var model =
                    (from v in db.Candidates
                     where v.CandidateId == candidateId
                     select v);
                foreach (Candidate cand in model)
                {
                    //add 1 to votes on button press
                    cand.Votes = cand.Votes + 1;
                }
                db.SaveChanges();
                //get VT entity
                var HasVote = (from v in db.Votes
                               where v.VoterId == voterId
                               select v);
                foreach (VoteTracker vote in HasVote)
                {
                    //set flag to prevent future votes for that particular election
                    vote.HasVoted = true;
                }
                db.SaveChanges();

                return RedirectToAction("Index", "Election");

            }
            // otherwise frigg off
            else
            {

                return RedirectToAction("");
            }
        }

        public ActionResult CastLean([Bind(Prefix = "id")] int candidateId, int electionId, VoteTracker votetracker, Candidate candidate)
        {
            var voterId = WebSecurity.GetUserId(User.Identity.Name);


            var HasLeaned = db.Votes.All(x => x.ElectionId == electionId && x.VoterId == voterId && x.HasLeaned == false);
            //This implementation is duplicated but different, as the HasLeaned query is a switching flag
            if (HasLeaned.Equals(true))
            {

                var model =
                    (from v in db.Candidates
                     where v.CandidateId == candidateId
                     select v);
                foreach (Candidate cand in model)
                {
                    cand.LeanAmt = cand.LeanAmt + 1;
                }
                db.SaveChanges();

                var HasVote = (from v in db.Votes
                               where v.VoterId == voterId
                               select v);
                foreach (VoteTracker vote in HasVote)
                {
                    vote.HasLeaned = true;
                }
                db.SaveChanges();

                return RedirectToAction("Index", "Election");

            }
            else
            {

                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult CastInverseLean([Bind(Prefix = "id")] int candidateId, int electionId, VoteTracker votetracker, Candidate candidate)
        {
            var voterId = WebSecurity.GetUserId(User.Identity.Name);


            var HasLeaned = db.Votes.All(x => x.ElectionId == electionId && x.VoterId == voterId && x.HasLeaned == true);

            if (HasLeaned.Equals(true))
            {

                var model =
                    (from v in db.Candidates
                     where v.CandidateId == candidateId
                     select v);
                foreach (Candidate cand in model)
                {
                    cand.LeanAmt = cand.LeanAmt - 1;
                }
                db.SaveChanges();

                var HasVote = (from v in db.Votes
                               where v.VoterId == voterId
                               select v);
                foreach (VoteTracker vote in HasVote)
                {
                    vote.HasLeaned = false;
                }
                db.SaveChanges();

                return RedirectToAction("Index", "Election");

            }
            else
            {

                return RedirectToAction("Index", "Home");
            }
        }

        //POST: /_VoteTrackerPartial/

        public ActionResult CountVote([Bind(Prefix = "id")] int CandidateId)
        {
            //Uneeded. Remove?
            var voterId = WebSecurity.GetUserId(User.Identity.Name);

            //Tally all votes / leans for particular candidate. Used to display vote count in election screen.

            var countVote =
                   (from v in db.Candidates
                    where
                    v.CandidateId == CandidateId
                    select new VoteTrackerViewModel
                    {
                        Votes = v.Votes,
                        Leans = v.LeanAmt

                    });



            return View(countVote);
        }


        //
        // GET: /Candidate/Details/5

        public ActionResult Details(int id = 0)
        {
            Candidate candidate = db.Candidates.Find(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            return View(candidate);
        }

        //
        // GET: /Candidate/Create

        public ActionResult Create()
        {

            ElectionViewModel election = new ElectionViewModel();


            // GET used to generate election selectlist. Pain in the ass!
            election.ElectionList = new SelectList(db.Elections.ToList(), "ElectionId", "ElectionName");


            return View(election);
        }


        //
        // POST: /Candidate/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Candidate candidate)
        {
            if (ModelState.IsValid)
            {
                db.Candidates.Add(candidate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(candidate);
        }

        //
        // GET: /Candidate/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Candidate candidate = db.Candidates.Find(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            return View(candidate);
        }

        //
        // POST: /Candidate/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Candidate candidate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(candidate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(candidate);
        }

        //
        // GET: /Candidate/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Candidate candidate = db.Candidates.Find(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            return View(candidate);
        }

        //
        // POST: /Candidate/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Candidate candidate = db.Candidates.Find(id);
            db.Candidates.Remove(candidate);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }

}