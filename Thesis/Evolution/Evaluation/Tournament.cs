using System;
using System.Collections.Generic;

namespace Thesis.Evolution.Evaluation
{
    public class Tournament
    {
        public List<Player> Players { get; set; }
        public double Evaluate()
        {
            var count = Players.Count;

            double sum = 0;

            for (int i=0; i<count; i++)
            {
                for (int j=i+1; j<count; j++)
                {
                    var matchup = new Matchup(Players[i], Players[j]);
                    matchup.PlayMatchup();

                    var winRate = matchup.P1Wins/matchup.GamesPlayed;

                    sum += (winRate - 0.5) * (winRate - 0.5);
                }
            }

            var games = count * (count - 1)/2;

            return 2 * Math.Sqrt(sum / games);
        }
    }
}
