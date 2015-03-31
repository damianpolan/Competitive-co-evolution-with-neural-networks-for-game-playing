using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COMP4106_Project.Game
{
    public class Piece : BoardLocation
    {
        public int id, player, health, vision, defence;

        public void Attack(Direction direction)
        {
            throw new NotImplementedException();
        }
    }
}
