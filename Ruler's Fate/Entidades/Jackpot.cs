using System;
using System.Collections.Generic;
using System.Text;

namespace Ruler_s_Fate.Entidades
{
    public class Jackpot : PersonagemBase
    {
        // Variável exclusiva desta classe para rolar os dados
        private Random dadoSorte = new Random();

        // O Jackpot tem status bem equilibrados/médios
        public Jackpot(string nome)
            : base(nome, vida: 120, mana: 50, forca: 15, inteligencia: 15, velocidade: 15)
        {
        }

        public void DadoViciado(PersonagemBase alvo)
        {
            int rolagem = dadoSorte.Next(1, 7); // Rola de 1 a 6
            Console.WriteLine($"\n{Nome} joga o Dado Viciado para o alto... Caiu {rolagem}!");

            if (rolagem == 1)
            {
                Console.WriteLine("Falha Crítica! O ataque falhou miseravelmente e não causou dano.");
            }
            else if (rolagem == 6)
            {
                int danoTriplo = Forca * 3;
                Console.WriteLine("JACKPOT!!! Acerto em cheio com dano triplo!");
                alvo.ReceberDano(danoTriplo);
            }
            else
            {
                Console.WriteLine("Acerto normal.");
                alvo.ReceberDano(Forca);
            }
        }
    }
}