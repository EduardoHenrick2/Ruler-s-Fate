namespace Ruler_s_Fate.Sistemas.Racas
{
    /// <summary>
    /// Elfo: mago nato com alta Mana e Inteligência, porém menos resistente.
    /// Raça rara do Gacha. Taxa de obtenção: 10%.
    /// </summary>
    public class Elfo : Raca
    {
        public Elfo() : base("Elfo", modVida: 0.8, modMana: 1.4, modForca: 1.0, modInt: 1.4, modVel: 1.0)
        {
            // Lógica de regeneração de mana passiva será adicionada futuramente no sistema de turnos
        }
    }
}
