using System;

namespace Ruler_s_Fate.Entidades.Personagens
{
    /// <summary>
    /// Jackpot: personagem de alto risco/recompensa que depende da sorte para seus ataques.
    /// Status equilibrados, mas o dano varia drasticamente com os dados.
    /// </summary>
    public class Jackpot : PersonagemBase
    {
        private readonly Random dadoSorte = new Random();

        public Jackpot(string nome)
            : base(nome, vida: 120, mana: 50, forca: 15, inteligencia: 15, velocidade: 15)
        {
        }

        /// <summary>
        /// Rola um dado de 6 faces. 1 = Falha Crítica, 6 = JACKPOT (dano triplo),
        /// qualquer outro = dano normal de Força.
        /// </summary>
        public void DadoViciado(PersonagemBase alvo)
        {
            int rolagem = dadoSorte.Next(1, 7); // Rola de 1 a 6
            Console.WriteLine($"\n{Nome} joga o Dado Viciado para o alto... Caiu {rolagem}!");

            if (rolagem == 1)
            {
                Console.WriteLine("Falha Crítica! O ataque falhou miseravelmente e não causou dano.");
            }
            else if (rolagem == 6)
            {
                int danoTriplo = Forca * 3;
                Console.WriteLine("JACKPOT!!! Acerto em cheio com dano triplo!");
                alvo.ReceberDano(danoTriplo);
            }
            else
            {
                Console.WriteLine("Acerto normal.");
                alvo.ReceberDano(Forca);
            }
        }
    }
}
