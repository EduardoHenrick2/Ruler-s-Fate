using System;
using System.Collections.Generic;

namespace Ruler_s_Fate.Entidades.Personagens
{
    /// <summary>
    /// Invocador: especialista em convocar aliados mágicos para lutar ao seu lado.
    /// O limite de invocações simultâneas pode ser expandido via itens.
    /// </summary>
    public class Invocador : PersonagemBase
    {
        // O limite base é 3. Público para que itens possam aumentar esse valor.
        public int LimiteInvocacoes { get; set; } = 3;

        /// <summary>Lista das criaturas atualmente na arena lutando pelo Invocador.</summary>
        public List<Aliado> CriaturasInvocadas { get; private set; } = new List<Aliado>();

        public Invocador(string nome)
            : base(nome, vida: 90, mana: 100, forca: 5, inteligencia: 20, velocidade: 10)
        {
        }

        /// <summary>
        /// Invoca um Golem de Pedra. Custa 30 de Mana e respeita o limite de invocações.
        /// </summary>
        public void InvocarGolem()
        {
            // Checa se o limite máximo de invocações foi atingido
            if (CriaturasInvocadas.Count < LimiteInvocacoes)
            {
                if (Mana >= 30)
                {
                    Mana -= 30;
                    Aliado golem = new Aliado("Golem de Pedra", vida: 50, forca: 15, turnos: 3);
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
