using Ruler_s_Fate.Entidades.Personagens;
using System;

namespace Ruler_s_Fate.Sistemas.Core
{
    /// <summary>
    /// Envenenado ☠️: efeito de dano por veneno que piora a cada turno.
    /// O dano inicial aumenta em <c>aumentoPorTurno</c> a cada ciclo,
    /// simulando um veneno que se espalhando pelo corpo.
    /// </summary>
    public class Envenenado : EfeitoStatus
    {
        private int danoPorTurno;
        private readonly int aumentoPorTurno;

        /// <param name="duracao">Número de turnos que o veneno dura.</param>
        /// <param name="dano">Dano inicial por turno.</param>
        /// <param name="aumento">Quanto o dano cresce a cada turno (padrão: +1).</param>
        public Envenenado(int duracao, int dano, int aumento = 1)
            : base("Envenenado ☠️", duracao)
        {
            danoPorTurno = dano;
            aumentoPorTurno = aumento;
        }

        public override void AoAplicar(PersonagemBase alvo)
        {
            Console.WriteLine($"  ☠️ O veneno começa a corroer {alvo.Nome}...");
        }

        public override void ProcessarTurno(PersonagemBase alvo)
        {
            Console.WriteLine($"  ☠️ {alvo.Nome} sofre {danoPorTurno} de dano por veneno!");
            alvo.ReceberDano(danoPorTurno);
            danoPorTurno += aumentoPorTurno; // O veneno piora a cada turno
        }
    }
}
