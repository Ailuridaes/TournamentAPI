using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SwissTournament.API.Infrastructure;
using SwissTournament.API.Models;

namespace SwissTournament.API.Controllers
{
    public class TournamentsController : ApiController
    {
        private TournamentDataContext db = new TournamentDataContext();

        //// POST: api/Tournaments
        //[ResponseType(typeof(Tournament))]
        //public IHttpActionResult PostTournament(Tournament tournament)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Tournaments.Add(tournament);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = tournament.TournamentId }, tournament);
        //}

        // POST: api/Tournaments
        [ResponseType(typeof(Tournament))]
        public IHttpActionResult PostTournament(TournamentOptions options)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tournament = new Tournament();
            db.Tournaments.Add(tournament);

            // TODO: Check # of players?
            // Call method in player controller? Repository?
            foreach (string name in options.PlayerNames)
            {
                var player = new Player(name, tournament.TournamentId);
                db.Players.Add(player);
            }

            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tournament.TournamentId }, tournament);
        }


    // GET: api/Tournaments
    public IQueryable<Tournament> GetTournaments()
        {
            return db.Tournaments;
        }

        // GET: api/Tournaments/5
        [ResponseType(typeof(Tournament))]
        public IHttpActionResult GetTournament(int id)
        {
            Tournament tournament = db.Tournaments.Find(id);
            if (tournament == null)
            {
                return NotFound();
            }

            return Ok(tournament);
        }

        // PUT: api/Tournaments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTournament(int id, Tournament tournament)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tournament.TournamentId)
            {
                return BadRequest();
            }

            db.Entry(tournament).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TournamentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/Tournaments/5
        [ResponseType(typeof(Tournament))]
        public IHttpActionResult DeleteTournament(int id)
        {
            Tournament tournament = db.Tournaments.Find(id);
            if (tournament == null)
            {
                return NotFound();
            }

            db.Tournaments.Remove(tournament);
            db.SaveChanges();

            return Ok(tournament);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TournamentExists(int id)
        {
            return db.Tournaments.Count(e => e.TournamentId == id) > 0;
        }
    }
}