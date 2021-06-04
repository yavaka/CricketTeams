namespace CricketTeams.Infrastructure.Persistence.Configurations
{
    using CricketTeams.Domain.Models.Players;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using static Domain.Models.ModelConstants.Common;
    using static Domain.Models.ModelConstants.BowlingStyle;
    using static Domain.Models.ModelConstants.FieldingPosition;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using CricketTeams.Domain.Models.Matches;

    internal class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder
                .HasKey(p => p.Id);

            builder
                .Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(MaxNameLength);

            builder
                .Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(MaxNameLength);

            builder
                .Property(p => p.Age)
                .HasMaxLength(MaxAge);

            builder
                .Property(p => p.PhotoUrl)
                .HasMaxLength(MaxUrlLength);

            //Batting style owner
            builder
                .OwnsOne(p => p.BattingStyle, p =>
                {
                    p.WithOwner();

                    p.Property(bs => bs!.Value);
                });

            //Bowling style owner
            builder
               .OwnsOne(p => p.BowlingStyle, p =>
               {
                   p.WithOwner();

                   p.Property(bowling => bowling!.StyleName)
                    .IsRequired()
                    .HasMaxLength(MaxStyleNameLength);

                   p.Property(bowling => bowling!.Description)
                    .IsRequired()
                    .HasMaxLength(MaxDescriptionLength);

                   // Bowling type owner
                   p.OwnsOne(bowling => bowling!.BowlingType, bt =>
                   {
                       bt.WithOwner();

                       bt.Property(bt => bt.Value)
                         .IsRequired();
                   });
               });

            //Fielding position owner
            builder
                .OwnsOne(p => p.FieldingPosition, p =>
                {
                    p.WithOwner();

                    p.Property(fp => fp!.PositionName)
                     .IsRequired()
                     .HasMaxLength(MaxPositionName);

                    p.Property(fp => fp!.Description)
                     .IsRequired()
                     .HasMaxLength(MaxDescriptionLength);
                });

            //History owner
            builder
                .OwnsOne(p => p.History, p =>
                 {
                     p.WithOwner();

                     //Match stat owner
                     p.OwnsMany(m => m.Matches, m =>
                     {
                         m.WithOwner().HasForeignKey("OwnerId");
                         
                         m.HasKey("Id");

                         m.Property(mId => mId.MatchId)
                          .IsRequired();

                         //Match batsman owner
                         m.OwnsOne(mS => mS.Batsman, mb =>
                         {
                             mb.WithOwner();

                             mb.Property(f => f!.NumberOfFour)
                             .IsRequired();

                             mb.Property(s => s!.NumberOfSix)
                             .IsRequired();
                         });

                         //Match  owner
                         m.OwnsOne(mS => mS.Bowler, mbowler =>
                         {
                             mbowler.WithOwner();
                         });
                         
                         //FieldingPosition owner
                         m.OwnsOne(mS => mS.FieldingPosition, fp =>
                         {
                             fp.WithOwner();

                             fp.OwnsOne(pos => pos!.FieldingPosition, fieldPos =>
                             {
                                 fieldPos.WithOwner();

                                 fieldPos.Property(fieldingPos => fieldingPos!.PositionName)
                                    .IsRequired()
                                    .HasMaxLength(MaxPositionName);

                                 fieldPos.Property(fieldingPos => fieldingPos!.Description)
                                    .IsRequired()
                                    .HasMaxLength(MaxDescriptionLength);
                             });

                             // check that --------------------------------------------------------------------------------------------
                             fp.Property(pOut => pOut!.PlayersOut)
                                .HasConversion(
                                    c => JsonConvert.SerializeObject(c),
                                    c => JsonConvert.DeserializeObject<Dictionary<Player, PlayerOutTypes>>(c));

                         });
                     });
                 });

            //Achievements owner
            builder
                .OwnsMany(a => a.Achievements, a =>
                {
                    a.WithOwner().HasForeignKey("OwnerId");
                    
                    a.Property(n => n.Name)
                     .IsRequired()
                     .HasMaxLength(MaxNameLength);

                    a.Property(n => n.Description)
                     .IsRequired()
                     .HasMaxLength(MaxDescriptionLength);

                    a.Property(n => n.ImageUrl)
                     .HasMaxLength(MaxUrlLength);

                    //Achievement type owner
                    a.OwnsOne(at => at.AchievementType, at =>
                    {
                        at.WithOwner();

                        at.Property(v => v.Value);
                    });
                });
        }
    }
}
