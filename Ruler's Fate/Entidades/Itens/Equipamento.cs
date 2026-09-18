using Ruler_s_Fate.Entidades.Personagens;
using System;

namespace Ruler_s_Fate.Entidades.Itens
{
    /// <summary>
    /// Define os locais do corpo onde um item pode ser vestido.
    /// Garante que o personagem não equipe dois itens no mesmo slot.
    /// </summary>
    public enum TipoSlot { Arma, Armadura, Acessorio }

    /// <summary>
    /// Classe base para todos os equipamentos (armas, armaduras, acessórios).
    /// Diferentemente dos itens consumíveis, equipamentos <b>não somem</b> do
    /// inventário ao serem usados — eles entram no estado "Equipado" e aplicam
    /// bônus constantes até serem desequipados.
    /// </summary>
    public abstract class Equipamento : Item
    {
        public TipoSlot Slot { get; protected set; }
        public bool EstaEquipado { get; protected set; } = false;

        /// <summary>Equipamentos permanecem no inventário ao serem usados.</summary>
        public override bool RemoverAoUsar => false;

        public Equipamento(string nome, string descricao, TipoSlot slot)
            : base(nome, descricao)
        {
            Slot = slot;
        }

        /// <summary>
        /// Funciona como interruptor liga/desliga:
        /// chamar <c>Usar</c> equipa ou desequipa conforme o estado atual.
        /// </summary>
        public override void Usar(PersonagemBase alvo)
        {
            if (EstaEquipado)
                Desequipar(alvo);
            else
                Equipar(alvo);
        }

        /// <summary>
        /// Equipa o item no personagem, registrando-o no slot correto via
        /// <see cref="PersonagemBase.VestirEquipamento"/>. Subclasses devem
        /// chamar <c>base.Equipar(alvo)</c> e depois aplicar seus bônus.
        /// </summary>
        public virtual void Equipar(PersonagemBase alvo)
        {
            EstaEquipado = true;
            alvo.VestirEquipamento(this); // Avisa ao corpo que o slot foi ocupado
            Console.WriteLine($"\n🛡️ {alvo.Nome} equipou: {Nome}!");
        }

        /// <summary>
        /// Desequipa o item, liberando o slot. Subclasses devem chamar
        /// <c>base.Desequipar(alvo)</c> e depois remover seus bônus.
        /// </summary>
        public virtual void Desequipar(PersonagemBase alvo)
        {
            EstaEquipado = false;
            Console.WriteLine($"\n🎒 {alvo.Nome} desequipou: {Nome}.");
        }
    }
}
