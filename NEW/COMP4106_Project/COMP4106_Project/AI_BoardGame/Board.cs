using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using COMP4106_Project.AI_BoardGame.Pieces;

namespace COMP4106_Project.AI_BoardGame
{
    public class Board
    {
        public Piece[,] Pieces;

        public int size;

        protected string outline;


        public Board(int size)
        {
            Pieces = new Piece[size, size];
            this.size = size;

            this.outline = "--";
            for (int i = 0; i < size; i++)
                this.outline += "-";

                this.setup();
        }

        //draws the board from a given perspective
        public virtual void Draw(Players playerPerspective = Players.World)
        {
            if (playerPerspective == Players.World)
            {
                this.drawWorldPerspective();
                return;
            }

        }

        //adds a piece to the board
        public bool AddPiece(Piece p)
        {
            if (Pieces[p.X, p.Y].Name != "Empty" && Pieces[p.X, p.Y].Name != "Unknown")
                return false;

            Pieces[p.X, p.Y] = p;
            return true;
        }

        //moves a piece by a given x, y
        public bool MovePiece(Piece p, int x, int y)
        {
            if (x < 0 || x >= this.size || y < 0 || y >= this.size)
                return false;

            if (Pieces[x, y].Name != "Empty")
                return false;

            //replaces old piece with empty
            this.Pieces[p.X, p.Y] = new Empty_Piece(p.X, p.Y);

            //places new piece
            p.X = x;
            p.Y = y;
            this.Pieces[x, y] = p;

            return true;
        }

        //damages a piece at a given x,y
        public bool DamagePiece(int x, int y, int amount = 1)
        {
            //return false if out of bounds
            if (x < 0 || x >= this.size || y < 0 || y >= this.size)
                return false;

            //return false if empty piece
            if (this.Pieces[x, y].Name == "Empty")
                return false;
            if(this.Pieces[x, y].Name == "Wall")
                return true;

            Pawn_Piece piece = this.Pieces[x, y] as Pawn_Piece;

            //if defending, block the blow
            if (piece.Defending)
                piece.Defending = false;
            else
                piece.Health -= amount;

            if (piece.Health <= 0)
                piece.Alive = false;

            return true;
        }

        //removes a piece from the baord
        public void RemovePiece(Piece p)
        {
            this.Pieces[p.X, p.Y] = new Empty_Piece(p.X, p.Y);
        }

        #region SUBMETHODS

        //sets up the board with empty pieces
        protected virtual void setup()
        {
            //fills entire board with empty
            for (int x = 0; x < this.size; x++)
            {
                for (int y = 0; y < this.size; y++)
                {
                    this.Pieces[x, y] = new Empty_Piece(x, y);
                }
            }
        }

        //draws the board from an all visible perspective
        protected virtual void drawWorldPerspective()
        {
            Console.WriteLine(this.outline);

            for (int y = 0; y < this.size; y++)
            {
                Console.Write("|");
                for (int x = 0; x < this.size; x++)
                {
                    Console.Write(this.Pieces[x, y].Character);
                }
                Console.Write("|");
                Console.WriteLine();
            }

            Console.WriteLine(this.outline);
        }

        #endregion
    }
}
