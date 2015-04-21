using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP4106_Project.AI_BoardGame.Pieces
{
    public class Empty_Piece : Piece
    {
        public Empty_Piece(int x, int y)
            : base(Players.World, "Empty", ' ', x, y)
        {

        }
    }
}
