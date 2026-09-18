using Ruler_s_Fate.Entidades.Personagens;
using System;

namespace Ruler_s_Fate.Sistemas.Core
{
    /// <summary>
    /// Classe base abstrata para todos os efeitos de status que persistem
    /// ao longo de turnos (DoTs, buffs, debuffs, etc.).
    /// <para>
    /// Subclasses devem implementar <see cref="ProcessarTurno"/> e, opcionalmente,
    /// sobrescrever <see cref="AoAplicar"/> e <see cref="AoRemover"/> para lógica
    /// de início/fim (útil para buffs que precisam ser revertidos).
    /// </para>
    /// </summary>
    public abstract class EfeitoStatus
    {
        public string Nome { get; protected set; }
        public int TurnosRestantes { get; set; }

        protected EfeitoStatus(string nome, int duracao)
        {
            Nome = nome;
            TurnosRestantes = duracao;
        }

        /// <summary>
        /// Chamado uma vez logo quando o efeito é aplicado ao personagem.
        /// Use para aplicar modificações imediatas (ex: aumentar Força).
        /// </summary>
        public virtual void AoAplicar(PersonagemBase alvo) { }

        /// <summary>
        /// Chamado a cada turno enquanto o efeito estiver ativo.
        /// Aqui vai a lógica repetida (ex: causar dano de veneno).
        /// </summary>
        public abstract void ProcessarTurno(PersonagemBase alvo);

        /// <summary>
        /// Chamado uma vez quando o efeito expira naturalmente.
        /// Use para reverter modificações de buffs (ex: reduzir Força de volta).
        /// </summary>
        public virtual void AoRemover(PersonagemBase alvo) { }
    }
}