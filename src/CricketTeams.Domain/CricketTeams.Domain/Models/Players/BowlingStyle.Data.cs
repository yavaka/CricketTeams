namespace CricketTeams.Domain.Models.Players
{
    using System;
    using System.Collections.Generic;
    using CricketTeams.Domain.Common;

    internal class BowlingStyleData : IInitialData
    {
        public Type EntityType => typeof(BowlingStyle);

        public IEnumerable<object> GetData()
        => new List<BowlingStyle>
        {
            new BowlingStyle("In Swinger", BowlingType.FastBowling, "A cricket ball is known to swing when the ball moves in the air after it has been released from the hands of the bowler."),
            new BowlingStyle("Out Swinger", BowlingType.FastBowling, "Out swing deliveries are the ones that move away from the batsman."),
            new BowlingStyle("Reverse Swing", BowlingType.FastBowling, "A reverse swing occurs when the ball starts to move in the reverse direction of how it is typically expected to swing."),
            new BowlingStyle("Bouncer", BowlingType.FastBowling, "A delivery is known as a bouncer when a bowler intentionally makes the ball bounce nearly half way on the pitch and, as a result, the ball reaches the batsman at a shoulder or head level height."),
            new BowlingStyle("Slower Ball", BowlingType.FastBowling, "The objective of a slower ball is to deceive the batsman with sudden change in the speed of the delivery in the air rather than scaring the batsman with really fast speed."),
            new BowlingStyle("Yorker", BowlingType.FastBowling, "A yorker is basically a type of delivery when the ball is bowled right at the base of the stumps of the batsman. Often, the aim of the bowler with the yorker is to bowl right at the toes of the batsman."),
            new BowlingStyle("Off cutter", BowlingType.FastBowling, "The purpose of this delivery is not necessarily to beat the batsman with the change in speed, but with the change in the direction of the ball after it bounces on the pitch."),
            new BowlingStyle("Leg cutter", BowlingType.FastBowling, "The goal of using the leg cutter is not necessarily deceiving the batsman with the speed at which the ball is bowled, but also the change in direction after the ball bounces on the pitch."),
            new BowlingStyle("Knuckleball", BowlingType.FastBowling, "The objective of bowling the knuckleball is similar to that of a slower ball which is to defeat the batsman with the change in the speed of the ball."),
            new BowlingStyle("Beamer", BowlingType.FastBowling, "What the ball bowled by the bowler reaches the batsman directly at about chest or head high, it is considered a beamer."),
            new BowlingStyle("Off Break", BowlingType.SpinBowling, "The idea of off break or an off spin, is to make the ball spin towards the stumps (for a right hand batsman). An off break ball will be typically pitched outside the off stump and then made to spin back into the stumps of the batsman."),
            new BowlingStyle("Top Spin for an Off Spinner", BowlingType.SpinBowling, "The top spin often very deceptive as the batsman expects the ball to turn very rapidly towards him, however, it in fact increases the speed after bouncing thereby going past the batsman."),
            new BowlingStyle("Arm Ball", BowlingType.SpinBowling, "The Arm Ball, as the name suggests, uses some extra effort from the arm of the bowler. It is essentially a much quicker delivery than a spinner usually bowls."),
            new BowlingStyle("The \"Doosra\"", BowlingType.SpinBowling, "The doosra looked like a typical off spin bowl when it left the hands of the bowler, however, after it bounced on the pitch it actually went in the opposite direction than it was supposed to go. The ball landed on the pitch and spun from right to left (like a leg spin) instead of spinning from left to right (like an off spin)."),
            new BowlingStyle("Carrom Ball", BowlingType.SpinBowling, "While bowling the carrom ball, the bowler typically holds the ball between the thumb and the middle finger. The ball then has to essentially be squeezed out of the hand of the bowler much like flicking a carrom disc in the game of carrom board."),
            new BowlingStyle("The \"Teesra\"", BowlingType.SpinBowling, "To bowl the teesra, the ball is held in a normal grip by an off spinner. However, at the point of release of the ball, the bowler moves the wrist but does not roll the fingers."),
            new BowlingStyle("Leg Break", BowlingType.LegSpinBowling, "Just as the off break is the usual delivery of an off spin bowler, the leg break is also the go-to delivery for a leg spin bowler! The leg break is also known as leg spin."),
            new BowlingStyle("Top Spin for a Leg Spinner", BowlingType.LegSpinBowling, "The ball loops in the air, but drops much faster and shorter than expected and often bounces much more than anticipated by the batsman."),
            new BowlingStyle("Googly", BowlingType.LegSpinBowling, "The objective of a googly for a leg spinner is to deceive the batsman by spinning the ball in the other direction than what is expected."),
            new BowlingStyle("Flipper", BowlingType.LegSpinBowling, "The idea behind the flipper is similar to the top spin. However, it doesn’t bounce as much as a typical top spin ball would. When a flipper is bowled by a leg spinner, it seems to the batsman that it would be a short of length delivery, however, it just doesn’t bounce as much and skids through getting the batsman out LBW or Bowled."),
            new BowlingStyle("Slider", BowlingType.LegSpinBowling, "The ball, instead of spinning like a leg break, actually just holds the line and slides on to the batsman."),
        };
    }
}
