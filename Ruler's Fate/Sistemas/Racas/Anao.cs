namespace Ruler_s_Fate.Sistemas.Racas
{
    /// <summary>
    /// Anão: resistente e forte, porém mais lento.
    /// Alta Vida e Força, Velocidade reduzida.
    /// Taxa de obtenção no Gacha: 30%.
    /// </summary>
    public class Anao : Raca
    {
        public Anao() : base("Anão", modVida: 1.25, modMana: 1.0, modForca: 1.25, modInt: 1.0, modVel: 0.8) { }
    }
}
