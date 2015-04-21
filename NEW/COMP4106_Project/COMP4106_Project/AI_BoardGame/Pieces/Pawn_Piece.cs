using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP4106_Project.AI_BoardGame.Pieces
{
    public class Pawn_Piece : Piece, ICloneable
    {
        public int Health;
        public int Vision;
        public bool Alive;
        public bool Defending;

        public Pawn_Piece(Players player, int x, int y)
            : this(player,"Pawn", 'o', x, y, 4, 2)
        {
        }

        protected Pawn_Piece(Players player, string name, char character, int x, int y, int health, int vision)
            : base(player, name, character, x, y)
        {
            this.Health = health;
            this.Vision = vision;
            this.Defending = false;
            this.Alive = true;
        }

        public virtual object Clone()
        {
            return new Pawn_Piece(this.Player, this.X, this.Y);
        }
    }
}
