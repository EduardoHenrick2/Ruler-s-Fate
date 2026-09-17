using Ruler_s_Fate.Entidades.Personagens;
using System;

namespace Ruler_s_Fate.Entidades.Itens
{
    /// <summary>
    /// Artefato raro que aumenta o limite de invocações simultâneas do Invocador em +1.
    /// Não tem efeito em outros tipos de personagem.
    /// </summary>
    public class ArtefatoInvocacao : Item
    {
        public ArtefatoInvocacao()
            : base("Artefato das Almas", "Um cristal sombrio que aumenta o limite de invocações em +1.")
        {
        }

        public override void Usar(PersonagemBase alvo)
        {
            // A palavra 'is' verifica se a PersonagemBase é, na verdade, um Invocador
            if (alvo is Invocador invocador)
            {
                invocador.LimiteInvocacoes++;
                Console.WriteLine($"As trevas envolvem {invocador.Nome}! O limite de invocações aumentou para {invocador.LimiteInvocacoes}.");
            }
            else
            {
                Console.WriteLine($"{alvo.Nome} não possui conhecimento arcano para usar isso. O item brilhou e desapareceu sem efeito.");
            }
        }
    }
}
