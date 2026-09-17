using System;
using System.Collections.Generic;
using System.Text;

namespace Ruler_s_Fate.Entidades
{
    public class Mago : PersonagemBase
    {
        // O Mago tem pouca vida e força, mas muita Mana e Inteligência
        public Mago(string nome)
            : base(nome, vida: 100, mana: 80, forca: 5, inteligencia: 25, velocidade: 12)
        {
        }

        public void BolaDeFogo(PersonagemBase alvo)
        {
            if (Mana >= 20)
            {
                Mana -= 20;
                int dano = Inteligencia * 2; // Dano baseado na inteligência

                Console.WriteLine($"{Nome} lança uma BOLA DE FOGO chamejante em {alvo.Nome}!");
                alvo.ReceberDano(dano);

                // Nota: O efeito "Queimando" será adicionado futuramente no sistema de turnos
            }
            else
            {
                Console.WriteLine($"{Nome} tenta conjurar Bola de Fogo, mas está sem Mana!");
            }
        }
    }
}
