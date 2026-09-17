using System;

namespace Ruler_s_Fate.Entidades.Personagens
{
    /// <summary>
    /// Classe guerreiro: combatente corpo-a-corpo equilibrado,
    /// com alta vida e força. Especialidade: Golpe Esmagador.
    /// </summary>
    public class Guerreiro : PersonagemBase
    {
        public Guerreiro(string nome)
            : base(nome, vida: 120, mana: 30, forca: 20, inteligencia: 5, velocidade: 10)
        {
        }

        /// <summary>
        /// Habilidade especial do Guerreiro. Consome 10 de Mana e causa
        /// dano igual a Força * 2 no alvo.
        /// </summary>
        public void GolpeEsmagador(PersonagemBase alvo)
        {
            if (Mana >= 10)
            {
                Mana -= 10;
                int dano = Forca * 2;
                Console.WriteLine($"{Nome} ergue a espada e desfere um GOLPE ESMAGADOR em {alvo.Nome}!");
                alvo.ReceberDano(dano);
            }
            else
            {
                Console.WriteLine($"{Nome} tenta o Golpe Esmagador, mas não tem Mana suficiente!");
            }
        }
    }
}
