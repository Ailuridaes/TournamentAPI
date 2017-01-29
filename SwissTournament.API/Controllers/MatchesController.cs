using SwissTournament.Core.DTO;
using SwissTournament.Core.Exceptions;
using SwissTournament.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace SwissTournament.API.Controllers
{
    public class MatchesController : ApiController
    {
        private IMatchService _matchService;

        public MatchesController(IMatchService matchService)
        {
            this._matchService = matchService;
        }

        // GET: api/Matches/{tournamentId}
        [HttpGet]
        [Route("api/Matches/{tournamentId}")]
        public IHttpActionResult GetMatches(int tournamentId)
        {
            try
            {
                return Ok(_matchService.GetMatches(tournamentId));
            }
            catch (ReadEntityException)
            {
                return NotFound();
            }
        }

        // GET: api/Matches/current/{tournamentId}
        [HttpGet]
        [Route("api/Matches/current/{tournamentId}")]
        public IHttpActionResult GetCurrentMatches(int tournamentId)
        {
            try
            {
                return Ok(_matchService.GetCurrentMatches(tournamentId));
            }
            catch (ReadEntityException)
            {
                return NotFound();
            };
        }

        // PUT: api/Matches/{matchId}
        [HttpPut]
        [Route("api/Matches/{matchId}")]
        public IHttpActionResult PutMatch(int matchId, MatchDto match)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (matchId != match.Id)
            {
                return BadRequest();
            }

            try
            {
                _matchService.Update(match);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
