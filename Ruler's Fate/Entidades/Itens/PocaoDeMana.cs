using Ruler_s_Fate.Entidades.Personagens;
using System;

namespace Ruler_s_Fate.Entidades.Itens
{
    /// <summary>
    /// Poção que restaura Mana do personagem alvo.
    /// </summary>
    public class PocaoDeMana : Item
    {
        private readonly int poderDeMana;

        public PocaoDeMana(string nome = "Poção de Mana Básica", int mana = 30)
            : base(nome, $"Um líquido azul e brilhante que restaura {mana} de Mana.")
        {
            poderDeMana = mana;
        }

        public override void Usar(PersonagemBase alvo)
        {
            Console.WriteLine($"\n{alvo.Nome} consome a {Nome}, sentindo sua energia mágica retornar.");
            alvo.RestaurarMana(poderDeMana);
        }
    }
}
