using System;
using System.Collections.Generic;
using System.Text;

namespace Bowling
{
    public class Frame
    {
        public enum FrameType { Common, Spare, Strike, NotYetDefined }

        public FrameType Type { get; private set; } = FrameType.NotYetDefined;

        public List<int> Rolls { get; private set; } = new List<int>();

        // true if this frame is the last of the game.
        private bool _isLast;

        public Frame(bool isLast = false)
        {
            this._isLast = isLast;
        }

        // return true if the frame is complete after the roll
        public bool Roll(int pins)
        {
            this.Rolls.Add(pins);
            int nbOfRolls = this.Rolls.Count;

            switch (nbOfRolls)
            {
                case 1:
                    if (Rolls[0] == 10)
                    {
                        this.Type = FrameType.Strike;
                        return !_isLast; // a strike ends a frame except if it is the last frame
                    }
                    return false;

                case 2:
                    int sum = Rolls[0] + Rolls[1];
                    if (sum < 10)
                    {
                        this.Type = FrameType.Common;
                        return true;
                    }
                    else if (sum == 10)
                    {
                        if (Type == FrameType.NotYetDefined)
                            this.Type = FrameType.Spare;

                        return !_isLast; // a spare ends a frame except if it is the last frame
                    }
                    else
                    {
                        if (_isLast && Type == FrameType.Strike)
                            return false;

                        throw new ImpossibleFrameException("" + sum);
                    }


                default:
                    return true;
            }

        }

    }


    [Serializable]
    public class ImpossibleFrameException : Exception
    {
        public ImpossibleFrameException()
        {

        }

        public ImpossibleFrameException(string name)
            : base(String.Format("This frame cannot reach {0} since it is neither a spare or strike", name))
        {

        }

    }
}
