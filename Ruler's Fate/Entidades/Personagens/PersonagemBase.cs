using Ruler_s_Fate.Entidades.Itens;
using Ruler_s_Fate.Sistemas.Core;
using Ruler_s_Fate.Sistemas.Racas;
using System;
using System.Collections.Generic;

namespace Ruler_s_Fate.Entidades.Personagens
{
    public abstract class PersonagemBase
    {
        // ── Atributos ──────────────────────────────────────────────────────────
        public string Nome { get; protected set; }
        public int VidaMax { get; protected set; }
        public int VidaAtual { get; protected set; }
        public bool EstaVivo => VidaAtual > 0;
        public int Mana { get; protected set; }
        public int Forca { get; protected set; }
        public int Inteligencia { get; protected set; }
        public int Velocidade { get; protected set; }
        public Raca? RacaDoPersonagem { get; protected set; }

        // ── Progressão ────────────────────────────────────────────────────────
        public int Nivel { get; protected set; } = 1;
        public int XPAtual { get; protected set; } = 0;
        public int XPProximoNivel { get; protected set; } = 100;

        // ── Sistemas ──────────────────────────────────────────────────────────
        public Inventario Mochila { get; protected set; } = new Inventario();

        /// <summary>Lista de efeitos de status ativos (Queimando, Envenenado, etc.).</summary>
        public List<EfeitoStatus> EfeitosAtivos { get; protected set; } = new List<EfeitoStatus>();

        /// <summary>
        /// Dicionário que registra o que está atualmente equipado em cada slot do corpo.
        /// Garante que um mesmo slot (ex: Arma) não receba dois equipamentos simultâneos.
        /// </summary>
        public Dictionary<TipoSlot, Equipamento> EquipamentosAtivos { get; protected set; }
            = new Dictionary<TipoSlot, Equipamento>();

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

        // ── Combate ───────────────────────────────────────────────────────────
        public void ReceberDano(int dano)
        {
            VidaAtual -= dano;
            if (VidaAtual < 0) VidaAtual = 0;
            Console.WriteLine($"{Nome} recebeu {dano} de dano! Vida restante: {VidaAtual}/{VidaMax}");
        }

        public void RestaurarVida(int quantidade)
        {
            VidaAtual += quantidade;
            // Garante que a cura não ultrapasse o limite máximo de vida
            if (VidaAtual > VidaMax) VidaAtual = VidaMax;
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

        /// <summary>
        /// Permite que efeitos de status (ex: Fortalecido) ajustem a Força
        /// temporariamente. Protege contra valores negativos.
        /// </summary>
        public void ModificarForca(int delta)
        {
            Forca += delta;
            if (Forca < 0) Forca = 0;
        }

        // ── Raça ──────────────────────────────────────────────────────────────
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

        // ── XP / Progressão ───────────────────────────────────────────────────
        public void GanharXP(int quantidade)
        {
            XPAtual += quantidade;
            Console.WriteLine($"\n[Experiência] {Nome} ganhou {quantidade} de XP! ({XPAtual}/{XPProximoNivel})");

            // 'while' porque o personagem pode ganhar muito XP e subir vários níveis de uma vez
            while (XPAtual >= XPProximoNivel)
            {
                SubirDeNivel();
            }
        }

        protected virtual void SubirDeNivel()
        {
            XPAtual -= XPProximoNivel;
            Nivel++;
            XPProximoNivel = (int)(XPProximoNivel * 1.5); // Próximo nível exige 50% a mais

            // Bônus de level up
            VidaMax += 20;
            VidaAtual = VidaMax; // Regra clássica de RPG: upar restaura a vida!
            Mana += 10;
            Forca += 5;
            Inteligencia += 5;
            Velocidade += 2;

            Console.WriteLine($"\n🎉 LEVEL UP! {Nome} alcançou o Nível {Nivel}!");
            Console.WriteLine($"  Vida e Mana aumentaram e HP foi restaurado completamente!");
        }

        // ── Efeitos de Status ─────────────────────────────────────────────────

        /// <summary>
        /// Adiciona um efeito de status ao personagem e dispara <see cref="EfeitoStatus.AoAplicar"/>.
        /// </summary>
        public void AdicionarStatus(EfeitoStatus novoStatus)
        {
            EfeitosAtivos.Add(novoStatus);
            Console.WriteLine($"⚠️ {Nome} foi afetado por {novoStatus.Nome} por {novoStatus.TurnosRestantes} turnos!");
            novoStatus.AoAplicar(this);
        }

        /// <summary>
        /// Deve ser chamado no início de cada turno do personagem.
        /// Processa todos os efeitos ativos, decrementa seus turnos e remove os expirados.
        /// </summary>
        public void ProcessarEfeitosDeStatus()
        {
            // Lemos a lista de trás para frente — truque clássico para evitar bugs
            // ao remover itens enquanto iteramos a coleção.
            for (int i = EfeitosAtivos.Count - 1; i >= 0; i--)
            {
                EfeitosAtivos[i].ProcessarTurno(this); // O efeito age (ex: causa dano)
                EfeitosAtivos[i].TurnosRestantes--;    // O tempo do efeito diminui

                // Se o tempo acabou, remove e notifica
                if (EfeitosAtivos[i].TurnosRestantes <= 0)
                {
                    Console.WriteLine($"✨ O efeito {EfeitosAtivos[i].Nome} desapareceu de {Nome}.");
                    EfeitosAtivos[i].AoRemover(this); // Reverte buffs (ex: Fortalecido)
                    EfeitosAtivos.RemoveAt(i);
                }
            }
        }
        /// <summary>
        /// Registra um equipamento no slot correto do personagem.
        /// Se já houver algo equipado no mesmo slot, o item anterior é desequipado
        /// automaticamente antes de o novo ser registrado.
        /// </summary>
        public void VestirEquipamento(Equipamento novoEquipamento)
        {
            // Se já existe algo equipado neste slot, tira primeiro
            if (EquipamentosAtivos.ContainsKey(novoEquipamento.Slot))
            {
                EquipamentosAtivos[novoEquipamento.Slot].Usar(this); // Chama o desequipar
            }

            // Registra o novo equipamento no slot
            EquipamentosAtivos[novoEquipamento.Slot] = novoEquipamento;
        }

        /// <summary>
        /// Aumenta (ou reduz, se delta for negativo) a Vida Máxima do personagem.
        /// A Vida Atual é ajustada para não ultrapassar o novo máximo.
        /// Usado por armaduras para conceder bônus de HP temporários.
        /// </summary>
        public void AumentarVidaMax(int delta)
        {
            VidaMax += delta;
            if (VidaMax < 1) VidaMax = 1;
            if (VidaAtual > VidaMax) VidaAtual = VidaMax;
        }
    }
}
