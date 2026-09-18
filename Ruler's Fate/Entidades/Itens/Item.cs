using Ruler_s_Fate.Entidades.Personagens;
using System;

namespace Ruler_s_Fate.Entidades.Itens
{
    /// <summary>
    /// Classe base para todos os itens do jogo.
    /// Itens concretos devem sobrescrever o método <see cref="Usar"/>.
    /// </summary>
    public class Item
    {
        public string Nome { get; protected set; }
        public string Descricao { get; protected set; }

        public Item(string nome, string descricao)
        {
            Nome = nome;
            Descricao = descricao;
        }

        /// <summary>
        /// Define se o item deve ser removido do inventário após ser usado.
        /// Itens consumíveis (poções) retornam <c>true</c> (padrão).
        /// Equipamentos retornam <c>false</c> — eles permanecem na mochila.
        /// </summary>
        public virtual bool RemoverAoUsar => true;

        /// <summary>
        /// Usa o item no personagem alvo. Pode ser sobrescrito por itens especializados.
        /// </summary>
        public virtual void Usar(PersonagemBase alvo)
        {
            Console.WriteLine($"{alvo.Nome} preparou o uso de {Nome}.");
        }
    }
}
