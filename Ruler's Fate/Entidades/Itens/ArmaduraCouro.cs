using Ruler_s_Fate.Entidades.Personagens;
using System;

namespace Ruler_s_Fate.Entidades.Itens
{
    /// <summary>
    /// Armadura de Couro — equipamento de slot Armadura.
    /// Aumenta a Vida Máxima do portador em +30 ao equipar
    /// e reverte o bônus ao desequipar.
    /// </summary>
    public class ArmaduraCouro : Equipamento
    {
        private const int bonusVida = 30;

        public ArmaduraCouro()
            : base("Armadura de Couro", $"Aumenta a Vida Máxima em +{bonusVida}.", TipoSlot.Armadura)
        {
        }

        public override void Equipar(PersonagemBase alvo)
        {
            base.Equipar(alvo);
            alvo.AumentarVidaMax(bonusVida);
            Console.WriteLine($"  ❤️ VidaMax de {alvo.Nome} aumentou para {alvo.VidaMax}.");
        }

        public override void Desequipar(PersonagemBase alvo)
        {
            base.Desequipar(alvo);
            alvo.AumentarVidaMax(-bonusVida);
            Console.WriteLine($"  ❤️ VidaMax de {alvo.Nome} voltou para {alvo.VidaMax}.");
        }
    }
}
