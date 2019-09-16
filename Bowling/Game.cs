using System;
using System.Linq;
using System.Collections.Generic;

namespace Bowling
{
    public class Game
    {

        private bool alreadyBuildingAFrame;

        private int previousRoll;

        private Frame firstFrame;
        private Frame currentFrame;
      
        public Game()
        {
            firstFrame = new Frame(1);
            currentFrame = firstFrame;
        }

        public void Roll(int pins)
        {

            currentFrame = currentFrame.Roll(pins);

        }

        public int Score ()
        {
            int ret = 0;
            Frame f = firstFrame;

            ret += f.Score();
            while(f.HasNext())
            {
                f = f.GetNext();
                ret += f.Score();
            }

            return ret;
        }

        
    }

 

    class Frame
    {

        public enum FrameType
        {
            Common,
            Spare,
            Strike,
            NotYetDefined
        }

        public int Id;

        
        private Frame next = null;

        private List<int> rolls;

        public FrameType Type = FrameType.NotYetDefined;

        private Frame()
        {
            this.rolls = new List<int>();
        }

        public Frame(int id)
            : this()
        {
            this.Id = id;
        }

        public Frame (Frame previous) 
            : this()
        {
            if (previous == null)
            {
                this.Id = 1;
                return;
            }
            previous.next = this;

            this.Id = previous.Id + 1;


        }


        public bool HasNext()
        {
            return next != null;
        }

        public Frame GetNext()
        {
            return next;
        }

        // return the frame which the next roll will belong to
        public Frame Roll(int pins)
        {
            this.rolls.Add(pins);
            int count = this.rolls.Count;

            // particular case of the last frame
            if (Id == 10)
            {

                switch(count)
                {
                    case 1:
                        if (rolls[0] == 10)
                        {
                            this.Type = FrameType.Strike;
                        }
                        return this;
                    case 2:
                        int sum = rolls[0] + rolls[1];
                        if (Type == FrameType.NotYetDefined)
                        {
                            if (sum == 10)
                            {
                                this.Type = FrameType.Spare;
                            }
                            else if (sum < 10)
                            {
                                this.Type = FrameType.Common;
                            }
                            else
                            {
                                throw new ImpossibleFrameException("" + sum);
                            }

                        }
                        return this;
                    default:
                        return null;
                }
 
            }
            //common cases (i.e the 9 first frames)
            else
            {
                switch(count)
                {
                    case 1: 
                        if(pins == 10)
                        {
                            Type = FrameType.Strike;
                            return new Frame(this);
                        }
                        return this;

                    case 2:
                        int sum = rolls[0] + rolls[1];
                        if (sum == 10)
                        {
                            Type = FrameType.Spare;
                        }
                        else if (sum > 10)
                        {
                            throw new ImpossibleFrameException("" + sum);
                        }
                        return new Frame(this);
                    default:
                        return null;

                }
            }

        }

        private int ComputeBonus()
        {
            if (Id == 10)
            {
                return 0;
            }

            switch(Type)
            {
                case FrameType.Common:
                    return 0;

                case FrameType.Spare:
                    return next.rolls[0];

                case FrameType.Strike:
                    if (next.Type == FrameType.Strike && next.Id != 10)
                    {
                        return next.rolls[0] + next.next.rolls[0];
                    }
                    return next.rolls[0] + next.rolls[1];
                default:
                    return 0;
            }


        }

        public int Score()
        {
            return rolls.Sum() + ComputeBonus();
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
