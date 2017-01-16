using System.Web.Http;
using System.Web.Http.Description;
using SwissTournament.API.Infrastructure;
using SwissTournament.API.Requests;
using SwissTournament.API.Service;
using SwissTournament.API.DTO;

namespace SwissTournament.API.Controllers
{
    public class TournamentsController : ApiController
    {
        private TournamentService _tournamentService;

        public TournamentsController(TournamentService tournamentService)
        { 
            this._tournamentService = tournamentService;
        }

        // POST: api/Tournaments
        [ResponseType(typeof(TournamentDto))]
        public IHttpActionResult PostTournament(TournamentOptions options)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tournament = _tournamentService.CreateTournament(options.PlayerNames);

            return CreatedAtRoute("DefaultApi", new { id = tournament.TournamentId }, tournament);
        }


        //// GET: api/Tournaments
        //public IQueryable<Tournament> GetTournaments()
        //{
        //    return db.Tournaments;
        //}

        //// GET: api/Tournaments/5
        //[ResponseType(typeof(Tournament))]
        //public IHttpActionResult GetTournament(int id)
        //{
        //    Tournament tournament = db.Tournaments.Find(id);
        //    if (tournament == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(tournament);
        //}

        //// PUT: api/Tournaments/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutTournament(int id, Tournament tournament)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != tournament.TournamentId)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(tournament).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TournamentExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// DELETE: api/Tournaments/5
        //[ResponseType(typeof(Tournament))]
        //public IHttpActionResult DeleteTournament(int id)
        //{
        //    Tournament tournament = db.Tournaments.Find(id);
        //    if (tournament == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Tournaments.Remove(tournament);
        //    db.SaveChanges();

        //    return Ok(tournament);
        //}
    }
}