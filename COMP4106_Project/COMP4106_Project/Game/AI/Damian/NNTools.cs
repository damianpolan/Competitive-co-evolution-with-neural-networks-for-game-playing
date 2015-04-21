using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AForge.Neuro;

namespace COMP4106_Project.Game.AI.Damian
{
    public static class NNTools
    {
        public static void printNN(Network n, String name = null)
        {
            if (name != null)
                Console.WriteLine(name);
            for (int i = 0; i < n.Layers.Length; i++)
            {
                for (int j = 0; j < n.Layers[i].Neurons.Length; j++)
                {
                    for (int k = 0; k < n.Layers[i].Neurons[j].Weights.Length; k++)
                    {
                        Console.Write(n.Layers[i].Neurons[j].Weights[k] + " ");
                    }
                    Console.WriteLine(" | ");
                }
                Console.WriteLine("\n");
            }
        }

    }
}
