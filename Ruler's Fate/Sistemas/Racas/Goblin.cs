namespace Ruler_s_Fate.Sistemas.Racas
{
    /// <summary>
    /// Goblin: extremamente veloz, mas com Força e Inteligência reduzidas.
    /// Taxa de obtenção no Gacha: 20%.
    /// </summary>
    public class Goblin : Raca
    {
        public Goblin() : base("Goblin", modVida: 1.0, modMana: 1.0, modForca: 0.85, modInt: 0.85, modVel: 1.4) { }
    }
}
