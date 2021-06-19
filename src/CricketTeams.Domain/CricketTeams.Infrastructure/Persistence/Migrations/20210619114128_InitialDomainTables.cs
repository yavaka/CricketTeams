using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CricketTeams.Infrastructure.Persistence.Migrations
{
    public partial class InitialDomainTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coaches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Nickname = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Age = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    BattingStyle_Value = table.Column<int>(type: "int", nullable: true),
                    BowlingStyle_StyleName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    BowlingStyle_BowlingType_Value = table.Column<int>(type: "int", nullable: true),
                    BowlingStyle_Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coaches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stadiums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<int>(type: "int", maxLength: 200000, nullable: false),
                    WebsiteUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stadiums", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Umpires",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Age = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    MatchesAsMainReferee = table.Column<int>(type: "int", nullable: false),
                    MatchesAsSecondReferee = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Umpires", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    PlayersId = table.Column<int>(type: "int", nullable: true),
                    CoachId = table.Column<int>(type: "int", nullable: true),
                    StadiumId = table.Column<int>(type: "int", nullable: true),
                    History_TotalWins = table.Column<int>(type: "int", nullable: true),
                    History_TotalLoses = table.Column<int>(type: "int", nullable: true),
                    History_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Coaches_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Coaches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teams_Stadiums_StadiumId",
                        column: x => x.StadiumId,
                        principalTable: "Stadiums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScoresFromMatches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TossWinnerTeamId = table.Column<int>(type: "int", nullable: false),
                    TossDecision_Value = table.Column<int>(type: "int", nullable: false),
                    TeamAId = table.Column<int>(type: "int", nullable: false),
                    TeamBId = table.Column<int>(type: "int", nullable: false),
                    OversPerInning = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    NumberOfInnings = table.Column<int>(type: "int", maxLength: 4, nullable: false),
                    TotalScoreTeamA = table.Column<int>(type: "int", nullable: false),
                    TotalScoreTeamB = table.Column<int>(type: "int", nullable: false),
                    IsMatchEnd = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoresFromMatches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScoresFromMatches_Teams_TeamAId",
                        column: x => x.TeamAId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScoresFromMatches_Teams_TeamBId",
                        column: x => x.TeamBId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sponsor",
                columns: table => new
                {
                    FK_Sponsor_Owned = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    WebsiteUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    SponsorType_Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sponsor", x => new { x.FK_Sponsor_Owned, x.Id });
                    table.ForeignKey(
                        name: "FK_Sponsor_Teams_FK_Sponsor_Owned",
                        column: x => x.FK_Sponsor_Owned,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inning",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BattingTeamId = table.Column<int>(type: "int", nullable: false),
                    BowlingTeamId = table.Column<int>(type: "int", nullable: false),
                    OversPerInning = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    TotalRuns = table.Column<int>(type: "int", nullable: false),
                    IsInningEnd = table.Column<bool>(type: "bit", nullable: false),
                    ScoreId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inning", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inning_ScoresFromMatches_ScoreId",
                        column: x => x.ScoreId,
                        principalTable: "ScoresFromMatches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inning_Teams_BattingTeamId",
                        column: x => x.BattingTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inning_Teams_BowlingTeamId",
                        column: x => x.BowlingTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamAId = table.Column<int>(type: "int", nullable: false),
                    TeamBId = table.Column<int>(type: "int", nullable: false),
                    NumberOfInnings = table.Column<int>(type: "int", maxLength: 4, nullable: false),
                    OversPerInning = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    InProgress = table.Column<bool>(type: "bit", nullable: false),
                    IsMatchEnded = table.Column<bool>(type: "bit", nullable: false),
                    FirstUmpireId = table.Column<int>(type: "int", nullable: true),
                    SecondUmpireId = table.Column<int>(type: "int", nullable: true),
                    ScoreId = table.Column<int>(type: "int", nullable: true),
                    Statistic_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Statistic_ManOfTheMatchId = table.Column<int>(type: "int", nullable: true),
                    Statistic_WinningTeamId = table.Column<int>(type: "int", nullable: true),
                    Statistic_WinningTeamTotalRuns = table.Column<int>(type: "int", nullable: true),
                    Statistic_LosingTeamId = table.Column<int>(type: "int", nullable: true),
                    Statistic_LosingTeamTotalRuns = table.Column<int>(type: "int", nullable: true),
                    Statistic_TossWinnerTeamId = table.Column<int>(type: "int", nullable: true),
                    Statistic_TossDecision_Value = table.Column<int>(type: "int", nullable: true),
                    StadiumId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_ScoresFromMatches_ScoreId",
                        column: x => x.ScoreId,
                        principalTable: "ScoresFromMatches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Stadiums_StadiumId",
                        column: x => x.StadiumId,
                        principalTable: "Stadiums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_TeamAId",
                        column: x => x.TeamAId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_TeamBId",
                        column: x => x.TeamBId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Umpires_FirstUmpireId",
                        column: x => x.FirstUmpireId,
                        principalTable: "Umpires",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Umpires_SecondUmpireId",
                        column: x => x.SecondUmpireId,
                        principalTable: "Umpires",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Over",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BowlerId = table.Column<int>(type: "int", nullable: false),
                    StrikerId = table.Column<int>(type: "int", nullable: false),
                    NonStrikerId = table.Column<int>(type: "int", nullable: false),
                    TotalRuns = table.Column<int>(type: "int", nullable: false),
                    ExtraBalls = table.Column<int>(type: "int", nullable: false),
                    IsOverEnd = table.Column<bool>(type: "bit", nullable: false),
                    InningId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Over", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Over_Inning_InningId",
                        column: x => x.InningId,
                        principalTable: "Inning",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Age = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    BattingStyle_Value = table.Column<int>(type: "int", nullable: true),
                    BowlingStyle_StyleName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    BowlingStyle_BowlingType_Value = table.Column<int>(type: "int", nullable: true),
                    BowlingStyle_Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FieldingPosition_PositionName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    FieldingPosition_Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CaptainId = table.Column<int>(type: "int", nullable: true),
                    InningId = table.Column<int>(type: "int", nullable: true),
                    OverId = table.Column<int>(type: "int", nullable: true),
                    TeamPlayersId1 = table.Column<int>(type: "int", nullable: true),
                    TeamPlayersId2 = table.Column<int>(type: "int", nullable: true),
                    TeamPlayersId3 = table.Column<int>(type: "int", nullable: true),
                    WicketKeeperId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Inning_InningId",
                        column: x => x.InningId,
                        principalTable: "Inning",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Players_Over_OverId",
                        column: x => x.OverId,
                        principalTable: "Over",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ball",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BowlerId = table.Column<int>(type: "int", nullable: false),
                    StrikerId = table.Column<int>(type: "int", nullable: false),
                    NonStrikerId = table.Column<int>(type: "int", nullable: false),
                    Runs = table.Column<int>(type: "int", nullable: false),
                    WideBall = table.Column<bool>(type: "bit", nullable: false),
                    NoBall = table.Column<bool>(type: "bit", nullable: false),
                    Six = table.Column<bool>(type: "bit", nullable: false),
                    Four = table.Column<bool>(type: "bit", nullable: false),
                    IsBatsmanOut = table.Column<bool>(type: "bit", nullable: false),
                    BowlingTeamPlayerId = table.Column<int>(type: "int", nullable: true),
                    DismissedBatsmanId = table.Column<int>(type: "int", nullable: true),
                    OutType_Value = table.Column<int>(type: "int", nullable: true),
                    OverId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ball", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ball_Over_OverId",
                        column: x => x.OverId,
                        principalTable: "Over",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ball_Players_BowlerId",
                        column: x => x.BowlerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ball_Players_BowlingTeamPlayerId",
                        column: x => x.BowlingTeamPlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ball_Players_DismissedBatsmanId",
                        column: x => x.DismissedBatsmanId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ball_Players_NonStrikerId",
                        column: x => x.NonStrikerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ball_Players_StrikerId",
                        column: x => x.StrikerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MatchStat",
                columns: table => new
                {
                    HistoryPlayerId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchId = table.Column<int>(type: "int", nullable: false),
                    Batsman_TotalRuns = table.Column<int>(type: "int", nullable: true),
                    Batsman_NumberOfSix = table.Column<int>(type: "int", nullable: true),
                    Batsman_NumberOfFour = table.Column<int>(type: "int", nullable: true),
                    Batsman_IsPlayerOut = table.Column<bool>(type: "bit", nullable: true),
                    Batsman_FielderId = table.Column<int>(type: "int", nullable: true),
                    Batsman_PlayerOutType_Value = table.Column<int>(type: "int", nullable: true),
                    Bowler_WideBalls = table.Column<int>(type: "int", nullable: true),
                    Bowler_Wickets = table.Column<int>(type: "int", nullable: true),
                    FieldingPosition_FieldingPosition_PositionName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    FieldingPosition_FieldingPosition_Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchStat", x => new { x.HistoryPlayerId, x.Id });
                    table.ForeignKey(
                        name: "FK_MatchStat_Players_Batsman_FielderId",
                        column: x => x.Batsman_FielderId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatchStat_Players_HistoryPlayerId",
                        column: x => x.HistoryPlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamPlayers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TwelfthId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamPlayers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamPlayers_Players_TwelfthId",
                        column: x => x.TwelfthId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlayerOut",
                columns: table => new
                {
                    MatchFieldingPositionMatchStatHistoryPlayerId = table.Column<int>(type: "int", nullable: false),
                    MatchFieldingPositionMatchStatId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DismissedPlayerId = table.Column<int>(type: "int", nullable: false),
                    PlayerOutType_Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerOut", x => new { x.MatchFieldingPositionMatchStatHistoryPlayerId, x.MatchFieldingPositionMatchStatId, x.Id });
                    table.ForeignKey(
                        name: "FK_PlayerOut_MatchStat_MatchFieldingPositionMatchStatHistoryPlayerId_MatchFieldingPositionMatchStatId",
                        columns: x => new { x.MatchFieldingPositionMatchStatHistoryPlayerId, x.MatchFieldingPositionMatchStatId },
                        principalTable: "MatchStat",
                        principalColumns: new[] { "HistoryPlayerId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerOut_Players_DismissedPlayerId",
                        column: x => x.DismissedPlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ball_BowlerId",
                table: "Ball",
                column: "BowlerId");

            migrationBuilder.CreateIndex(
                name: "IX_Ball_BowlingTeamPlayerId",
                table: "Ball",
                column: "BowlingTeamPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Ball_DismissedBatsmanId",
                table: "Ball",
                column: "DismissedBatsmanId");

            migrationBuilder.CreateIndex(
                name: "IX_Ball_NonStrikerId",
                table: "Ball",
                column: "NonStrikerId");

            migrationBuilder.CreateIndex(
                name: "IX_Ball_OverId",
                table: "Ball",
                column: "OverId");

            migrationBuilder.CreateIndex(
                name: "IX_Ball_StrikerId",
                table: "Ball",
                column: "StrikerId");

            migrationBuilder.CreateIndex(
                name: "IX_Inning_BattingTeamId",
                table: "Inning",
                column: "BattingTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Inning_BowlingTeamId",
                table: "Inning",
                column: "BowlingTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Inning_ScoreId",
                table: "Inning",
                column: "ScoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_FirstUmpireId",
                table: "Matches",
                column: "FirstUmpireId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_ScoreId",
                table: "Matches",
                column: "ScoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_SecondUmpireId",
                table: "Matches",
                column: "SecondUmpireId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_StadiumId",
                table: "Matches",
                column: "StadiumId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamAId",
                table: "Matches",
                column: "TeamAId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamBId",
                table: "Matches",
                column: "TeamBId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchStat_Batsman_FielderId",
                table: "MatchStat",
                column: "Batsman_FielderId");

            migrationBuilder.CreateIndex(
                name: "IX_Over_BowlerId",
                table: "Over",
                column: "BowlerId");

            migrationBuilder.CreateIndex(
                name: "IX_Over_InningId",
                table: "Over",
                column: "InningId");

            migrationBuilder.CreateIndex(
                name: "IX_Over_NonStrikerId",
                table: "Over",
                column: "NonStrikerId");

            migrationBuilder.CreateIndex(
                name: "IX_Over_StrikerId",
                table: "Over",
                column: "StrikerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerOut_DismissedPlayerId",
                table: "PlayerOut",
                column: "DismissedPlayerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_CaptainId",
                table: "Players",
                column: "CaptainId",
                unique: true,
                filter: "[CaptainId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Players_InningId",
                table: "Players",
                column: "InningId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_OverId",
                table: "Players",
                column: "OverId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamPlayersId1",
                table: "Players",
                column: "TeamPlayersId1");

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamPlayersId2",
                table: "Players",
                column: "TeamPlayersId2");

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamPlayersId3",
                table: "Players",
                column: "TeamPlayersId3");

            migrationBuilder.CreateIndex(
                name: "IX_Players_WicketKeeperId",
                table: "Players",
                column: "WicketKeeperId",
                unique: true,
                filter: "[WicketKeeperId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ScoresFromMatches_TeamAId",
                table: "ScoresFromMatches",
                column: "TeamAId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoresFromMatches_TeamBId",
                table: "ScoresFromMatches",
                column: "TeamBId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPlayers_TwelfthId",
                table: "TeamPlayers",
                column: "TwelfthId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CoachId",
                table: "Teams",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_PlayersId",
                table: "Teams",
                column: "PlayersId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_StadiumId",
                table: "Teams",
                column: "StadiumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_TeamPlayers_PlayersId",
                table: "Teams",
                column: "PlayersId",
                principalTable: "TeamPlayers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Over_Players_BowlerId",
                table: "Over",
                column: "BowlerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Over_Players_NonStrikerId",
                table: "Over",
                column: "NonStrikerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Over_Players_StrikerId",
                table: "Over",
                column: "StrikerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CaptainId",
                table: "Players",
                column: "CaptainId",
                principalTable: "TeamPlayers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_TeamPlayers_TeamPlayersId1",
                table: "Players",
                column: "TeamPlayersId1",
                principalTable: "TeamPlayers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_TeamPlayers_TeamPlayersId2",
                table: "Players",
                column: "TeamPlayersId2",
                principalTable: "TeamPlayers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_TeamPlayers_TeamPlayersId3",
                table: "Players",
                column: "TeamPlayersId3",
                principalTable: "TeamPlayers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WicketKeeperId",
                table: "Players",
                column: "WicketKeeperId",
                principalTable: "TeamPlayers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Over_OverId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamPlayers_Players_TwelfthId",
                table: "TeamPlayers");

            migrationBuilder.DropTable(
                name: "Ball");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "PlayerOut");

            migrationBuilder.DropTable(
                name: "Sponsor");

            migrationBuilder.DropTable(
                name: "Umpires");

            migrationBuilder.DropTable(
                name: "MatchStat");

            migrationBuilder.DropTable(
                name: "Over");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Inning");

            migrationBuilder.DropTable(
                name: "ScoresFromMatches");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Coaches");

            migrationBuilder.DropTable(
                name: "Stadiums");

            migrationBuilder.DropTable(
                name: "TeamPlayers");
        }
    }
}
