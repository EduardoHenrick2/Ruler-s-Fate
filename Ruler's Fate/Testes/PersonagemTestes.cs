using Ruler_s_Fate.Entidades.Itens;
using Ruler_s_Fate.Entidades.Personagens;
using Ruler_s_Fate.Sistemas.Core;
using Ruler_s_Fate.Sistemas.Racas;
using Xunit;

namespace RulersFate.Testes
{
    public class PersonagemTestes
    {
        // ─── Guerreiro ────────────────────────────────────────────────────────────

        [Fact]
        public void Guerreiro_CriadoComAtributosCorretos()
        {
            var g = new Guerreiro("Arthus");
            Assert.Equal("Arthus", g.Nome);
            Assert.Equal(120, g.VidaMax);
            Assert.Equal(120, g.VidaAtual);
            Assert.Equal(30, g.Mana);
            Assert.Equal(20, g.Forca);
        }

        [Fact]
        public void Guerreiro_GolpeEsmagador_CausaDanoEConsomeMana()
        {
            var guerreiro = new Guerreiro("Arthus");
            var inimigo = new Inimigo("Goblin", 1);

            int vidaAntes = inimigo.VidaAtual;
            int manaAntes = guerreiro.Mana;

            guerreiro.GolpeEsmagador(inimigo);

            Assert.Equal(manaAntes - 10, guerreiro.Mana);
            Assert.Equal(vidaAntes - (guerreiro.Forca * 2), inimigo.VidaAtual);
        }

        [Fact]
        public void Guerreiro_GolpeEsmagador_SemMana_NaoCausaDano()
        {
            var guerreiro = new Guerreiro("Arthus");
            var inimigo = new Inimigo("Goblin", 1);

            // Zera a mana manualmente via reflexão para simular estado sem mana
            // Alternativa: usa 3x GolpeEsmagador para gastar 30 MP (limite base = 30)
            guerreiro.GolpeEsmagador(inimigo); // -10 mp → 20
            guerreiro.GolpeEsmagador(inimigo); // -10 mp → 10
            guerreiro.GolpeEsmagador(inimigo); // -10 mp → 0

            int vidaAntesDaTentativa = inimigo.VidaAtual;
            guerreiro.GolpeEsmagador(inimigo); // sem mana, não deve causar dano

            Assert.Equal(vidaAntesDaTentativa, inimigo.VidaAtual);
        }

        // ─── Combate ─────────────────────────────────────────────────────────────

        [Fact]
        public void PersonagemBase_ReceberDano_ReduzVida()
        {
            var guerreiro = new Guerreiro("Herói");
            guerreiro.ReceberDano(30);
            Assert.Equal(90, guerreiro.VidaAtual);
        }

        [Fact]
        public void PersonagemBase_ReceberDano_NaoVaiAbaixoDeZero()
        {
            var guerreiro = new Guerreiro("Herói");
            guerreiro.ReceberDano(9999);
            Assert.Equal(0, guerreiro.VidaAtual);
            Assert.False(guerreiro.EstaVivo);
        }

        [Fact]
        public void PersonagemBase_RestaurarVida_NaoUltrapassaMax()
        {
            var guerreiro = new Guerreiro("Herói");
            guerreiro.ReceberDano(50);        // VidaAtual = 70
            guerreiro.RestaurarVida(9999);    // Deve ser capped em VidaMax
            Assert.Equal(guerreiro.VidaMax, guerreiro.VidaAtual);
        }

        [Fact]
        public void Inimigo_AtacarReduzVidaDoAlvo()
        {
            var inimigo = new Inimigo("Goblin", 1);
            var guerreiro = new Guerreiro("Herói");

            int vidaAntes = guerreiro.VidaAtual;
            inimigo.Atacar(guerreiro);

            Assert.Equal(vidaAntes - inimigo.Forca, guerreiro.VidaAtual);
        }

        // ─── Raças ───────────────────────────────────────────────────────────────

        [Fact]
        public void AplicarRaca_Humano_AumentaAtributos()
        {
            var guerreiro = new Guerreiro("Herói");
            int vidaAntes = guerreiro.VidaMax;
            guerreiro.AplicarRaca(new Humano());
            Assert.Equal((int)(vidaAntes * 1.1), guerreiro.VidaMax);
        }

        [Fact]
        public void AplicarRaca_Elfo_AumentaManaEInteligencia()
        {
            var mago = new Mago("Gandalf");
            int manaAntes = mago.Mana;
            int intAntes = mago.Inteligencia;
            mago.AplicarRaca(new Elfo());
            Assert.Equal((int)(manaAntes * 1.4), mago.Mana);
            Assert.Equal((int)(intAntes * 1.4), mago.Inteligencia);
        }

        [Fact]
        public void AplicarRaca_Anao_AumentaVidaEForca()
        {
            var guerreiro = new Guerreiro("Gimli");
            int vidaAntes = guerreiro.VidaMax;
            int forcaAntes = guerreiro.Forca;
            guerreiro.AplicarRaca(new Anao());
            Assert.Equal((int)(vidaAntes * 1.25), guerreiro.VidaMax);
            Assert.Equal((int)(forcaAntes * 1.25), guerreiro.Forca);
        }

        // ─── Inventário ──────────────────────────────────────────────────────────

        [Fact]
        public void Inventario_AdicionarItem_AumentaContagem()
        {
            var inventario = new Inventario();
            inventario.AdicionarItem(new PocaoDeCura());
            Assert.Single(inventario.ItensGuardados);
        }

        [Fact]
        public void Inventario_NaoAdicionaAlemDaCapacidade()
        {
            var inventario = new Inventario(); // cap = 5
            for (int i = 0; i < 6; i++)
                inventario.AdicionarItem(new PocaoDeCura());
            Assert.Equal(5, inventario.ItensGuardados.Count);
        }

        [Fact]
        public void Inventario_AumentarCapacidade_FuncionaCorretamente()
        {
            var inventario = new Inventario();
            inventario.AumentarCapacidade(3);
            Assert.Equal(8, inventario.CapacidadeMaxima);
        }

        [Fact]
        public void Inventario_ConsumirItem_RemoveEAplicaEfeito()
        {
            var guerreiro = new Guerreiro("Herói");
            guerreiro.ReceberDano(50); // VidaAtual = 70

            guerreiro.Mochila.AdicionarItem(new PocaoDeCura("Poção Teste", 40));
            guerreiro.Mochila.ConsumirItem(0, guerreiro);

            Assert.Empty(guerreiro.Mochila.ItensGuardados);
            Assert.Equal(110, guerreiro.VidaAtual); // 70 + 40
        }

        // ─── Gacha ───────────────────────────────────────────────────────────────

        [Fact]
        public void GachaSystem_RetornaRacaValida()
        {
            for (int i = 0; i < 20; i++)
            {
                Raca raca = GachaSystem.RolarGacha();
                Assert.NotNull(raca);
                Assert.False(string.IsNullOrEmpty(raca.Nome));
            }
        }

        // ─── Invocador ───────────────────────────────────────────────────────────

        [Fact]
        public void Invocador_InvocarGolem_AdicionaAliado()
        {
            var invocador = new Invocador("Zedd");
            invocador.InvocarGolem();
            Assert.Single(invocador.CriaturasInvocadas);
        }

        [Fact]
        public void Invocador_NaoUltrapassaLimiteDeInvocacoes()
        {
            var invocador = new Invocador("Zedd");
            // Tenta invocar mais do que o limite (3), mas com mana suficiente (100)
            for (int i = 0; i < 5; i++)
                invocador.InvocarGolem();
            Assert.Equal(3, invocador.CriaturasInvocadas.Count);
        }

        [Fact]
        public void ArtefatoInvocacao_AumentaLimiteDoInvocador()
        {
            var invocador = new Invocador("Zedd");
            int limiteAntes = invocador.LimiteInvocacoes;
            new ArtefatoInvocacao().Usar(invocador);
            Assert.Equal(limiteAntes + 1, invocador.LimiteInvocacoes);
        }
    }
}
