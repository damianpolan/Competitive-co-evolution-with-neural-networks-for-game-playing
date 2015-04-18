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


        public BoardLocation[,] pieces;//[x,y]


        private List<Piece> player_0_pieces, player_1_pieces;

        private const int BOARD_SIZE = 30; // static board size and pawn count for simplicity
        //pawn count = 5
        // + 1 king

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

            //PLAYER 0
            player_0_pieces = new List<Piece>();
            
            player_0_pieces.Add(new KingPiece(1, kingY, 0));
            pieces[1, kingY] = player_0_pieces[player_0_pieces.Count - 1];//king in middle left for player one

            player_0_pieces.Add(new Piece(2, kingY, 0));
            pieces[2, kingY] = player_0_pieces[player_0_pieces.Count - 1];

            player_0_pieces.Add(new Piece(2, kingY - 1, 0));
            pieces[2, kingY - 1] = player_0_pieces[player_0_pieces.Count - 1];

            player_0_pieces.Add(new Piece(2, kingY + 1, 0));
            pieces[2, kingY + 1] = player_0_pieces[player_0_pieces.Count - 1];

            player_0_pieces.Add(new Piece(2, kingY - 2, 0));
            pieces[2, kingY - 2] = player_0_pieces[player_0_pieces.Count - 1];

            player_0_pieces.Add(new Piece(2, kingY + 2, 0));
            pieces[2, kingY + 2] = player_0_pieces[player_0_pieces.Count - 1];


            //PLAYER 1

            player_1_pieces = new List<Piece>();

            player_1_pieces.Add(new KingPiece(BOARD_SIZE - 2, kingY, 1));
            pieces[BOARD_SIZE - 2, kingY] = player_0_pieces[player_1_pieces.Count - 1];//king in middle left for player one

            player_1_pieces.Add(new Piece(BOARD_SIZE - 3, kingY, 1));
            pieces[BOARD_SIZE - 3, kingY] = player_0_pieces[player_1_pieces.Count - 1];

            player_1_pieces.Add(new Piece(BOARD_SIZE - 3, kingY - 1, 1));
            pieces[BOARD_SIZE - 3, kingY - 1] = player_0_pieces[player_1_pieces.Count - 1];

            player_1_pieces.Add(new Piece(BOARD_SIZE - 3, kingY + 1, 1));
            pieces[BOARD_SIZE - 3, kingY + 1] = player_0_pieces[player_1_pieces.Count - 1];

            player_1_pieces.Add(new Piece(BOARD_SIZE - 3, kingY - 2, 1));
            pieces[BOARD_SIZE - 3, kingY - 2] = player_0_pieces[player_1_pieces.Count - 1];

            player_1_pieces.Add(new Piece(BOARD_SIZE - 3, kingY + 2, 1));
            pieces[BOARD_SIZE - 3, kingY + 2] = player_0_pieces[player_1_pieces.Count - 1];



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
            foreach (BoardLocation bl in player_0_pieces)
            {
                if (bl is Piece)
                {
                    if (((Piece)bl).id == pieceID)
                        return (Piece)bl;
                }
            }

            foreach (BoardLocation bl in player_1_pieces)
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
        public void MakeMove(Move[] moves)
        {
            //attack/defend phase first
            for (int i = 0; i < moves.Length; i++)
            {
                if (moves[i].type == MoveType.Attack)
                {
                    Piece p = getPieceWithId(moves[i].pieceId);
                    Point attLoc = getDisplacedLoc(p.x, p.y, moves[i].direction);
                    BoardLocation targetP = pieces[attLoc.X, attLoc.Y];
                    if (targetP.type.Equals("pawn") || targetP.type.Equals("king")) // must attack valid pieces
                    {
                        Piece targetPiece = (Piece)targetP;
                        if (targetPiece.player != p.player) //no friendly fire
                        {
                            targetPiece.damage(); //do the damage
                        }
                    }
                }
            }
            //clean up dead phase ( should be done after attack phase )
            for (int y = 0; y < pieces.GetLength(1); y++)
            {
                for (int x = 0; x < pieces.GetLength(0); x++)
                {
                    if (pieces[x, y] is Piece)
                    {
                        Piece piece = (Piece)pieces[x, y];
                        if (piece.isDead())
                        {
                            pieces[x, y] = new BoardLocation(x, y);
                        }
                    }
                }
            }

            //move phase second
            for (int i = 0; i < moves.Length; i++)
            {
                if (moves[i].type == MoveType.Move)
                {
                    Piece p = getPieceWithId(moves[i].pieceId);
                    if (p == null) continue;
                    Point gotoL = getDisplacedLoc(p.x, p.y, moves[i].direction);

                    if (pieces[gotoL.X, gotoL.Y].type.Equals("none"))
                    {
                        bool anyOtherPieces = false;
                        //valid move, check if other pieces are trying to move into here
                        for (int j = 0; j < moves.Length; j++)
                        {
                            if (j != i)
                            {
                                Piece p2 = getPieceWithId(moves[j].pieceId);
                                Point gotoL2 = getDisplacedLoc(p2.x, p2.y, moves[j].direction);
                                if (gotoL.Equals(gotoL2))
                                {
                                    anyOtherPieces = true;
                                    j = moves.Length;
                                }
                            }
                        }

                        if (!anyOtherPieces)//the move can be made
                        {
                            pieces[p.x, p.y] = pieces[gotoL.X, gotoL.Y];
                            pieces[p.x, p.y].x = p.x;
                            pieces[p.x, p.y].y = p.y;

                            pieces[gotoL.X, gotoL.Y] = p;
                            p.x = gotoL.X;
                            p.y = gotoL.Y;
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
            Point king_index;

            return null;
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
            String s = "";

            for (int y = 0; y < pieces.GetLength(1); y++)
            {
                for (int x = 0; x < pieces.GetLength(0); x++)
                {
                    s += stringOf(pieces[x, y]);
                    s += "  ";
                }
                s += "\n";
            }

            return s;
        }

        public string stringOf(BoardLocation p)
        {
            if (p.type.Equals("block"))
                return "#";
            else if (p.type.Equals("none"))
                return "-";
            else if (p.type.Equals("pawn") && ((Piece)p).player == 0)
                return "x";
            else if (p.type.Equals("pawn") && ((Piece)p).player == 1)
                return "o";
            else if (p.type.Equals("king") && ((Piece)p).player == 0)
                return "X";
            else if (p.type.Equals("king") && ((Piece)p).player == 1)
                return "O";
            else
                return "E"; // E for invalid
        }

        protected VisibleState generateLocalState(int playerId)
        {
            bool[,] vis_map = getVisionMap(playerId);

            BoardLocation[,] bl = new BoardLocation[BOARD_SIZE, BOARD_SIZE];

            for(int x = 0; x < BOARD_SIZE; x++)
            {
                for(int y = 0; y < BOARD_SIZE; y++)
                {
                    if(vis_map[x,y])
                    {
                        bl[x, y] = pieces[x, y];
                    }
                }
            }

            List<Piece> player_pieces, opponent_pieces;

            if(playerId == 0)
            {
                player_pieces = player_0_pieces;
                opponent_pieces = player_1_pieces;
            }
            else
            {
                player_pieces = player_1_pieces;
                opponent_pieces = player_0_pieces;
            }


            //remove opponent pieces not visible
            for(int i = 0; i < opponent_pieces.Count; i++)
            {
                Piece p = opponent_pieces[i];

                if (!vis_map[p.x,p.y])
                    opponent_pieces.Remove(p);
            }

            //return new VisibleState(bl, player_pieces, opponent_pieces);
            return null;
        }

        protected bool[,] getVisionMap(int playerId)
        {
            bool[,] vis_map = new bool[BOARD_SIZE, BOARD_SIZE];

            List<Piece> player_pieces = playerId == 0 ? player_0_pieces : player_1_pieces;

            foreach (Piece piece in player_pieces)
            {
                for (int x = -piece.vision; x <= piece.vision; x++)
                {
                    for (int y = -piece.vision; y <= piece.vision; y++)
                    {
                        int relX = piece.x - x;
                        int relY = piece.y - y;

                        //within grid and vision radius
                        if (relX >= 0 && relX < BOARD_SIZE && relY >= 0 && relY < BOARD_SIZE
                            && Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2)) <= piece.vision)
                        {
                            // not already visible
                            if (!vis_map[relX, relY])
                            {
                                //much simpler to just use radius, no need to raycast
                                vis_map[relX, relY] = true;
                            }
                        }
                    }
                }
            }

            return vis_map;
        }

    }
}
