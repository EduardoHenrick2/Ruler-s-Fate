using Ruler_s_Fate.Sistemas.Racas;
using System;

namespace Ruler_s_Fate.Sistemas.Core
{
    /// <summary>
    /// Sistema de Gacha responsável por sortear uma raça aleatória para o personagem.
    /// As probabilidades são fixas: Humano 40%, Anão 30%, Goblin 20%, Elfo 10%.
    /// </summary>
    public static class GachaSystem
    {
        private static readonly Random rng = new Random();

        /// <summary>
        /// Sorteia uma raça aleatória com as probabilidades definidas do jogo.
        /// </summary>
        /// <returns>Uma instância da raça sorteada.</returns>
        public static Raca RolarGacha()
        {
            int sorteio = rng.Next(1, 101); // gera um número de 1 a 100

            if (sorteio <= 40) return new Humano();  // 40% de chance
            if (sorteio <= 70) return new Anao();    // 30% de chance
            if (sorteio <= 90) return new Goblin();  // 20% de chance

            return new Elfo();                       // 10% de chance
        }
    }
}
