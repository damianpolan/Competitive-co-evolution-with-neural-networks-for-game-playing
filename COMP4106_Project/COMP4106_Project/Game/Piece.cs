using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COMP4106_Project.Game
{
    public class Piece : BoardLocation
    {
        static int idCounter = 0;
        static int STARTING_HEALTH = 4;
        static int PAWN_VISION = 3;
        static int PAWN_DEFENCE = 0;


        public int id, player, health, vision, defence;

        public Piece(int xPos, int yPos, int player)
            : base(xPos, yPos)
        {
            this.id = Piece.idCounter++;
            this.player = player;
            this.health = STARTING_HEALTH;
            this.vision = PAWN_VISION;
            this.defence = PAWN_DEFENCE;
            base.type = "pawn";
        }

        public void damage()
        {
            health--;
        }

        public bool isDead()
        {
            return health <= 0;
        }

    }
}
