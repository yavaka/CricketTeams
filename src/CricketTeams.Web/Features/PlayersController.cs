namespace CricketTeams.Web.Features
{
    using CricketTeams.Application.Contracts;
    using CricketTeams.Domain.Models.Players;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    [ApiController]
    [Route("[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly IRepository<Player> _players;

        public PlayersController(IRepository<Player> players)
            => this._players = players;

        [HttpGet]
        public IEnumerable<Player> Get()
            => this._players.All();
    }
}
