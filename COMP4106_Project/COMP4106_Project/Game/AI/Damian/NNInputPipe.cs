using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COMP4106_Project.Game.AI.Damian
{
    public static class NNInputPipe
    {
        /// <summary>
        /// Filters the board information from the perspective of a singlePiece.
        /// Simple: 
        ///  [0 - 3] - indicating direction of enemy pieces. intensity correlated to distance. (1 = adjacent, 0 = none). 
        ///             If there is more than one enemy toward a certain direction, the distance of the closest enemy is chosen.
        ///  [4]     - 0 - 1 health value of self 
        /// 
        /// Output:
        /// { inputFunctionN, inputFunctionE, inputFunctionS, inputFunctionW, healthRatio }
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <param name="playerIdPerspective"></param>
        /// <returns></returns>
        public static double[] pipe_fromPerspective_Simple(VisibleState state, int pieceIdPerspective)
        {
            RelPieces relState = new RelPieces(state, pieceIdPerspective);


            double[] values = new double[5];
            //distance equation must compute in range [0 ... 1]
            //using equation double F(p) =  1 / (2 ^ (ManhattenDist(p) - 1))

            RelPieces.RelPiece closestN = null;
            RelPieces.RelPiece closestE = null;
            RelPieces.RelPiece closestS = null;
            RelPieces.RelPiece closestW = null;

            //finds all closest
            for (int i = 0; i < relState.enemy.Length; i++)
            {
                RelPieces.RelPiece thisEnemy = relState.enemy[i];

                if (thisEnemy.yRel < 0) // north of
                {
                    if (closestN == null || thisEnemy.ManhattenDist < closestN.ManhattenDist) closestN = thisEnemy;
                }
                else if (thisEnemy.yRel > 0) //south of
                {
                    if (closestS == null || thisEnemy.ManhattenDist < closestS.ManhattenDist) closestS = thisEnemy;
                }

                if (thisEnemy.xRel < 0) // west of
                {
                    if (closestW == null || thisEnemy.ManhattenDist < closestW.ManhattenDist) closestW = thisEnemy;
                }
                else if (thisEnemy.xRel > 0) //east of
                {
                    if (closestE == null || thisEnemy.ManhattenDist < closestE.ManhattenDist) closestE = thisEnemy;
                }
            }


            double inputFunctionN = closestN == null ? 0 : 1d / Math.Pow(2, closestN.ManhattenDist - 1d);
            double inputFunctionE = closestE == null ? 0 : 1d / Math.Pow(2, closestE.ManhattenDist - 1d);
            double inputFunctionS = closestS == null ? 0 : 1d / Math.Pow(2, closestS.ManhattenDist - 1d);
            double inputFunctionW = closestW == null ? 0 : 1d / Math.Pow(2, closestW.ManhattenDist - 1d);

            double healthRatio = (double) Piece.STARTING_HEALTH / (double) relState.self.health;


            Console.WriteLine("Inputs - Perspective Simple");
            Console.WriteLine("Inputs - Perspective Simple");
            Console.WriteLine("Inputs - Perspective Simple");
            Console.WriteLine("Inputs - Perspective Simple");

            return new double[] { inputFunctionN, inputFunctionE, inputFunctionS, inputFunctionW, healthRatio };
        }
        
        private class RelPieces
        {
            public RelPiece self;
            public RelPiece[] team;
            public RelPiece[] enemy;
            public RelPiece[] blocks;

            public RelPieces(VisibleState state, int pieceIdPerspective)
            {
                //find the self palyer piece matching pieceIdPerspective
                for (int i = 0; i < state.player.Length; i++)
                    if (state.player[i].id == pieceIdPerspective)
                    {
                        self = new RelPiece(state.player[i], state.player[i].x, state.player[i].y);
                        i = state.player.Length;
                    }


                //populate team with relative pieces
                team = new RelPiece[state.player.Length - 1];
                int count = 0;
                for (int i = 0; i < state.player.Length; i++)
                {
                    if (state.player[i].id != pieceIdPerspective)
                    {
                        team[count] = new RelPiece(state.player[i], self.xRel, self.yRel);
                        count++;
                    }
                }

                //populate enemy with enemy pieces
                enemy = new RelPiece[state.enemy.Length];
                for (int i = 0; i < state.enemy.Length; i++)
                    enemy[i] = new RelPiece(state.enemy[i], self.xRel, self.yRel);


                //populate blocks with block board locations
                blocks = new RelPiece[state.blocks.Length];
                for (int i = 0; i < state.blocks.Length; i++)
                    blocks[i] = new RelPiece(state.blocks[i], self.xRel, self.yRel);

            }

            public class RelPiece
            {
                public int id, health, vision, defence, xRel, yRel;
                public string type;

                public RelPiece(BoardLocation l, int midX, int midY)
                {
                    type = l.type;
                    xRel = l.x - midX;
                    yRel = l.y - midY;

                    if (l is Piece)
                    {
                        Piece p = (Piece)l;
                        this.id = p.id;
                        this.health = p.health;
                        this.vision = p.vision;
                        this.defence = p.defence;
                    }
                }

                public int ManhattenDist
                {
                    get
                    {
                        return Math.Abs(xRel) + Math.Abs(yRel);
                    }
                }

            }
        }

    }
}
