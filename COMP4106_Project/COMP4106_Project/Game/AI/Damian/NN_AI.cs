using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using COMP4106_Project.Game.AI;
using AForge.Neuro;

namespace COMP4106_Project.Game.AI.Damian
{
    /// <summary>
    /// 
    /// </summary>
    public class NN_AI : AI
    {
        public NN_AI(int player)
            : base(player)
        {

        }

        public override Move[] requestMove(VisibleState state)
        {
            for (int p = 0; p < state.player.Length; p++)
            {
                double[] inputSet = NNInputPipe.pipe_fromPerspective_Simple(state, state.player[p].id);
                ActivationNetwork network = new ActivationNetwork(new SigmoidFunction(2), inputSet.Length, 10, 7, 5, 2);
            }





            throw new NotImplementedException();
        }
    }
}
