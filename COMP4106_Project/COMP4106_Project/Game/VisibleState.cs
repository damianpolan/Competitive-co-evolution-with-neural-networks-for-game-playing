using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COMP4106_Project.Game
{
    public class VisibleState
    {
        public Piece[][] board;
        public Piece[] player;
        public Piece[] enemy;

        public VisibleState(Piece[][] boardPieces, Piece[] playerPieces, Piece[] enemyPieces)
        {
            this.board = boardPieces;
            this.player = playerPieces;
            this.enemy = enemyPieces;
        }
    }
}
