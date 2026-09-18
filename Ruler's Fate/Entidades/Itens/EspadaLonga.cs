using Ruler_s_Fate.Entidades.Personagens;

namespace Ruler_s_Fate.Entidades.Itens
{
    /// <summary>
    /// Espada Longa de Aço — equipamento de slot Arma.
    /// Ao equipar, aumenta a Força do portador em +10.
    /// Ao desequipar, reverte o bônus automaticamente.
    /// </summary>
    public class EspadaLonga : Equipamento
    {
        // 'const' permite uso no construtor base (campos de instância não podem)
        private const int bonusForca = 10;

        public EspadaLonga()
            : base("Espada Longa de Aço", $"Aumenta a Força em +{bonusForca}.", TipoSlot.Arma)
        {
        }

        public override void Equipar(PersonagemBase alvo)
        {
            base.Equipar(alvo);              // Exibe a mensagem e registra no slot
            alvo.ModificarForca(bonusForca); // Aplica o buff real de Força
        }

        public override void Desequipar(PersonagemBase alvo)
        {
            base.Desequipar(alvo);
            alvo.ModificarForca(-bonusForca); // Remove o buff ao guardar a espada
        }
    }
}
