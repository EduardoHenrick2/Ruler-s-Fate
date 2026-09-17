using Ruler_s_Fate.Entidades.Itens;
using Ruler_s_Fate.Entidades.Personagens;
using Ruler_s_Fate.Sistemas.Core;
using Ruler_s_Fate.Sistemas.Racas;
using System;

namespace Ruler_s_Fate
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== BEM-VINDO A RULER'S FATE ===");
            Console.Write("Digite o nome do seu herói: ");
            string nomeHeroi = Console.ReadLine() ?? "Herói";

            // 1. O Sistema de Gacha em ação
            Console.WriteLine("\n[SISTEMA] Rolando a roleta do destino...");
            Raca racaSorteada = GachaSystem.RolarGacha();

            // 2. Instanciando o Herói como Guerreiro
            Guerreiro heroi = new Guerreiro(nomeHeroi);
            heroi.AplicarRaca(racaSorteada);

            // 3. Preparando o Inventário inicial
            heroi.Mochila.AdicionarItem(new PocaoDeCura());
            heroi.Mochila.AdicionarItem(new PocaoDeMana());

            // 4. Instanciando o Inimigo (Nível 1)
            Inimigo inimigo = new Inimigo("Goblin Saqueador", 1);
            Console.WriteLine($"\nUm {inimigo.Nome} selvagem apareceu em seu caminho!");

            // 5. O Loop de Combate Clássico
            while (heroi.EstaVivo && inimigo.EstaVivo)
            {
                Console.WriteLine($"\n=======================================");
                Console.WriteLine($"--- TURNO DE {heroi.Nome.ToUpper()} ---");
                Console.WriteLine($"HP: {heroi.VidaAtual}/{heroi.VidaMax} | MP: {heroi.Mana}");
                Console.WriteLine("1 - Ataque Básico");
                Console.WriteLine("2 - Golpe Esmagador (10 MP)");
                Console.WriteLine("3 - Abrir Inventário");
                Console.Write("Escolha sua ação: ");

                string acao = Console.ReadLine() ?? "";
                Console.WriteLine();

                // Lógica de ação do jogador
                if (acao == "1")
                {
                    heroi.AtacarBasico(inimigo);
                }
                else if (acao == "2")
                {
                    heroi.GolpeEsmagador(inimigo);
                }
                else if (acao == "3")
                {
                    heroi.Mochila.ExibirMochila();
                    Console.Write("\nDigite o número do item que deseja usar (ou qualquer letra para cancelar): ");

                    if (int.TryParse(Console.ReadLine(), out int indiceItem))
                    {
                        heroi.Mochila.ConsumirItem(indiceItem, heroi);
                    }
                    else
                    {
                        Console.WriteLine("Ação cancelada.");
                        continue; // Reinicia o turno sem o inimigo atacar
                    }
                }
                else
                {
                    Console.WriteLine("Ação inválida! Você tropeçou e perdeu o turno.");
                }

                // Turno do Inimigo (só ataca se não tiver morrido no golpe anterior)
                if (inimigo.EstaVivo)
                {
                    Console.WriteLine($"\n--- TURNO DO INIMIGO ---");
                    inimigo.Atacar(heroi);
                }
            }

            // 6. Resolução da Batalha
            Console.WriteLine($"\n=======================================");
            if (heroi.EstaVivo)
            {
                Console.WriteLine($"VITÓRIA! O {inimigo.Nome} foi derrotado.");
                Console.WriteLine("O inimigo deixou cair um artefato misterioso...");
                heroi.Mochila.AdicionarItem(new ArtefatoInvocacao());
            }
            else
            {
                Console.WriteLine($"GAME OVER! {heroi.Nome} caiu em batalha. O destino foi cruel.");
            }

            Console.WriteLine("\nPressione ENTER para encerrar.");
            Console.ReadLine();
        }
    }
}