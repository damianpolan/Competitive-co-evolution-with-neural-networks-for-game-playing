using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COMP4106_Project.Game
{
    public struct Move
    {
        public Direction direction;
        public MoveType type;

        public Move(MoveType moveType, Direction moveDirection)
        {
            this.type = moveType;
            this.direction = moveDirection;
        }
    }
}
