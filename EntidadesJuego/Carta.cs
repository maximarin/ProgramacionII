using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesJuego
{
    public enum TipoDeCarta { Normal = 0, Amarilla = 1 , Roja = 2, Especial = 3 }
    public class Carta
    {   
        public int IdCarta { get; set; }
        public List<Atributo> Atributos { get; set; }
        public TipoDeCarta TipoCarta { get; set; }
        
        public Carta()
        {
            this.Atributos = new List<Atributo>();
        }

    }
}
