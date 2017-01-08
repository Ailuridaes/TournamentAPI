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
using SwissTournament.API.Domain;
using SwissTournament.API.Requests;
using SwissTournament.API.Service;

namespace SwissTournament.API.Controllers
{
    public class TournamentsController : ApiController
    {
        private TournamentService _tournamentService = new TournamentService();
        // TODO: REMOVE once all dependencies are replaced
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

            var tournament = _tournamentService.createTournament(options.PlayerNames);

            // TODO: Switch to DTO
            // Create anonymous object
            /* 
             * Could take this approach instead of DTO classes
             * So can perform specific queries
             * Rather than rely on client to sort relevant info
             * Ex. Return only matchups for the current round
             * But might be able to perform query first,
             * then automap children to DTOs and pass DTOs to parent mapper
             * Could make request to Match controller and perform query + mapping there
             */
            var tournamentDto = new
            {
                TournamentId = tournament.TournamentId,
                Round = tournament.Round,
                TotalRounds = tournament.TotalRounds,
                StartTime = tournament.StartTime,
                
                Players = tournament.Players.Select(p => new
                {
                    PlayerId = p.PlayerId,
                    TournamentId = p.TournamentId,
                    Name = p.Name,
                    Standing = p.Standing
                })
            };

            return CreatedAtRoute("DefaultApi", new { id = tournament.TournamentId }, tournamentDto);
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