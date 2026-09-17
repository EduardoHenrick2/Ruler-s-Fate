using Ruler_s_Fate.Entidades.Itens;
using Ruler_s_Fate.Entidades.Personagens;
using System;
using System.Collections.Generic;

namespace Ruler_s_Fate.Sistemas.Core
{
    /// <summary>
    /// Gerencia os itens carregados pelo personagem.
    /// A capacidade padrão é de 5 slots e pode ser expandida via <see cref="UpgradeMochila"/>.
    /// </summary>
    public class Inventario
    {
        /// <summary>Capacidade máxima atual do inventário.</summary>
        public int CapacidadeMaxima { get; private set; } = 5;

        /// <summary>Lista de itens guardados.</summary>
        public List<Item> ItensGuardados { get; private set; } = new List<Item>();

        /// <summary>
        /// Tenta adicionar um item ao inventário. Informa se estiver cheio.
        /// </summary>
        public void AdicionarItem(Item novoItem)
        {
            if (ItensGuardados.Count < CapacidadeMaxima)
            {
                ItensGuardados.Add(novoItem);
                Console.WriteLine($"\n[Loot] Você guardou: {novoItem.Nome}. ({ItensGuardados.Count}/{CapacidadeMaxima} espaços)");
            }
            else
            {
                Console.WriteLine($"\n[Aviso] Sua mochila está cheia! Não foi possível pegar {novoItem.Nome}.");
            }
        }

        /// <summary>Expande a capacidade máxima do inventário.</summary>
        public void AumentarCapacidade(int espacosExtras)
        {
            CapacidadeMaxima += espacosExtras;
            Console.WriteLine($"Sua capacidade de inventário aumentou para {CapacidadeMaxima} espaços!");
        }

        /// <summary>Exibe todos os itens do inventário no console.</summary>
        public void ExibirMochila()
        {
            Console.WriteLine($"\n=== INVENTÁRIO ({ItensGuardados.Count}/{CapacidadeMaxima}) ===");
            if (ItensGuardados.Count == 0)
            {
                Console.WriteLine("Sua mochila está vazia.");
                return;
            }

            for (int i = 0; i < ItensGuardados.Count; i++)
            {
                Console.WriteLine($"[{i}] {ItensGuardados[i].Nome} - {ItensGuardados[i].Descricao}");
            }
        }

        /// <summary>
        /// Usa e remove o item no índice especificado, aplicando seu efeito no <paramref name="alvo"/>.
        /// </summary>
        public void ConsumirItem(int indice, PersonagemBase alvo)
        {
            if (indice >= 0 && indice < ItensGuardados.Count)
            {
                Item itemEscolhido = ItensGuardados[indice];
                itemEscolhido.Usar(alvo);
                ItensGuardados.RemoveAt(indice);
            }
            else
            {
                Console.WriteLine("Opção inválida.");
            }
        }
    }
}
