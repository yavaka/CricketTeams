namespace CricketTeams.Domain.Models.Scores
{
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Exceptions;
    using CricketTeams.Domain.Models.Matches;
    using CricketTeams.Domain.Models.Players;
    using CricketTeams.Domain.Models.Teams;
    using System;
    using System.Collections.Generic;

    public class Score : Entity<int>
    {
        public Score(
            int tossWinnerTeamId,
            TossDecisions tossDecision,
            Team teamA,
            Team teamB,
            int oversPerInning,
            int numberOfInnings)
        {
            ValidateTeams(teamA, teamB);
            this.TossWinnerTeamId = tossWinnerTeamId;
            this.TossDecision = tossDecision;
            this.TeamA = teamA;
            this.TeamB = teamB;
            this.OversPerInning = oversPerInning;
            this.NumberOfInnings = numberOfInnings;
            this.Innings = new List<Inning>();
        }

        public int TossWinnerTeamId { get; private set; }
        public TossDecisions TossDecision { get; private set; }
        public Team TeamA { get; private set; }
        public Team TeamB { get; private set; }
        public int OversPerInning { get; private set; }
        public int NumberOfInnings { get; set; }
        public Inning? CurrentInning { get; private set; }
        public ICollection<Inning> Innings { get; private set; }
        public int TotalScoreTeamA { get; private set; }
        public int TotalScoreTeamB { get; private set; }
        public bool IsMatchEnd { get; private set; }

        #region Over methods

        public Score UpdateCurrentBallWithRuns(int runs)
        {
            ValidateIsCurrentInningSet();
            ValidateIsMatchEnd();

            this.CurrentInning!.UpdateCurrentBallWithRuns(runs);

            EndInning();

            return this;
        }

        public Score UpdateCurrentBallWithSix()
        {
            ValidateIsCurrentInningSet();
            ValidateIsMatchEnd();

            this.CurrentInning!.UpdateCurrentBallWithSix();

            EndInning();

            return this;
        }

        public Score UpdateCurrentBallWithFour()
        {
            ValidateIsCurrentInningSet();
            ValidateIsMatchEnd();

            this.CurrentInning!.UpdateCurrentBallWithFour();

            EndInning();

            return this;
        }

        public Score UpdateCurrentBallWithNoBall(int runs)
        {
            ValidateIsCurrentInningSet();
            ValidateIsMatchEnd();

            this.CurrentInning!.UpdateCurrentBallWithNoBall(runs);

            EndInning();

            return this;
        }

        public Score UpdateCurrentBallWithWideBall(int runs)
        {
            ValidateIsCurrentInningSet();
            ValidateIsMatchEnd();

            this.CurrentInning!.UpdateCurrentBallWithWideBall(runs);

            EndInning();

            return this;
        }

        public Score UpdateCurrentBallWithDismissedBatsman(
            bool isStrikerOut,
            Player newBatsman,
            Player bowlingTeamPlayer,
            Player dismissedBatsman,
            PlayerOutTypes batsmanOutType)
        {
            ValidateIsCurrentInningSet();
            ValidateIsMatchEnd();

            this.CurrentInning!.UpdateCurrentBallWithDismissedBatsman(
                isStrikerOut,
                newBatsman,
                bowlingTeamPlayer,
                dismissedBatsman,
                batsmanOutType);

            EndInning();

            return this;
        }

        #endregion

        #region Inning methods

        public Score UpdateCurrentOver(Player bowler, Player striker, Player nonStriker)
        {
            ValidateIsCurrentInningSet();
            ValidateIsMatchEnd();

            this.CurrentInning!.UpdateCurrentOver(bowler, striker, nonStriker);

            return this;
        }

        public Score UpdateCurrentOverWithBatsman(Player bowler, Player newBatsman)
        {
            ValidateIsCurrentInningSet();
            ValidateIsMatchEnd();

            this.CurrentInning!.UpdateCurrentOverWithBatsman(bowler, newBatsman);

            return this;
        }

        #endregion

        public Score UpdateCurrentInning()
        {
            ValidateIsLastInningEnd();

            if (this.CurrentInning is not null)
            {
                ValidateIsMatchEnd();

                if (this.CurrentInning.BattingTeam == TeamA)
                {
                    this.CurrentInning = new Inning(
                        battingTeam: TeamB,
                        bowlingTeam: TeamA,
                        this.OversPerInning);
                }
                else
                {
                    this.CurrentInning = new Inning(
                        battingTeam: TeamA,
                        bowlingTeam: TeamB,
                        this.OversPerInning);
                }
            }
            else
            {
                CreateInning();
            }
            AddLastInning();

            return this;
        }

        private void CreateInning()
        {
            if (this.TossDecision == TossDecisions.Batting)
            {
                //teamA - batting
                if (this.TeamA.Id == this.TossWinnerTeamId)
                {
                    this.CurrentInning = new Inning(this.TeamA, this.TeamB, this.OversPerInning);
                }
                else //teamB - batting
                {
                    this.CurrentInning = new Inning(this.TeamB, this.TeamA, this.OversPerInning);
                }
            }
            else
            {
                if (this.TeamA.Id == this.TossWinnerTeamId)
                {
                    this.CurrentInning = new Inning(this.TeamB, this.TeamA, this.OversPerInning);
                }
                else
                {
                    this.CurrentInning = new Inning(this.TeamA, this.TeamB, this.OversPerInning);
                }
            }
        }

        private void UpdateScoreStat()
        {
            if (this.CurrentInning!.BattingTeam == this.TeamA)
            {
                this.TotalScoreTeamA = this.CurrentInning!.TotalRuns;
            }
            else if (this.CurrentInning!.BattingTeam == this.TeamB)
            {
                this.TotalScoreTeamB = this.CurrentInning!.TotalRuns;
            }

            ValidateIsMatchEnd();
        }

        private void EndMatch()
        {
            if (this.Innings.Count == this.NumberOfInnings &&
                IsLastInningEnd())
            {
                this.IsMatchEnd = true;
            }
        }

        private bool IsLastInningEnd()
        {
            if (/* Check overs */this.CurrentInning!.Overs.Count == this.OversPerInning &&
                /* Check balls */this.CurrentInning!.CurrentOver!.Balls.Count == 6 + this.CurrentInning!.CurrentOver!.ExtraBalls)
            {
                return true;
            }
            return false;
        }

        private void AddLastInning()
        {
            if (this.CurrentInning is not null)
            {
                this.Innings.Add(this.CurrentInning);
            }
        }

        private void EndInning()
        {
            if (IsLastInningEnd())
            {
                UpdateScoreStat();
            }
        }

        #region Validations

        private void ValidateIsMatchEnd()
        {
            if (this.IsMatchEnd)
            {
                throw new InvalidOverException("Match ended.");
            }
            else
            {
                EndMatch();
            }
        }

        private void ValidateTeams(Team teamA, Team teamB)
        {
            if (teamA == teamB)
            {
                throw new InvalidScoreException($"Two same teams cannot compete against each other.");
            }
        }

        private void ValidateIsCurrentInningSet()
        {
            if (this.CurrentInning is null)
            {
                throw new InvalidScoreException($"{nameof(this.CurrentInning)} is not initialised. Use {nameof(UpdateCurrentInning)} method.");
            }
        }

        private void ValidateIsLastInningEnd()
        {
            if (this.CurrentInning is not null && !IsLastInningEnd())
            {
                throw new InvalidScoreException("Inning still in progress.");
            }
        }

        #endregion
    }
}