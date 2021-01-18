using System;
using System.Collections.Generic;
using System.Linq;
using SabberStoneCore.Enums;
using SabberStoneCore.Model;
using SabberStoneCore.Model.Helpers;
using Thesis.Evaluation;

namespace Thesis.Evolution
{
    public class EvolutionBase
    {
        public List<Player> Players { get; private set; }
        public List<AttributesChange> Changes { get; private set; }

        public EvolutionBase(List<Player> players)
        {
            Players = players;

            InitCards();
        }

        private void InitCards()
        {
            
        }
    }
}
