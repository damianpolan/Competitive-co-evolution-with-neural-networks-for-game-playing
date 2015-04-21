using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP4106_Project.AI_BoardGame.Pieces
{
    public class Wall_Piece : Piece
    {
        public Wall_Piece(int x, int y)
            : base(Players.World, "Wall", 'X', x, y)
        {

        }
    }
}
