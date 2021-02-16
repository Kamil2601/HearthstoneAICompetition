using System;
using System.Collections.Generic;
using Thesis.Evolution.Models;

namespace Thesis.Evolution.Evaluation
{
    public class Tournament: IEvaluation
    {
        public int GamesForMatchup { get; set; } = 50;

        public double Evaluate(List<Player> players)
        {
            var count = players.Count;

            double sum = 0;

            for (int i=0; i<count; i++)
            {
                for (int j=i+1; j<count; j++)
                {
                    var matchup = new Matchup(players[i], players[j])
                    {
                        GamesForMatchup = this.GamesForMatchup
                    };
                    
                    matchup.PlayMatchup();

                    double winRate = (double)matchup.P1Wins/(double)matchup.GamesPlayed;

                    sum += (winRate - 0.5) * (winRate - 0.5);
                }
            }

            var games = count * (count - 1)/2;

            return 2 * Math.Sqrt(sum / games);
        }
    }
}
