using System;
using System.Collections.Generic;

namespace Bowling
{
    public class Game
    {

        private bool alreadyBuildingAFrame;

        private int previousRoll;

        private Frame firstFrame = null;
        private Frame lastFrame = null;
      
        public Game()
        {
        }

        public void Roll(int pins)
        {

           if (alreadyBuildingAFrame)
            {
                Frame f = new Frame(previousRoll, pins, lastFrame);
                lastFrame = f;
                if (firstFrame == null)
                {
                    firstFrame = f;
                }
                alreadyBuildingAFrame = false;
            }

           else
            {

                if (lastFrame != null && lastFrame.Id == 10)
                {
                    lastFrame.setExtraRoll(pins);
                }

                else if (pins == 10)
                {
                    Frame f = new Frame(10, 0, lastFrame);
                    lastFrame = f;
                    if (firstFrame == null)
                    {
                        firstFrame = f;
                    }
                }

                else
                {
                    previousRoll = pins;
                    alreadyBuildingAFrame = true;
                }
            }

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
            Strike
        }

        public int Id;

        private Frame previous = null;
        private Frame next = null;

        public bool HasNext()
        {
            return next != null;
        }

        public Frame GetNext()
        {
            return next;
        }

        private int roll1 = 0, roll2 = 0, roll3 = 0;

        

        public FrameType Type;


        private Frame(int roll1, int roll2)
        {
            if (roll1 + roll2 > 10)
            {
                throw new ImpossibleFrameException("" + (roll1 + roll2));
            }

            else if (roll1 == 10)
            {
                this.Type = FrameType.Strike;
            }

            else if (roll1 + roll2 == 10)
            {
                this.Type = FrameType.Spare;
            }


            this.roll1 = roll1;
            this.roll2 = roll2;
        }

        public Frame (int roll1, int roll2, Frame previous) 
            : this(roll1, roll2)
        {
            if (previous == null)
            {
                this.Id = 1;
                return;
            }
            this.previous = previous;
            previous.next = this;

            this.Id = previous.Id + 1;


        }

        public void setExtraRoll (int extraRoll)
        {
            if (this.roll3 != 0)
            {
                this.roll2 = this.roll3;
            }
            this.roll3 = extraRoll;
        }



        public int Score()
        {
            int ret = roll1 + roll2 + roll3;

            if (next == null)
            {
                return ret;
            }

            if (Type == FrameType.Spare)
            {
                ret += next.roll1;
            }

            else if (Type == FrameType.Strike)
            {
                if (next.Type == FrameType.Strike)
                {
                    if (next.HasNext())
                    {
                        ret += 10 + next.next.roll1;
                    }
                    else
                    {
                        ret += 10 + next.roll2;
                    }
                }
                else
                {
                    ret += next.roll1 + next.roll2;
                }
            }

            return ret;
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
