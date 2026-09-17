using System;
using System.Collections.Generic;
using System.Text;

namespace Ruler_s_Fate.Entidades
{
    // O Aliado também herda de PersonagemBase, pois tem vida e pode tomar dano
    public class Aliado : PersonagemBase
    {
        public int TurnosDeVida { get; private set; }

        // O construtor recebe quantos turnos a criatura dura antes de sumir
        public Aliado(string nome, int vida, int forca, int turnos)
            : base(nome, vida, mana: 0, forca, inteligencia: 0, velocidade: 10)
        {
            TurnosDeVida = turnos;
        }

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
