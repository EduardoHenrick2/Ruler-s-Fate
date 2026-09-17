using System;

namespace Ruler_s_Fate.Entidades.Personagens
{
    /// <summary>
    /// Aliado invocado: tem vida e pode atacar, mas dura apenas
    /// um número limitado de turnos antes de se dissipar.
    /// </summary>
    public class Aliado : PersonagemBase
    {
        public int TurnosDeVida { get; private set; }

        /// <summary>
        /// Cria um aliado invocado que dura <paramref name="turnos"/> turnos.
        /// </summary>
        public Aliado(string nome, int vida, int forca, int turnos)
            : base(nome, vida, mana: 0, forca, inteligencia: 0, velocidade: 10)
        {
            TurnosDeVida = turnos;
        }

        /// <summary>
        /// Ataca o alvo e consome um turno de vida da invocação.
        /// </summary>
        public void Atacar(PersonagemBase alvo)
        {
            Console.WriteLine($"{Nome} (Invocação) avança pesadamente contra {alvo.Nome}!");
            alvo.ReceberDano(Forca);

            // Reduz o tempo de vida da invocação a cada ataque
            TurnosDeVida--;
            if (TurnosDeVida == 0)
            {
                Console.WriteLine($"O tempo de {Nome} acabou. Ele se desfez em poeira.");
            }
        }
    }
}
