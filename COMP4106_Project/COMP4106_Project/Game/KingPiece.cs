using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COMP4106_Project.Game
{
    class KingPiece : Piece
    {
        static int KING_VISION = 6;

        public KingPiece(int xPos, int yPos, int player)
            : base(xPos, yPos, player)
        {
            this.vision = KING_VISION;
            this.type = "king";
        }
    }
}
