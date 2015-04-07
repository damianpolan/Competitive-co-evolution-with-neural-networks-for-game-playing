using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COMP4106_Project.Game
{
    public class BoardLocation
    {
        public string type;
        public int x, y;

        public BoardLocation(int xPos, int yPos)
        {
            this.x = xPos;
            this.y = yPos;
            this.type = "none";
        }

    }
}
