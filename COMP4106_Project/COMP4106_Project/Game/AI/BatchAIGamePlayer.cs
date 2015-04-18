using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COMP4106_Project.Game.AI
{
    public class BatchAIGamePlayer
    {
        Board game;
        AI ai1, ai2;
        public BatchAIGamePlayer(Board game, AI ai1, AI ai2)
        {
            this.game = game;
            this.ai1 = ai1;
            this.ai2 = ai2;
        }


        public int playSingleGame()
        {
            while (!game.IsGameOver())
            {
                VisibleState A1 = game.GetVisibleStateForPlayer(0);
                VisibleState A2 = game.GetVisibleStateForPlayer(1);


                //todo
            }

            return -1;
        }

    }
}
