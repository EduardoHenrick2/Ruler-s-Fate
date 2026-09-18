using Ruler_s_Fate.Entidades.Itens;
using Ruler_s_Fate.Entidades.Personagens;
using Ruler_s_Fate.Sistemas.Core;
using Ruler_s_Fate.Sistemas.Racas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ruler_s_Fate
{
    class Program
    {
        // ── Tabela de mobs disponíveis para caça ─────────────────────────────────
        private static readonly (string Nome, int Nivel)[] TabelaMobs =
        {
            ("Goblin Saqueador",   1),
            ("Lobo das Trevas",    1),
            ("Esqueleto Guerreiro",2),
            ("Ogro Selvagem",      2),
            ("Troll das Pedras",   3),
            ("Necromante Menor",   3),
            ("Dragão de Fogo",     5),
        };

        private static readonly Random rng = new Random();

        // ─────────────────────────────────────────────────────────────────────────

        static void Main(string[] args)
        {
            Console.Clear();
            Cabecalho("BEM-VINDO A RULER'S FATE");

            // ── 1. Nome do herói ─────────────────────────────────────────────────
            Console.Write("  Digite o nome do seu herói: ");
            string nomeHeroi = Console.ReadLine() ?? "Herói";

            // ── 2. Escolha de classe ─────────────────────────────────────────────
            PersonagemBase heroi = EscolherClasse(nomeHeroi);

            // ── 3. Gacha de raça ─────────────────────────────────────────────────
            Console.WriteLine("\n  [DESTINO] Rolando a roleta das raças...");
            Raca racaSorteada = GachaSystem.RolarGacha();
            heroi.AplicarRaca(racaSorteada);

            // ── 4. Inventário inicial ─────────────────────────────────────────────
            heroi.Mochila.AdicionarItem(new PocaoDeCura());
            heroi.Mochila.AdicionarItem(new PocaoDeMana());
            heroi.Mochila.AdicionarItem(new EspadaLonga());
            heroi.Mochila.AdicionarItem(new ArmaduraCouro());

            // ── 5. Exibe status inicial ───────────────────────────────────────────
            ExibirStatus(heroi);
            Console.WriteLine("\n  Pressione ENTER para começar sua aventura...");
            Console.ReadLine();

            // ── 6. Campo Aberto — loop principal ─────────────────────────────────
            bool continuar = true;
            while (continuar && heroi.EstaVivo)
            {
                Console.Clear();
                Cabecalho("CAMPO ABERTO");
                MiniStatus(heroi);

                Console.WriteLine("\n  O que deseja fazer?\n");
                Console.WriteLine("  [1] Caçar mobs");
                Console.WriteLine("  [2] Ver status completo");
                Console.WriteLine("  [3] Ver inventário");
                Console.WriteLine("  [4] Encerrar aventura");
                Console.Write("\n  > ");

                string opcao = Console.ReadLine() ?? "";
                Console.WriteLine();

                switch (opcao)
                {
                    case "1": CacarMob(heroi);            break;
                    case "2": ExibirStatus(heroi); Pausar(); break;
                    case "3": heroi.Mochila.ExibirMochila(); UsarItemFora(heroi); break;
                    case "4": continuar = false;           break;
                    default:
                        Console.WriteLine("  Opção inválida.");
                        Pausar();
                        break;
                }
            }

            // ── 7. Fim ────────────────────────────────────────────────────────────
            Console.Clear();
            if (!heroi.EstaVivo)
            {
                Cabecalho("GAME OVER");
                Console.WriteLine($"\n  {heroi.Nome} caiu em batalha. O destino foi cruel...\n");
            }
            else
            {
                Cabecalho("ATÉ A PRÓXIMA, AVENTUREIRO");
                Console.WriteLine($"\n  {heroi.Nome} retorna ao acampamento com glória.\n");
                ExibirStatus(heroi);
            }

            Console.WriteLine("\n  Pressione ENTER para encerrar.");
            Console.ReadLine();
        }

        // ═════════════════════════════════════════════════════════════════════════
        //  ESCOLHA DE CLASSE
        // ═════════════════════════════════════════════════════════════════════════
        static PersonagemBase EscolherClasse(string nome)
        {
            while (true)
            {
                Console.Clear();
                Cabecalho("ESCOLHA SUA CLASSE");
                Console.WriteLine();
                Console.WriteLine("  [1] Guerreiro  — HP: 120 | MP: 30  | FOR: 20 | INT:  5 | VEL: 10");
                Console.WriteLine("                   Especial: Golpe Esmagador (10 MP) — dano = FOR × 2");
                Console.WriteLine();
                Console.WriteLine("  [2] Mago       — HP: 100 | MP: 80  | FOR:  5 | INT: 25 | VEL: 12");
                Console.WriteLine("                   Especial: Bola de Fogo (20 MP) — dano = INT × 2 + Queimando");
                Console.WriteLine();
                Console.WriteLine("  [3] Invocador  — HP:  90 | MP: 100 | FOR:  5 | INT: 20 | VEL: 10");
                Console.WriteLine("                   Especial: Invocar Golem (30 MP)");
                Console.WriteLine();
                Console.WriteLine("  [4] Jackpot    — HP: 120 | MP: 50  | FOR: 15 | INT: 15 | VEL: 15");
                Console.WriteLine("                   Especial: Dado Viciado — crítico, normal ou falha");
                Console.WriteLine();
                Console.Write("  Escolha: ");

                PersonagemBase? heroi = (Console.ReadLine() ?? "") switch
                {
                    "1" => new Guerreiro(nome),
                    "2" => new Mago(nome),
                    "3" => new Invocador(nome),
                    "4" => new Jackpot(nome),
                    _   => null
                };

                if (heroi != null)
                {
                    Console.WriteLine($"\n  Classe [{heroi.GetType().Name}] escolhida para {nome}!");
                    return heroi;
                }

                Console.WriteLine("  Opção inválida. Tente novamente.");
                Pausar();
            }
        }

        // ═════════════════════════════════════════════════════════════════════════
        //  CAÇA A MOBS
        // ═════════════════════════════════════════════════════════════════════════
        static void CacarMob(PersonagemBase heroi)
        {
            Console.Clear();
            Cabecalho("CAÇA — ESCOLHA SEU ALVO");
            Console.WriteLine();

            for (int i = 0; i < TabelaMobs.Length; i++)
            {
                var (nome, nivel) = TabelaMobs[i];
                var preview = new Inimigo(nome, nivel);
                Console.WriteLine($"  [{i + 1}] {nome,-22} | Nv.{nivel} | HP: {preview.VidaMax,3} | FOR: {preview.Forca,2} | XP: {preview.XPDrop}");
            }

            Console.WriteLine($"\n  [A] Inimigo aleatório   [V] Voltar");
            Console.Write("\n  > ");

            string input = (Console.ReadLine() ?? "").ToUpper();

            Inimigo? alvo = null;
            if (input == "V") return;

            if (input == "A")
            {
                var (n, nv) = TabelaMobs[rng.Next(TabelaMobs.Length)];
                alvo = new Inimigo(n, nv);
            }
            else if (int.TryParse(input, out int idx) && idx >= 1 && idx <= TabelaMobs.Length)
            {
                var (n, nv) = TabelaMobs[idx - 1];
                alvo = new Inimigo(n, nv);
            }
            else
            {
                Console.WriteLine("\n  Opção inválida.");
                Pausar();
                return;
            }

            IniciarCombate(heroi, alvo);
        }

        // ═════════════════════════════════════════════════════════════════════════
        //  LOOP DE COMBATE
        // ═════════════════════════════════════════════════════════════════════════
        static void IniciarCombate(PersonagemBase heroi, Inimigo inimigo)
        {
            Console.Clear();
            Cabecalho("COMBATE");
            Console.WriteLine($"\n  Um {inimigo.Nome} (HP:{inimigo.VidaMax} | FOR:{inimigo.Forca} | XP:{inimigo.XPDrop}) apareceu!\n");
            Pausar();

            while (heroi.EstaVivo && inimigo.EstaVivo)
            {
                Console.Clear();
                Cabecalho("COMBATE");

                // ── 1. Processar efeitos de status no início do turno ─────────────
                if (heroi.EfeitosAtivos.Count > 0)
                {
                    Console.WriteLine("\n  --- EFEITOS DO HERÓI ---");
                    heroi.ProcessarEfeitosDeStatus();
                    if (!heroi.EstaVivo) break; // O herói pode morrer por DoT
                }

                // ── 2. HUD ───────────────────────────────────────────────────────
                BarraHUD(heroi, inimigo);

                // ── 3. Menu de ações ─────────────────────────────────────────────
                Console.WriteLine("\n  Suas ações:\n");
                Console.WriteLine("  [1] Ataque Básico");
                ExibirAcaoEspecial(heroi);
                Console.WriteLine("  [3] Inventário");
                Console.Write("\n  > ");

                string acao = Console.ReadLine() ?? "";
                Console.WriteLine();

                bool turnoValido = true;

                if (acao == "1")
                {
                    heroi.AtacarBasico(inimigo);
                }
                else if (acao == "2")
                {
                    UsarHabilidadeEspecial(heroi, inimigo);
                }
                else if (acao == "3")
                {
                    heroi.Mochila.ExibirMochila();
                    Console.Write("\n  Número do item (ou ENTER para cancelar): ");
                    if (int.TryParse(Console.ReadLine(), out int idx))
                        heroi.Mochila.ConsumirItem(idx, heroi);
                    else
                    {
                        Console.WriteLine("  Ação cancelada — inimigo não atacará.");
                        turnoValido = false;
                    }
                }
                else
                {
                    Console.WriteLine("  Ação inválida! Você tropeçou e perdeu o turno.");
                }

                // ── 4. Turno do inimigo ───────────────────────────────────────────
                if (turnoValido && inimigo.EstaVivo)
                {
                    Console.WriteLine($"\n  --- TURNO DO INIMIGO ---");

                    // Efeitos de status do inimigo também processam
                    if (inimigo.EfeitosAtivos.Count > 0)
                        inimigo.ProcessarEfeitosDeStatus();

                    if (inimigo.EstaVivo)
                        inimigo.Atacar(heroi);
                }

                Pausar();
            }

            // ── 5. Resolução ──────────────────────────────────────────────────────
            Console.Clear();
            Cabecalho("FIM DO COMBATE");
            if (heroi.EstaVivo)
            {
                Console.WriteLine($"\n  VITÓRIA! {inimigo.Nome} foi derrotado!");
                heroi.GanharXP(inimigo.XPDrop);
                DropLoot(heroi, inimigo);
            }
            else
            {
                Console.WriteLine($"\n  DERROTA! {heroi.Nome} foi abatido por {inimigo.Nome}...");
            }

            Pausar();
        }

        // ═════════════════════════════════════════════════════════════════════════
        //  HABILIDADE ESPECIAL
        // ═════════════════════════════════════════════════════════════════════════
        static void ExibirAcaoEspecial(PersonagemBase heroi)
        {
            string desc = heroi switch
            {
                Guerreiro  => "[2] Golpe Esmagador (10 MP) — FOR × 2",
                Mago       => "[2] Bola de Fogo (20 MP) — INT × 2 + Queimando",
                Invocador  => "[2] Invocar Golem (30 MP) + aliados atacam",
                Jackpot    => "[2] Dado Viciado — sorte decide tudo",
                _          => "[2] Habilidade Especial"
            };
            Console.WriteLine($"  {desc}");
        }

        static void UsarHabilidadeEspecial(PersonagemBase heroi, Inimigo alvo)
        {
            switch (heroi)
            {
                case Guerreiro g:
                    g.GolpeEsmagador(alvo);
                    break;
                case Mago m:
                    m.BolaDeFogo(alvo);
                    break;
                case Invocador inv:
                    inv.InvocarGolem();
                    // Aliados existentes também atacam o inimigo neste turno
                    foreach (var aliado in inv.CriaturasInvocadas)
                        if (aliado.EstaVivo) aliado.Atacar(alvo);
                    break;
                case Jackpot j:
                    j.DadoViciado(alvo);
                    break;
            }
        }

        // ═════════════════════════════════════════════════════════════════════════
        //  DROP DE LOOT
        // ═════════════════════════════════════════════════════════════════════════
        static void DropLoot(PersonagemBase heroi, Inimigo inimigo)
        {
            Console.WriteLine("\n  O inimigo deixou cair itens...\n");
            heroi.Mochila.AdicionarItem(new PocaoDeCura($"Poção de Cura (drop)", 30 + inimigo.Forca));

            if (rng.Next(2) == 0)   // 50%: poção de mana
                heroi.Mochila.AdicionarItem(new PocaoDeMana($"Poção de Mana (drop)", 20 + inimigo.Forca / 2));

            if (rng.Next(5) == 0)   // 20%: artefato de invocação
                heroi.Mochila.AdicionarItem(new ArtefatoInvocacao());

            if (rng.Next(4) == 0)   // 25%: equipamento
            {
                Equipamento dropEquip = rng.Next(2) == 0
                    ? (Equipamento)new EspadaLonga()
                    : new ArmaduraCouro();
                Console.WriteLine($"  ✨ Item raro encontrado: {dropEquip.Nome}!");
                heroi.Mochila.AdicionarItem(dropEquip);
            }
        }

        // ═════════════════════════════════════════════════════════════════════════
        //  USO DE ITEM FORA DO COMBATE
        // ═════════════════════════════════════════════════════════════════════════
        static void UsarItemFora(PersonagemBase heroi)
        {
            if (heroi.Mochila.ItensGuardados.Count == 0) { Pausar(); return; }
            Console.Write("\n  Número do item para usar (ou ENTER para fechar): ");
            if (int.TryParse(Console.ReadLine(), out int idx))
                heroi.Mochila.ConsumirItem(idx, heroi);
            Pausar();
        }

        // ═════════════════════════════════════════════════════════════════════════
        //  DISPLAY HELPERS
        // ═════════════════════════════════════════════════════════════════════════

        static void Cabecalho(string titulo)
        {
            string linha = new string('═', 47);
            Console.WriteLine($"  ╔{linha}╗");
            Console.WriteLine($"  ║  {titulo.PadRight(45)}║");
            Console.WriteLine($"  ╚{linha}╝");
        }

        static void MiniStatus(PersonagemBase h)
        {
            string raca = h.RacaDoPersonagem?.Nome ?? "Sem raça";
            Console.WriteLine($"\n  ► {h.Nome} [{h.GetType().Name} — {raca}] | Nv.{h.Nivel}");
            Console.WriteLine($"    HP: {Barra(h.VidaAtual, h.VidaMax, 20)} {h.VidaAtual}/{h.VidaMax}");
            Console.WriteLine($"    XP: {Barra(h.XPAtual, h.XPProximoNivel, 20)} {h.XPAtual}/{h.XPProximoNivel}");
            Console.WriteLine($"    MP: {h.Mana}  |  FOR: {h.Forca}  |  INT: {h.Inteligencia}  |  VEL: {h.Velocidade}");

            if (h.EfeitosAtivos.Count > 0)
            {
                string efeitos = string.Join("  ", h.EfeitosAtivos.Select(e => $"{e.Nome}({e.TurnosRestantes}t)"));
                Console.WriteLine($"    Status: {efeitos}");
            }
        }

        static void ExibirStatus(PersonagemBase h)
        {
            string raca = h.RacaDoPersonagem?.Nome ?? "—";
            string linha = new string('─', 43);

            Console.WriteLine($"\n  ┌{linha}┐");
            Console.WriteLine($"  │  STATUS DE {h.Nome.ToUpper().PadRight(31)}│");
            Console.WriteLine($"  ├{linha}┤");
            Console.WriteLine($"  │  Classe  : {h.GetType().Name,-32}│");
            Console.WriteLine($"  │  Raça    : {raca,-32}│");
            Console.WriteLine($"  │  Nível   : {h.Nivel,-32}│");
            Console.WriteLine($"  ├{linha}┤");
            Console.WriteLine($"  │  HP  {Barra(h.VidaAtual, h.VidaMax, 18)} {h.VidaAtual,4}/{h.VidaMax,-4}     │");
            Console.WriteLine($"  │  XP  {Barra(h.XPAtual, h.XPProximoNivel, 18)} {h.XPAtual,4}/{h.XPProximoNivel,-4}     │");
            Console.WriteLine($"  │  MP  {h.Mana,-37}│");
            Console.WriteLine($"  ├{linha}┤");
            Console.WriteLine($"  │  Força        : {h.Forca,-26}│");
            Console.WriteLine($"  │  Inteligência : {h.Inteligencia,-26}│");
            Console.WriteLine($"  │  Velocidade   : {h.Velocidade,-26}│");
            Console.WriteLine($"  ├{linha}┤");

            // ── Equipamentos ──────────────────────────────────────────────────
            Console.WriteLine($"  │  EQUIPAMENTOS{"".PadRight(29)}│");
            string arma     = h.EquipamentosAtivos.TryGetValue(TipoSlot.Arma,      out var a) ? a.Nome : "—";
            string armadura = h.EquipamentosAtivos.TryGetValue(TipoSlot.Armadura,  out var r) ? r.Nome : "—";
            string acess    = h.EquipamentosAtivos.TryGetValue(TipoSlot.Acessorio, out var ac) ? ac.Nome : "—";
            Console.WriteLine($"  │  ⚔  Arma     : {arma,-26}│");
            Console.WriteLine($"  │  🛡 Armadura  : {armadura,-26}│");
            Console.WriteLine($"  │  💍 Acessório : {acess,-26}│");
            Console.WriteLine($"  ├{linha}┤");

            Console.WriteLine($"  │  Inventário   : {h.Mochila.ItensGuardados.Count}/{h.Mochila.CapacidadeMaxima} slots{"".PadRight(19)}│");

            if (h.EfeitosAtivos.Count > 0)
            {
                Console.WriteLine($"  ├{linha}┤");
                Console.WriteLine($"  │  Efeitos Ativos:{"".PadRight(26)}│");
                foreach (var e in h.EfeitosAtivos)
                    Console.WriteLine($"  │    {e.Nome} — {e.TurnosRestantes} turno(s){"".PadRight(Math.Max(0, 22 - e.Nome.Length))}│");
            }

            Console.WriteLine($"  └{linha}┘");
        }

        static void BarraHUD(PersonagemBase heroi, Inimigo inimigo)
        {
            Console.WriteLine($"\n  HERÓI   ► {heroi.Nome} [{heroi.GetType().Name}] | Nv.{heroi.Nivel}");
            Console.WriteLine($"    HP: {Barra(heroi.VidaAtual, heroi.VidaMax, 18)} {heroi.VidaAtual}/{heroi.VidaMax}   MP: {heroi.Mana}");
            Console.WriteLine($"    XP: {Barra(heroi.XPAtual, heroi.XPProximoNivel, 18)} {heroi.XPAtual}/{heroi.XPProximoNivel}");

            if (heroi.EfeitosAtivos.Count > 0)
            {
                string efs = string.Join("  ", heroi.EfeitosAtivos.Select(e => $"{e.Nome}({e.TurnosRestantes}t)"));
                Console.WriteLine($"    ⚠ {efs}");
            }

            Console.WriteLine($"\n  INIMIGO ► {inimigo.Nome}");
            Console.WriteLine($"    HP: {Barra(inimigo.VidaAtual, inimigo.VidaMax, 18)} {inimigo.VidaAtual}/{inimigo.VidaMax}");

            if (inimigo.EfeitosAtivos.Count > 0)
            {
                string efs = string.Join("  ", inimigo.EfeitosAtivos.Select(e => $"{e.Nome}({e.TurnosRestantes}t)"));
                Console.WriteLine($"    ⚠ {efs}");
            }
        }

        /// <summary>Gera uma barra visual de progresso usando caracteres de bloco.</summary>
        static string Barra(int atual, int max, int tamanho)
        {
            if (max <= 0) return new string('░', tamanho);
            int preenchido = Math.Clamp((int)Math.Round((double)atual / max * tamanho), 0, tamanho);
            return "[" + new string('█', preenchido) + new string('░', tamanho - preenchido) + "]";
        }

        static void Pausar()
        {
            Console.Write("\n  [ENTER para continuar]");
            Console.ReadLine();
        }
    }
}