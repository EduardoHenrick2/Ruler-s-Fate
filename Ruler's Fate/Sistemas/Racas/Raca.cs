using System;

namespace Ruler_s_Fate.Sistemas.Racas
{
    /// <summary>
    /// Classe base para todas as raças do jogo.
    /// Cada raça define modificadores percentuais sobre os atributos base do personagem.
    /// </summary>
    public abstract class Raca
    {
        public string Nome { get; protected set; }
        public double ModificadorVida { get; protected set; }
        public double ModificadorMana { get; protected set; }
        public double ModificadorForca { get; protected set; }
        public double ModificadorInteligencia { get; protected set; }
        public double ModificadorVelocidade { get; protected set; }

        public Raca(string nome, double modVida, double modMana, double modForca, double modInt, double modVel)
        {
            Nome = nome;
            ModificadorVida = modVida;
            ModificadorMana = modMana;
            ModificadorForca = modForca;
            ModificadorInteligencia = modInt;
            ModificadorVelocidade = modVel;
        }
    }
}
