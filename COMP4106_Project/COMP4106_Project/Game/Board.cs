using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace COMP4106_Project.Game
{
    public class Board
    {
        public static Random rndGen = new Random();


        private BoardLocation[,] pieces;//[x,y]

        private const int BOARD_SIZE = 30; // static board size and pawn count for simplicity
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

        private Piece getPieceWithId(int pieceID)
        {
            foreach (BoardLocation bl in pieces)
            {
                if (bl is Piece)
                {
                    if (((Piece)bl).id == pieceID)
                        return (Piece)bl;
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// using a O(n^2) search for comparing valid moves. Wont make significant impact because of the low piece count dealt with.
        /// 
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="moves"></param>
        public void MakeMove(int playerId, Move[] moves)
        {
            //attack/defend phase first
            for (int i = 0; i < moves.Length; i++)
            {
                if (moves[i].type == MoveType.Attack)
                {

                }
            }

            //move phase second
            for (int i = 0; i < moves.Length; i++)
            {
                if (moves[i].type == MoveType.Move)
                {
                    Piece p = getPieceWithId(moves[i].pieceId);
                    Point gotoL = getDisplacedLoc(p.x, p.y, moves[i].direction);

                    if (pieces[gotoL.X, gotoL.Y].type.Equals("none"))
                    {
                        bool anyOtherPieces = false;
                        //valid move, check if other pieces are trying to move into here
                        for (int j = 0; j < moves.Length; j++)
                        {
                            Piece p2 = getPieceWithId(moves[i].pieceId);
                            Point gotoL2 = getDisplacedLoc(p.x, p.y, moves[i].direction);
                            if (gotoL.Equals(gotoL2))
                            {
                                anyOtherPieces = true;
                                j = moves.Length;
                            }
                        }

                        if (!anyOtherPieces)
                        {
                            //the move can be made


                        }
                        else { } //invalid move
                    }

                }
            }
        }

        private Point getDisplacedLoc(int x, int y, Direction d)
        {
            if (d == Direction.Up)
                return new Point(x, y - 1);
            else if (d == Direction.Down)
                return new Point(x, y + 1);
            else if (d == Direction.Left)
                return new Point(x - 1, y);
            else if (d == Direction.Right)
                return new Point(x + 1, y);

            return new Point(x, y);
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
