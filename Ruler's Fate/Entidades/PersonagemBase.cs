using System;
using System.Collections.Generic;
using System.Text;

namespace Ruler_s_Fate.Entidades
{

    public abstract class PersonagemBase
    {
        public string Nome { get; protected set; }
        public int VidaMax { get; protected set; }
        public int VidaAtual { get; protected set; }
        public int Mana { get; protected set; }
        public int Forca { get; protected set; }
        public int Inteligencia { get; protected set; }
        public int Velocidade { get; protected set; }
        public Raca RacaDoPersonagem { get; protected set; }

        public PersonagemBase(string nome, int vida, int mana, int forca, int inteligencia, int velocidade)
        {
            Nome = nome;
            VidaMax = vida;
            VidaAtual = vida;
            Mana = mana;
            Forca = forca;
            Inteligencia = inteligencia;
            Velocidade = velocidade;
        }
        public void ReceberDano(int dano)
        {
            if (VidaAtual < 0 )
            {
                VidaAtual = 0;
            }
            Console.WriteLine($"{Nome} recebeu {dano} de dano! Vida restante: {VidaAtual}/{VidaMax}");
        }

    }
}
