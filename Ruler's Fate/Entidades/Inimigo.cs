using System;
using System.Collections.Generic;
using System.Text;

namespace Ruler_s_Fate.Entidades
{
    public class Inimigo : PersonagemBase
    {
        public Inimigo(string nome, int nivel)
            : base(nome, vida: 50 * nivel, mana: 0, forca: 8 * nivel, inteligencia: 2, velocidade: 5 + nivel)
        {

        }
        public void Atacar(PersonagemBase alvo)
        {
            Console.WriteLine($"O {Nome} avança e ataca {alvo.Nome}!");
            alvo.ReceberDano(Forca);
        }
    }
}
