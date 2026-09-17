using System;

namespace Ruler_s_Fate.Entidades.Personagens
{
    /// <summary>
    /// Mago: pouca vida e força, mas alta Mana e Inteligência.
    /// Especialidade: Bola de Fogo.
    /// </summary>
    public class Mago : PersonagemBase
    {
        public Mago(string nome)
            : base(nome, vida: 100, mana: 80, forca: 5, inteligencia: 25, velocidade: 12)
        {
        }

        /// <summary>
        /// Lança uma Bola de Fogo no alvo. Consome 20 de Mana e causa
        /// dano igual a Inteligência * 2.
        /// </summary>
        public void BolaDeFogo(PersonagemBase alvo)
        {
            if (Mana >= 20)
            {
                Mana -= 20;
                int dano = Inteligencia * 2; // Dano baseado na inteligência

                Console.WriteLine($"{Nome} lança uma BOLA DE FOGO chamejante em {alvo.Nome}!");
                alvo.ReceberDano(dano);

                // Nota: O efeito "Queimando" será adicionado futuramente no sistema de turnos
            }
            else
            {
                Console.WriteLine($"{Nome} tenta conjurar Bola de Fogo, mas está sem Mana!");
            }
        }
    }
}
