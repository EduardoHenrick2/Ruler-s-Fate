using System;
using System.Collections.Generic;
using System.Text;

namespace Ruler_s_Fate.Entidades
{
    public class Invocador : PersonagemBase
    {
        // A mecânica que você sugeriu: o limite base é 3.
        // O "set" está público para que itens e armas dropadas possam aumentar esse limite depois.
        public int LimiteInvocacoes { get; set; } = 3;

        // A Lista que guarda quem está na arena lutando por você
        public List<Aliado> CriaturasInvocadas { get; private set; } = new List<Aliado>();

        public Invocador(string nome)
            : base(nome, vida: 90, mana: 100, forca: 5, inteligencia: 20, velocidade: 10)
        {
        }

        public void InvocarGolem()
        {
            // Checa se você já não atingiu o limite máximo de invocações
            if (CriaturasInvocadas.Count < LimiteInvocacoes)
            {
                if (Mana >= 30)
                {
                    Mana -= 30;
                    // Cria a criatura usando a classe que fizemos acima
                    Aliado golem = new Aliado("Golem de Pedra", vida: 50, forca: 15, turnos: 3);

                    // Adiciona o Golem na sua lista de aliados ativos
                    CriaturasInvocadas.Add(golem);

                    Console.WriteLine($"{Nome} bate o cajado no chão e invoca um {golem.Nome}! (Invocações ativas: {CriaturasInvocadas.Count}/{LimiteInvocacoes})");
                }
                else
                {
                    Console.WriteLine($"{Nome} não tem Mana suficiente para invocar o Golem!");
                }
            }
            else
            {
                Console.WriteLine($"O limite de {LimiteInvocacoes} invocações simultâneas foi atingido. Use um item para aumentar sua taxa!");
            }
        }
    }
}