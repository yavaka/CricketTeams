namespace CricketTeams.Domain.Models.Teams
{
    using CricketTeams.Domain.Common;
    using System.Collections.Generic;

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

        public Players AddCaptain(Player captain)
        {
            this.Captain = captain;

            return this;
        }

        public Players AddWicketKeeper(Player wicketKeeper)
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
            this.Batsmen.Add(batsman);

            return this;
        }

        public Players AddBowler(Player bowler)
        {
            this.Bowlers.Add(bowler);

            return this;
        }

        public Players AddAllRounder(Player allRounder)
        {
            this.AllRounders.Add(allRounder);

            return this;
        }
    }
}
