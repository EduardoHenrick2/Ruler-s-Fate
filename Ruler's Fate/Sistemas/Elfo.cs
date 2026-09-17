using System;
using System.Collections.Generic;
using System.Text;

namespace Ruler_s_Fate.Sistemas
{
    public class  Elfo : Raca
    {
        public Elfo() : base("Elfo", modVida: 0.8, modMana: 1.4, modForca: 1.0, modInt: 1.4, modVel: 1.0)
        {
            // Logica de regeneração de mana aqui

        }
    }
}
