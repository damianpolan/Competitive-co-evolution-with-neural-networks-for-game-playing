using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COMP4106_Project.Game
{
    public class VisibleState
    {
        public BoardLocation[,] board;
        public Piece[] player;
        public Piece[] enemy;
        public BoardLocation[] blocks;

        public VisibleState(BoardLocation[,] boardPieces, Piece[] playerPieces, Piece[] enemyPieces, BoardLocation[] blocks)
        {
            this.board = boardPieces;
            this.player = playerPieces;
            this.enemy = enemyPieces;
            this.blocks = blocks;
        }
    }
}
