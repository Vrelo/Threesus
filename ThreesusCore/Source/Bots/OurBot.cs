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
            ulong knownNextCardIndex;
            switch (nextCardHint)
            {
                case NextCardHint.One:
                    knownNextCardIndex = 1;
                    break;
                case NextCardHint.Two:
                    knownNextCardIndex = 2;
                    break;
                case NextCardHint.Three:
                    knownNextCardIndex = 3;
                    break;
                case NextCardHint.Bonus:
                    knownNextCardIndex = ulong.MaxValue; // MaxValue means "bonus" and is calculated specially.
                    break;
                default:
                    throw new NotSupportedException("Unknown NextCardHint '" + nextCardHint + "'.");
            }
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
