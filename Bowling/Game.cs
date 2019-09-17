using System;
using System.Linq;
using System.Collections.Generic;

namespace Bowling
{
    public class Game
    {
        public Frame[] Frames { get; private set; } = new Frame[10];

        private int _currentFrameIndex = 0;

        public Game(){}

        public void Roll(int pins)
        {

            if (_currentFrameIndex > 9)
                throw new Exception("The game is ended already");

            if (Frames[_currentFrameIndex] == null)
                Frames[_currentFrameIndex] = new Frame(_currentFrameIndex == 9);

            if (Frames[_currentFrameIndex].Roll(pins))
                _currentFrameIndex++;

        }

        public int ComputeFrameBonus(int frameIndex)
        {
            if (frameIndex == 9)
                return 0;

            switch (Frames[frameIndex].Type)
            {
                case Frame.FrameType.Spare:
                    return Frames[frameIndex + 1].Rolls[0];

                case Frame.FrameType.Strike:
                    return
                        frameIndex != 8 && Frames[frameIndex + 1].Type == Frame.FrameType.Strike
                            ? Frames[frameIndex + 1].Rolls[0] + Frames[frameIndex + 2].Rolls[0] // if next frame is a both strike and not the last frame
                            : Frames[frameIndex + 1].Rolls[0] + Frames[frameIndex + 1].Rolls[1]; 

                default:
                    return 0;
            }
        }

        public int Score ()
        {
            int score = 0;
            for (int i = 0; i < 10; i++) {
                if (Frames[i] == null)
                    throw new Exception("Cannot compute score of an unfinished game");
                score += Frames[i].Rolls.Sum() + ComputeFrameBonus(i);
            }
            return score;
        }
    }
}
