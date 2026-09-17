using System;

namespace Ruler_s_Fate.Entidades.Personagens
{
    /// <summary>
    /// Inimigo genérico cujos atributos escalam com o nível.
    /// </summary>
    public class Inimigo : PersonagemBase
    {
        public Inimigo(string nome, int nivel)
            : base(nome, vida: 50 * nivel, mana: 0, forca: 8 * nivel, inteligencia: 2, velocidade: 5 + nivel)
        {
        }

        /// <summary>Ataca o alvo com dano igual à sua Força.</summary>
        public void Atacar(PersonagemBase alvo)
        {
            Console.WriteLine($"O {Nome} avança e ataca {alvo.Nome}!");
            alvo.ReceberDano(Forca);
        }
    }
}
