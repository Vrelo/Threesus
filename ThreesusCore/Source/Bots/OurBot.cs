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
        private readonly BoardQualityEvaluator _evaluator;

        public OurBot(BoardQualityEvaluator evaluator) {
            if (evaluator == null)
                throw new ArgumentNullException("evaluator");
            _evaluator = evaluator;
        }
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
            float quality;
            return GetBestMoveForBoard(board, deck, knownNextCardIndex, out quality, ref movesEvaluated);
        }

        /// <summary>
        /// Returns the next move to make based on the state of the specified game, or null to make no move.
        /// </summary>
        public ShiftDirection? GetNextMove(FastBoard board, FastDeck deck, NextCardHint nextCardHint) {
            long movesEvaluated = 0;
            return GetNextMove(board, deck, nextCardHint, ref movesEvaluated);
        }

        private ShiftDirection? GetBestMoveForBoard(FastBoard board, FastDeck deck, ulong knownNextCardIndex, out float moveQuality, ref long movesEvaluated)
        {
            float? leftQuality = null;
            float? rightQuality = null;
            float? upQuality = null;
            float? downQuality = null;
            Parallel.Invoke(
                () =>
                {
                    leftQuality = EvaluateMoveForBoard(board, deck, knownNextCardIndex, ShiftDirection.Left);
                    rightQuality = EvaluateMoveForBoard(board, deck, knownNextCardIndex, ShiftDirection.Right);
                },
                () =>
                {
                    upQuality = EvaluateMoveForBoard(board, deck, knownNextCardIndex, ShiftDirection.Up);
                    downQuality = EvaluateMoveForBoard(board, deck, knownNextCardIndex, ShiftDirection.Down);
                });
            //movesEvaluated += moves1 + moves2;

            float? bestQuality = leftQuality;
            ShiftDirection? bestDir = leftQuality != null ? ShiftDirection.Left : (ShiftDirection?)null;
            if (rightQuality != null && (bestQuality == null || rightQuality.Value > bestQuality.Value))
            {
                bestQuality = rightQuality;
                bestDir = ShiftDirection.Right;
            }
            if (upQuality != null && (bestQuality == null || upQuality.Value > bestQuality.Value))
            {
                bestQuality = upQuality;
                bestDir = ShiftDirection.Up;
            }
            if (downQuality != null && (bestQuality == null || downQuality.Value > bestQuality.Value))
            {
                bestQuality = downQuality;
                bestDir = ShiftDirection.Down;
            }
            moveQuality = bestQuality ?? float.MinValue;
            return bestDir;
        }

        private unsafe float? EvaluateMoveForBoard(FastBoard board, FastDeck deck, ulong knownNextCardIndex, ShiftDirection dir)
        { 
            FastBoard shiftedBoard = board;
			IntVector2D* newCardCells = stackalloc IntVector2D[4];
            if (shiftedBoard.ShiftInPlace(dir, newCardCells))
            {
                if (knownNextCardIndex == ulong.MaxValue) // Special value for bonus card.
                {
                    return null;
                }
                else if (knownNextCardIndex > 0)
                { }
                else { }
            }
            else {
                return null;
            }
        }
    }
}
