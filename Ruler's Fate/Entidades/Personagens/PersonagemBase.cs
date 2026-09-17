using Ruler_s_Fate.Entidades.Itens;
using Ruler_s_Fate.Sistemas.Core;
using Ruler_s_Fate.Sistemas.Racas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ruler_s_Fate.Entidades.Personagens
{
    public abstract class PersonagemBase
    {
        public string Nome { get; protected set; }
        public int VidaMax { get; protected set; }
        public int VidaAtual { get; protected set; }
        public bool EstaVivo => VidaAtual > 0;
        public int Mana { get; protected set; }
        public int Forca { get; protected set; }
        public int Inteligencia { get; protected set; }
        public int Velocidade { get; protected set; }
        public Raca? RacaDoPersonagem { get; protected set; }
        public Inventario Mochila { get; protected set; } = new Inventario();

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
            VidaAtual -= dano;

            if (VidaAtual < 0)
            {
                VidaAtual = 0;
            }
            Console.WriteLine($"{Nome} recebeu {dano} de dano! Vida restante: {VidaAtual}/{VidaMax}");
        }

        public void RestaurarVida(int quantidade)
        {
            VidaAtual += quantidade;
            // Garante que a cura não ultrapasse o limite máximo de vida
            if (VidaAtual > VidaMax)
            {
                VidaAtual = VidaMax;
            }
            Console.WriteLine($"{Nome} recuperou {quantidade} de Vida! ({VidaAtual}/{VidaMax})");
        }

        public void RestaurarMana(int quantidade)
        {
            Mana += quantidade;
            Console.WriteLine($"{Nome} recuperou {quantidade} de Mana! (Mana total: {Mana})");
        }

        public virtual void AtacarBasico(PersonagemBase alvo)
        {
            Console.WriteLine($"{Nome} realiza um ataque básico contra {alvo.Nome}!");
            alvo.ReceberDano(Forca);
        }

        public void AplicarRaca(Raca racaSorteada)
        {
            RacaDoPersonagem = racaSorteada;

            VidaMax = (int)(VidaMax * racaSorteada.ModificadorVida);
            VidaAtual = VidaMax;
            Mana = (int)(Mana * racaSorteada.ModificadorMana);
            Forca = (int)(Forca * racaSorteada.ModificadorForca);
            Inteligencia = (int)(Inteligencia * racaSorteada.ModificadorInteligencia);
            Velocidade = (int)(Velocidade * racaSorteada.ModificadorVelocidade);

            Console.WriteLine($"{Nome} tornou-se um {racaSorteada.Nome}! Seus atributos foram ajustados.");
        }
    }
}
