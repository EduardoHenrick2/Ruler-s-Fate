using Ruler_s_Fate.Entidades.Personagens;
using System;

namespace Ruler_s_Fate.Entidades.Itens
{
    /// <summary>
    /// Item que aumenta permanentemente a capacidade do inventário do personagem.
    /// Mochilas de diferentes níveis podem ser criadas passando valores distintos.
    /// </summary>
    public class UpgradeMochila : Item
    {
        private readonly int espacosConcedidos;

        public UpgradeMochila(string nome, int espacos)
            : base(nome, $"Aumenta o espaço do seu inventário em {espacos} slots permanentes.")
        {
            espacosConcedidos = espacos;
        }

        public override void Usar(PersonagemBase alvo)
        {
            Console.WriteLine($"{alvo.Nome} equipou a {Nome}!");
            // Acessa o inventário do alvo e chama o método de expansão
            alvo.Mochila.AumentarCapacidade(espacosConcedidos);
        }
    }
}
