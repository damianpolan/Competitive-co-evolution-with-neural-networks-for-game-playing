using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP4106_Project.AI_BoardGame
{
    public class Piece
    {
        public Players Player;

        public string Name;

        public char Character;

        public int X, Y;

        public Piece(Players player, string name, char character, int x, int y)
        {
            this.Player = player;
            this.Name = name;
            this.Character = character;
            this.X = x;
            this.Y = y;
        }
    }
}
