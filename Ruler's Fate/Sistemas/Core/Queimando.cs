using Ruler_s_Fate.Entidades.Personagens;
using System;

namespace Ruler_s_Fate.Sistemas.Core
{
    /// <summary>
    /// Queimando 🔥: efeito de dano por fogo aplicado a cada turno.
    /// Causado pela habilidade <c>BolaDeFogo</c> do Mago.
    /// </summary>
    public class Queimando : EfeitoStatus
    {
        private readonly int danoPorTurno;

        /// <param name="duracao">Número de turnos que o efeito dura.</param>
        /// <param name="dano">Dano causado a cada turno.</param>
        public Queimando(int duracao, int dano)
            : base("Queimando 🔥", duracao)
        {
            danoPorTurno = dano;
        }

        public override void ProcessarTurno(PersonagemBase alvo)
        {
            Console.WriteLine($"  🔥 {alvo.Nome} está em chamas e sofre {danoPorTurno} de dano!");
            alvo.ReceberDano(danoPorTurno);
        }
    }
}
