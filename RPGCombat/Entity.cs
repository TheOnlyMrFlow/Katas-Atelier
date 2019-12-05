using System;
using System.Collections.Generic;
using System.Text;

namespace RPGCombat
{
    public abstract class Entity
    {
        public Point2D Position { get; set;  }

        public Entity()
        {
            Position = new Point2D(0, 0);
        }

    }
}
