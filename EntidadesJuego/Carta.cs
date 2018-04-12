using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesJuego
{
    public enum TipoDeCarta { Normal = 1, Roja = 2, Amarilla = 3, Especial = 4 }
    public class Carta
    {   
        public int IdCarta { get; set; }
        public List<Atributo> Atributos { get; set; }
        public TipoDeCarta TipoCarta { get; set; }
        
        
    }
}
