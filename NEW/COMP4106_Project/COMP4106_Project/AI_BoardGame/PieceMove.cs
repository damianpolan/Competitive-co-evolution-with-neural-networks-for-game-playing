using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using COMP4106_Project.AI_BoardGame.Pieces;

namespace COMP4106_Project.AI_BoardGame
{
    public struct PieceMove
    {
        public Pawn_Piece Piece;
        public Moves Move1;
        public Moves Move2;

        public PieceMove(Pawn_Piece p, Moves m1, Moves m2)
        {
            this.Piece = p;
            this.Move1 = m1;
            this.Move2 = m2;
        }
    }
}
