using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COMP4106_Project.Game
{
    public abstract class AI
    {
        int player;

        public AI(int player)
        {
            this.player = player;
        }

        public abstract Move[] requestMove(VisibleState state);
    }
}
