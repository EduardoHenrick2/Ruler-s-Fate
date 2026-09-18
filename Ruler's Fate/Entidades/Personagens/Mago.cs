using Ruler_s_Fate.Sistemas.Core;
using System;

namespace Ruler_s_Fate.Entidades.Personagens
{
    /// <summary>
    /// Mago: pouca vida e força, mas alta Mana e Inteligência.
    /// Especialidade: Bola de Fogo — causa dano e aplica <see cref="Queimando"/>.
    /// </summary>
    public class Mago : PersonagemBase
    {
        public Mago(string nome)
            : base(nome, vida: 100, mana: 80, forca: 5, inteligencia: 25, velocidade: 12)
        {
        }

        /// <summary>
        /// Lança uma Bola de Fogo no alvo. Consome 20 de Mana, causa dano
        /// igual a Inteligência × 2 e aplica o efeito <see cref="Queimando"/>
        /// por 3 turnos (5 de dano por turno).
        /// </summary>
        public void BolaDeFogo(PersonagemBase alvo)
        {
            if (Mana >= 20)
            {
                Mana -= 20;
                int dano = Inteligencia * 2;

                Console.WriteLine($"{Nome} lança uma BOLA DE FOGO chamejante em {alvo.Nome}!");
                alvo.ReceberDano(dano);

                // Aplica o efeito Queimando por 3 turnos, causando 5 de dano por turno!
                alvo.AdicionarStatus(new Queimando(duracao: 3, dano: 5));
            }
            else
            {
                Console.WriteLine($"{Nome} tenta conjurar Bola de Fogo, mas está sem Mana!");
            }
        }
    }
}
