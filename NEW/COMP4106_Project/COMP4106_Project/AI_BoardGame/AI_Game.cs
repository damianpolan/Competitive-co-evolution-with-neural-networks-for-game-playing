using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using COMP4106_Project.AI_BoardGame.Pieces;

namespace COMP4106_Project.AI_BoardGame
{
    public class AI_Game
    {
        protected Board board;
        protected List<Pawn_Piece> player_1_pieces, player_2_pieces;

        protected bool game_over;
        protected Players winner;

        //PARAMS
        //returns the pieces for a player
        public Pawn_Piece[] getPieces(Players player)
        {
            if (player == Players.One) return this.player_1_pieces.ToArray();
            else if (player == Players.Two) return this.player_2_pieces.ToArray();

            return null;
        }

        //true if game over
        public bool isGameOver()
        {
            return this.game_over;
        }

        //the winning player, world = tie
        public Players getWinner()
        {
            return this.winner;
        }

        //returns a board with vision applied
        public Board getRelativeState(Players player)
        {
            return this.generateRelativeBoard(player);
        }

        //CONSTRUCTOR
        public AI_Game()
        {
            this.game_over = false;
            this.winner = Players.World;
            this.setupBoard();
        }

        //draws the board state to the console
        public virtual void Draw()
        {
            this.board.Draw();
        }

        //plays out a turn
        public virtual void PlayTurn(PieceMove[] piecemoves1, PieceMove[] piecemoves2)
        {
            if (this.isGameOver())
                return;

            this.applyMoves(piecemoves1, piecemoves2, true);
            this.Draw();

            if (this.isGameOver())
                return;

            this.applyMoves(piecemoves1, piecemoves2, false);
            this.Draw();
        }

        #region SUBMETHODS

        //sets up the basic board7
        protected void setupBoard()
        {
            int board_size = 41;

            this.board = new Board(board_size);

            player_1_pieces = new List<Pawn_Piece>();
            player_2_pieces = new List<Pawn_Piece>();

            this.placePieces(Players.One, player_1_pieces, 1, 0);
            this.placePieces(Players.Two, player_2_pieces, board.size - 2, board.size - 1);

            //randomizes walls
            int wall_count = board_size * board_size / 10;
            Random r = new Random();

            for(int i = 0; i < wall_count; i++)
                while (!this.board.AddPiece(new Wall_Piece(r.Next(board_size), r.Next(board_size)))) { }
        }

        //sets up the pieces for a player
        protected virtual void placePieces(Players player, List<Pawn_Piece> pieces, int pawn_x, int king_x)
        {
            //place pawns
            for (int y = this.board.size / 2 - 2; y < this.board.size / 2 + 3; y++)
            {
                Pawn_Piece pawn = new Pawn_Piece(player, pawn_x, y);

                this.board.AddPiece(pawn);

                if (player == Players.One) this.player_1_pieces.Add(pawn);
                else if (player == Players.Two) this.player_2_pieces.Add(pawn);
            }

            //place king
            King_Piece king = new King_Piece(player, king_x, this.board.size / 2);

            this.board.AddPiece(king);

            if (player == Players.One) this.player_1_pieces.Add(king);
            else if (player == Players.Two) this.player_2_pieces.Add(king);
        }

        //applies all moves in arrays
        protected virtual void applyMoves(PieceMove[] player_1_moves, PieceMove[] player_2_moves, bool move1)
        {
            if (player_1_moves == null || player_2_moves == null)
                throw new Exception("Player(s) without set moves.");

            //sets defending bool
            this.applyDefendingMoves(player_1_moves, move1);
            this.applyDefendingMoves(player_2_moves, move1);

            //applies attacks
            this.applyAttackMoves(player_1_moves, move1);
            this.applyAttackMoves(player_2_moves, move1);

            //removes dead pieces
            this.removeDeadPieces(Players.One);
            this.removeDeadPieces(Players.Two);

            //generates move map
            bool[, ,] move_map = this.generateMoveMap(player_1_moves, player_2_moves, move1);

            //applies moving
            this.applyMovingMoves(player_1_moves, move_map, move1);
            this.applyMovingMoves(player_2_moves, move_map, move1);
        }

        //tries all moves to see if there is overlap
        protected virtual bool[, ,] generateMoveMap(PieceMove[] piecemoves1, PieceMove[] piecemoves2, bool move1)
        {
            bool[,,] move_map = new bool[this.board.size, this.board.size, 2];

            //adds all walls
            for (int x = 0; x < this.board.size; x++)
            {
                for(int y = 0; y < this.board.size; y++)
                {
                    if(this.board.Pieces[x,y].Name == "Wall")
                    {
                        move_map[x, y, 0] = true;
                        move_map[x, y, 1] = true;
                    }
                }
            }

                //all player 1 moves
                foreach (PieceMove pm in piecemoves1)
                {
                    int x = pm.Piece.X, y = pm.Piece.Y;

                    //if movement, apply to x,y
                    switch (move1 ? pm.Move1 : pm.Move2)
                    {
                        case Moves.Left: x--; break;
                        case Moves.Right: x++; break;
                        case Moves.Up: y--; break;
                        case Moves.Down: y++; break;
                    }

                    //set the second level to true if the first is already set
                    if (move_map[x, y, 0])
                    {
                        move_map[x, y, 1] = true;
                        //also set the current position
                        move_map[pm.Piece.X, pm.Piece.Y, 1] = true;
                    }
                    else
                        move_map[x, y, 0] = true;
                }

            //all player 2 moves
            foreach (PieceMove pm in piecemoves2)
            {
                int x = pm.Piece.X, y = pm.Piece.Y;

                //if movement, apply to x,y
                switch (move1 ? pm.Move1 : pm.Move2)
                {
                    case Moves.Left: x--; break;
                    case Moves.Right: x++; break;
                    case Moves.Up: y--; break;
                    case Moves.Down: y++; break;
                }

                //set the second level to true if the first is already set
                if (move_map[x, y, 0])
                {
                    move_map[x, y, 1] = true;
                    //also set the current position
                    move_map[pm.Piece.X, pm.Piece.Y, 1] = true;
                }
                else
                    move_map[x, y, 0] = true;
            }

            return move_map;
        }

        //checks and applies defending move
        protected virtual void applyDefendingMoves(PieceMove[] pms, bool move1)
        {
            foreach (PieceMove pm in pms)
                if (pm.Piece.Alive && (move1 ? pm.Move1 : pm.Move2) == Moves.Defend)
                    (pm.Piece as Pawn_Piece).Defending = true;
        }

        //attack move
        protected virtual void applyAttackMoves(PieceMove[] pms, bool move1)
        {
            foreach (PieceMove pm in pms)
            {
                //direction of attack
                bool positive_attack = false;
                bool vertical_attack = false;
                bool attacking = true;

                //sets direction
                switch (move1 ? pm.Move1 : pm.Move2)
                {
                    case Moves.Attack_Left: break;
                    case Moves.Attack_Right: positive_attack = true; break;
                    case Moves.Attack_Up: vertical_attack = true; break;
                    case Moves.Attack_Down: positive_attack = true; vertical_attack = true; break;
                    default: attacking = false; break;
                }

                if (pm.Piece.Alive && attacking)
                {
                    //if its a king we need to attack in a line
                    King_Piece kp = pm.Piece as King_Piece;
                    if (kp != null)
                        this.applyKingAttack(kp, positive_attack, vertical_attack);
                    else
                    {
                        //damage cell next to pawn
                        Pawn_Piece pp = pm.Piece;
                        if (positive_attack)
                        {
                            if (vertical_attack) this.board.DamagePiece(pp.X, pp.X + 1);
                            else this.board.DamagePiece(pp.X + 1, pp.X);
                        }
                        else
                        {
                            if (vertical_attack) this.board.DamagePiece(pp.X, pp.X - 1);
                            else this.board.DamagePiece(pp.X - 1, pp.X);
                        }
                    }
                }
            }
        }

        //attacks in a straght line in a given direction
        protected virtual void applyKingAttack(King_Piece kp, bool positive_attack, bool vertical_attack)
        {
            if (vertical_attack)
            {
                if (positive_attack)
                {
                    for (int y = kp.Y + 1; y < this.board.size; y++)
                    {
                        //if you hit something, stop
                        if (this.board.DamagePiece(kp.X, y))
                            y = this.board.size;
                    }
                }
                else
                {
                    for (int y = kp.Y - 1; y > 0; y--)
                    {
                        //if you hit something, stop
                        if (this.board.DamagePiece(kp.X, y))
                            y = 0;
                    }
                }
            }
            else
            {
                if (positive_attack)
                {
                    for (int x = kp.X + 1; x < this.board.size; x++)
                    {
                        //if you hit something, stop
                        if (this.board.DamagePiece(x, kp.Y))
                            x = this.board.size;
                    }
                }
                else
                {
                    for (int x = kp.X - 1; x > 0; x--)
                    {
                        //if you hit something, stop
                        if (this.board.DamagePiece(x, kp.Y))
                            x = 0;
                    }
                }
            }
        }

        //removes dead pieces
        protected virtual void removeDeadPieces(Players player)
        {
            if(player == Players.One)
            {
                for(int i = 0; i < this.player_1_pieces.Count; i++)
                {
                    Pawn_Piece p = this.player_1_pieces[i];
                    if(!p.Alive)
                    {
                        if(p as King_Piece != null)
                        {
                            this.game_over = true;
                            this.winner = Players.Two;
                        }

                        //remove from board and player pieces
                        this.board.RemovePiece(p);
                        this.player_1_pieces.Remove(p);
                    }
                }
            }
            else if(player == Players.Two)
            {
                for(int i = 0; i < this.player_2_pieces.Count; i++)
                {
                    Pawn_Piece p = this.player_2_pieces[i];
                    if(!p.Alive)
                    {
                        if (p as King_Piece != null)
                        {
                            //if the winner is player 2, tie game
                            this.game_over = true;
                            if(this.winner == Players.Two)
                                this.winner = Players.World;
                            else
                                this.winner = Players.One;
                        }

                        //remove from board and player pieces
                        this.board.RemovePiece(p);
                        this.player_2_pieces.Remove(p);
                    }
                }
            }
        }

        //applies movement based on moving map
        protected virtual void applyMovingMoves(PieceMove[] pms, bool[,,] move_map, bool move1)
        {
            foreach (PieceMove pm in pms)
            {
                //only move if alive
                if(pm.Piece.Alive)
                {
                    int x = pm.Piece.X, y = pm.Piece.Y;
                    bool moving = true;

                    //if movement, apply to x,y
                    switch (move1 ? pm.Move1 : pm.Move2)
                    {
                        case Moves.Left: x--; break;
                        case Moves.Right: x++; break;
                        case Moves.Up: y--; break;
                        case Moves.Down: y++; break;
                        default: moving = false; break;
                    }

                    if(moving)
                    {
                        //check second level of map
                        if (!move_map[x, y, 1])
                        {
                            this.board.MovePiece(pm.Piece, x, y);
                        }
                    }
                }
            }
        }

        //generates a board relative to a certain player
        protected virtual Board generateRelativeBoard(Players player)
        {
            if (player == Players.World) return this.board;

            Pawn_Piece[] pieces = player == Players.One ? this.player_1_pieces.ToArray() : this.player_2_pieces.ToArray();

            bool[,] vision_map = this.generateVisionMap(pieces);

            Board b = new Board(this.board.size);

            for(int x = 0; x < this.board.size; x++)
            {
                for(int y = 0; y < this.board.size; y++)
                {
                    if (vision_map[x, y])
                        b.AddPiece(this.board.Pieces[x, y]);
                    else
                        b.AddPiece(new Unknown_Piece(x, y));
                }
            }

            return b;
        }

        //generates a map of visible pieces
        protected virtual bool[,] generateVisionMap(Pawn_Piece[] pieces)
        {
            bool[,] vis_map = new bool[this.board.size, this.board.size];

            foreach (Pawn_Piece piece in pieces)
            {
                for (int x = -piece.Vision; x <= piece.Vision; x++)
                {
                    for (int y = -piece.Vision; y <= piece.Vision; y++)
                    {
                        int relX = piece.X - x;
                        int relY = piece.Y - y;

                        //within grid and vision radius
                        if (relX >= 0 && relX < this.board.size && relY >= 0 && relY < this.board.size
                            && Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2)) <= piece.Vision)
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
        #endregion
    }
}
