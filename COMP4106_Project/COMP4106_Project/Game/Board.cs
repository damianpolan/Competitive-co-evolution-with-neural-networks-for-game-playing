using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COMP4106_Project.Game
{
    public class Board
    {
        private Piece[][] pieces;

        public Board(int size, int pawnCount)
        {
            throw new NotImplementedException();
        }

        public void MakeMove(int playerId, MoveId[] moves)
        {
            throw new NotImplementedException();
        }

        public VisibleState GetVisibleStateForPlayer(int playerId)
        {
            throw new NotImplementedException();
        }

        public bool IsGameOver()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {

        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        //public Piece[] GetPieces()
        //{
        //}


        protected VisibleState generateLocalState(int playerId)
        {
            throw new NotImplementedException();
        }

        protected Piece getPiece(int id)
        {
            throw new NotImplementedException();
        }
    }
}
