using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COMP4106_Project.Game
{
    public struct MoveId
    {
        public Move move;
        public int id;
        public MoveId(int pieceId, Move pieceMove)
        {
            this.id = pieceId;
            this.move = pieceMove;
        }
    }
}
