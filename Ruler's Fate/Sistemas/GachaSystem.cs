using System;
using System.Collections.Generic;
using System.Text;

namespace Ruler_s_Fate.Sistemas
{
    public static class GachaSystem
    {
        private static Random rng = new Random();

        public static Raca RolarGacha() 
        {
            int sorteio = rng.Next(1, 101); //gera um numero de 1 a 100

            if (sorteio <= 40) return new Humano(); //40% de chance

            if (sorteio <= 70) return new Anao(); //30% de chance

            if (sorteio <= 90) return new Goblin(); // 20$ de chance

            return new Elfo(); // 10% de chance
        }
    }
}
