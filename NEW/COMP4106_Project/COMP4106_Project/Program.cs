using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using COMP4106_Project.AI_BoardGame;
using COMP4106_Project.AI_BoardGame.Pieces;

namespace COMP4106_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            AI_Game game = new AI_Game();

            game.Draw();

            for (int i = 0; i < 50; i++)
            {
                if (game.isGameOver())
                    i = 10000;

                Console.ReadLine();

                Pawn_Piece[] p1s = game.getPieces(Players.One);
                PieceMove[] p1m = new PieceMove[p1s.Length];

                for (int x = 0; x < p1s.Length; x++)
                    p1m[x] = new PieceMove(p1s[x], Moves.Right, Moves.Right);

                Pawn_Piece[] p2s = game.getPieces(Players.Two);
                PieceMove[] p2m = new PieceMove[p2s.Length];

                for (int x = 0; x < p2s.Length; x++)
                    p2m[x] = new PieceMove(p2s[x], Moves.Left, Moves.Attack_Left);

                game.PlayTurn(p1m, p2m);

                Console.WriteLine("Player 1 Vision");
                game.getRelativeState(Players.One).Draw();
                Console.WriteLine("Player 2 Vision");
                game.getRelativeState(Players.Two).Draw();
            }

            Console.WriteLine("Winner is " + game.getWinner());

            Console.ReadLine();
        }
    }
}
