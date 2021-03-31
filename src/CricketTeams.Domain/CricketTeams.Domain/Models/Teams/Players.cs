namespace CricketTeams.Domain.Models.Teams
{
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Exceptions;
    using CricketTeams.Domain.Models.Players;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Players : ValueObject
    {
        internal Players(Player captain, Player wicketKeeper, Player twelfth)
        {
            this.Captain = captain;
            this.WicketKeeper = wicketKeeper;
            this.Twelfth = twelfth;

            this.Batsmen = new List<Player>();
            this.Bowlers = new List<Player>();
            this.AllRounders = new List<Player>();
        }

        private Players(Player captain, Player wicketKeeper)
        {
            this.Captain = captain;
            this.WicketKeeper = wicketKeeper;

            this.Twelfth = default!;
            this.Batsmen = default!;
            this.Bowlers = default!;
            this.AllRounders = default!;
        }

        public Player Captain { get; private set; }
        public Player WicketKeeper { get; private set; }
        public Player? Twelfth { get; private set; }
        public ICollection<Player> Batsmen { get; private set; }
        public ICollection<Player> Bowlers { get; private set; }
        public ICollection<Player> AllRounders { get; private set; }

        public Players UpdateCaptain(Player captain)
        {
            this.Captain = captain;

            return this;
        }

        public Players UpdateWicketKeeper(Player wicketKeeper)
        {
            this.WicketKeeper = wicketKeeper;

            return this;
        }

        public Players AddTwelfth(Player twelfth)
        {
            this.Twelfth = twelfth;

            return this;
        }

        public Players AddBatsman(Player batsman)
        {
            ValidateIsBatsmanExist(batsman);
            
            this.Batsmen.Add(batsman);

            return this;
        }
       
        public Players AddBowler(Player bowler)
        {
            ValidateIsBowlerExist(bowler);

            this.Bowlers.Add(bowler);

            return this;
        }

        public Players AddAllRounder(Player allRounder)
        {
            ValidateIsAllRounderExist(allRounder);

            this.AllRounders.Add(allRounder);

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
            if (this.Bowlers.Any(b =>b == bowler && b.FullName == bowler.FullName))
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
