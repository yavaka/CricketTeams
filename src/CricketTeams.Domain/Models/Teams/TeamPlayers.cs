namespace CricketTeams.Domain.Models.Teams
{
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Exceptions;
    using CricketTeams.Domain.Models.Players;
    using System.Collections.Generic;
    using System.Linq;

    public class TeamPlayers : Entity<int>
    {
        private List<Player> _batsmen;
        private List<Player> _bowlers;
        private List<Player> _allRounders;

        internal TeamPlayers(Player captain, Player wicketKeeper, Player twelfth)
        {
            this.Captain = captain;
            this.WicketKeeper = wicketKeeper;
            this.Twelfth = twelfth;

            this._batsmen = new List<Player>();
            this._bowlers = new List<Player>();
            this._allRounders = new List<Player>();
        }

        private TeamPlayers()
        {
            this.Captain = default!;
            this.WicketKeeper = default!;
            this.Twelfth = default!;
            this._batsmen = default!;
            this._bowlers = default!;
            this._allRounders = default!;
        }

        public Player Captain { get; private set; }
        public Player WicketKeeper { get; private set; }
        public Player? Twelfth { get; private set; }
        public IReadOnlyCollection<Player> Batsmen
            => this._batsmen.ToList().AsReadOnly();
        public IReadOnlyCollection<Player> Bowlers
            => this._bowlers.ToList().AsReadOnly();
        public IReadOnlyCollection<Player> AllRounders
            => this._allRounders.ToList().AsReadOnly();
        public IReadOnlyCollection<Player> AllPlayers => Batsmen;

        public TeamPlayers UpdateCaptain(Player captain)
        {
            this.Captain = captain;

            return this;
        }

        public TeamPlayers UpdateWicketKeeper(Player wicketKeeper)
        {
            this.WicketKeeper = wicketKeeper;

            return this;
        }

        public TeamPlayers UpdateTwelfth(Player twelfth)
        {
            this.Twelfth = twelfth;

            return this;
        }

        public TeamPlayers AddBatsman(Player batsman)
        {
            ValidateIsBatsmanExist(batsman);

            this._batsmen.Add(batsman);

            return this;
        }

        public TeamPlayers AddBowler(Player bowler)
        {
            ValidateIsBowlerExist(bowler);

            this._bowlers.Add(bowler);

            return this;
        }

        public TeamPlayers AddAllRounder(Player allRounder)
        {
            ValidateIsAllRounderExist(allRounder);

            this._allRounders.Add(allRounder);

            return this;
        }

        private void ValidateIsBatsmanExist(Player batsman)
        {
            if (this.Batsmen.Any(b => b == batsman && b.FullName == batsman.FullName))
            {
                throw new InvalidTeamPlayersException($"Batsman {batsman.FullName} already in this team.");
            }
        }

        private void ValidateIsBowlerExist(Player bowler)
        {
            if (this.Bowlers.Any(b => b == bowler && b.FullName == bowler.FullName))
            {
                throw new InvalidTeamPlayersException($"Bowler {bowler.FullName} already in this team.");
            }
        }

        private void ValidateIsAllRounderExist(Player allRounder)
        {
            if (this.AllRounders.Any(r => r == allRounder && r.FullName == allRounder.FullName))
            {
                throw new InvalidTeamPlayersException($"All rounder {allRounder.FullName} already in this team.");
            }
        }
    }
}
