using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COMP4106_Project.Game
{
    public class Board
    {
        public static Random rndGen = new Random();


        private BoardLocation[,] pieces;//[x,y]

        private static const int BOARD_SIZE = 30; // static board size and pawn count for simplicity
        //pawn count = 5

        public Board()
        {
            pieces = new BoardLocation[BOARD_SIZE, BOARD_SIZE]; //x,y

            //assign blank values
            for (int x = 0; x < pieces.GetLength(0); x++)
                for (int y = 0; y < pieces.GetLength(1); y++)
                {
                    if (x == 0 || y == 0 || x == pieces.GetLength(0) - 1 || y == pieces.GetLength(1) - 1)
                    {
                        pieces[x, y] = new Block(x, y);
                    }
                    else
                        pieces[x, y] = new BoardLocation(x, y);
                }

            //generate starting positions
            /*
             * BOARD ARANGMENT:
             *   P          P
             *   P          P
             * K P   ...    P K
             *   P          P
             *   P          P
             * 
             */
            int kingY = BOARD_SIZE / 2;
            pieces[0, kingY] = new KingPiece(0, kingY, 0);//king in middle left for player one
            pieces[1, kingY] = new Piece(1, kingY, 0);
            pieces[1, kingY - 1] = new Piece(1, kingY - 1, 0);
            pieces[1, kingY + 1] = new Piece(1, kingY + 1, 0);
            pieces[1, kingY - 2] = new Piece(1, kingY - 2, 0);
            pieces[1, kingY + 2] = new Piece(1, kingY + 2, 0);

            pieces[BOARD_SIZE - 1, kingY] = new KingPiece(BOARD_SIZE - 1, kingY, 0);//king in middle left for player one
            pieces[BOARD_SIZE - 2, kingY] = new Piece(BOARD_SIZE - 2, kingY, 0);
            pieces[BOARD_SIZE - 2, kingY - 1] = new Piece(BOARD_SIZE - 2, kingY - 1, 0);
            pieces[BOARD_SIZE - 2, kingY + 1] = new Piece(BOARD_SIZE - 2, kingY + 1, 0);
            pieces[BOARD_SIZE - 2, kingY - 2] = new Piece(BOARD_SIZE - 2, kingY - 2, 0);
            pieces[BOARD_SIZE - 2, kingY + 2] = new Piece(BOARD_SIZE - 2, kingY + 2, 0);


            //generate random block obstacles and border blocks
            int numberOfBlocks = 15;
            while (numberOfBlocks > 0)
            {
                int rX = rndGen.Next(0, BOARD_SIZE);
                int rY = rndGen.Next(0, BOARD_SIZE);
                if (pieces[rX, rY].type.Equals("none"))
                {
                    pieces[rX, rY] = new Block(rX, rY);
                    numberOfBlocks--;
                }
            }


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
