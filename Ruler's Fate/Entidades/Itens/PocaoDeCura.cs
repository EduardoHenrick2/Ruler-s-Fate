using Ruler_s_Fate.Entidades.Personagens;
using System;

namespace Ruler_s_Fate.Entidades.Itens
{
    /// <summary>
    /// Poção que restaura Vida do personagem alvo.
    /// </summary>
    public class PocaoDeCura : Item
    {
        private readonly int poderDeCura;

        /// <summary>
        /// Cria uma poção de cura. Os valores padrão representam a versão básica;
        /// versões mais poderosas podem ser criadas passando valores maiores.
        /// </summary>
        public PocaoDeCura(string nome = "Poção de Cura Básica", int cura = 40)
            : base(nome, $"Uma poção avermelhada que restaura {cura} de Vida.")
        {
            poderDeCura = cura;
        }

        public override void Usar(PersonagemBase alvo)
        {
            Console.WriteLine($"\n{alvo.Nome} destampa a {Nome} e bebe de um só gole.");
            alvo.RestaurarVida(poderDeCura);
        }
    }
}
