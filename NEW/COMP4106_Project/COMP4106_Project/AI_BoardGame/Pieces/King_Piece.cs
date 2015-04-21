using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP4106_Project.AI_BoardGame.Pieces
{
    public class King_Piece : Pawn_Piece
    {
        public King_Piece(Players player, int x, int y)
            : base(player, "King", 'K', x, y, 2, 4)
        {

        }

        public override object Clone()
        {
            return new King_Piece(this.Player, this.X, this.Y);
        }
    }
}
