using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COMP4106_Project.Game
{
    public class Block : BoardLocation
    {
        public Block(int xPos, int yPos)
            :base(xPos, yPos)
        {
            base.type = "block";
        }
    }
}
