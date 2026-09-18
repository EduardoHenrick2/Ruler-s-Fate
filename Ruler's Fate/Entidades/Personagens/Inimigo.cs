using System;

namespace Ruler_s_Fate.Entidades.Personagens
{
    public class Inimigo : PersonagemBase
    {
        // Propriedade que define quanto XP esse monstro vale
        public int XPDrop { get; private set; }

        public Inimigo(string nome, int nivel)
            : base(nome, vida: 50 * nivel, mana: 0, forca: 8 * nivel, inteligencia: 2, velocidade: 5 + nivel)
        {
            // Um monstro nível 1 dá 40 XP. Nível 2 dá 80 XP, etc.
            XPDrop = 40 * nivel;
        }

        public void Atacar(PersonagemBase alvo)
        {
            Console.WriteLine($"O {Nome} avança e ataca {alvo.Nome}!");
            alvo.ReceberDano(Forca);
        }
    }
}
