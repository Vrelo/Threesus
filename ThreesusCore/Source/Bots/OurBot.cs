using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Threesus.CoreGame;

namespace Threesus.Bots
{
    public class OurBot : IBot
    {
        /// <summary>
        /// Returns the next move to make based on the state of the specified game, or null to make no move.
        /// </summary>
        public ShiftDirection? GetNextMove(FastBoard board, FastDeck deck, NextCardHint nextCardHint, ref long movesEvaluated) {
            return null;
        }

        /// <summary>
        /// Returns the next move to make based on the state of the specified game, or null to make no move.
        /// </summary>
        public ShiftDirection? GetNextMove(FastBoard board, FastDeck deck, NextCardHint nextCardHint) {
            long movesEvaluated = 0;
            return GetNextMove(board, deck, nextCardHint, ref movesEvaluated);
        }
    }
}
