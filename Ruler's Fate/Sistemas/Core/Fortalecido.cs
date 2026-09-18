using Ruler_s_Fate.Entidades.Personagens;
using System;

namespace Ruler_s_Fate.Sistemas.Core
{
    /// <summary>
    /// Fortalecido ⚔️: buff temporário que aumenta a Força do personagem.
    /// A Força extra é revertida automaticamente quando o efeito expira,
    /// usando os métodos <see cref="EfeitoStatus.AoAplicar"/> e <see cref="EfeitoStatus.AoRemover"/>.
    /// </summary>
    public class Fortalecido : EfeitoStatus
    {
        private readonly int bonusForca;

        /// <param name="duracao">Número de turnos que o buff dura.</param>
        /// <param name="bonus">Quantidade de Força adicionada temporariamente.</param>
        public Fortalecido(int duracao, int bonus)
            : base("Fortalecido ⚔️", duracao)
        {
            bonusForca = bonus;
        }

        /// <summary>Aplica o bônus de Força imediatamente ao receber o efeito.</summary>
        public override void AoAplicar(PersonagemBase alvo)
        {
            alvo.ModificarForca(bonusForca);
            Console.WriteLine($"  ⚔️ {alvo.Nome} sente um poder incrível! +{bonusForca} de Força por {TurnosRestantes} turnos.");
        }

        /// <summary>
        /// Nenhuma ação por turno — o buff apenas persiste até expirar.
        /// </summary>
        public override void ProcessarTurno(PersonagemBase alvo) { }

        /// <summary>Reverte a Força de volta ao valor original quando o efeito termina.</summary>
        public override void AoRemover(PersonagemBase alvo)
        {
            alvo.ModificarForca(-bonusForca);
        }
    }
}
