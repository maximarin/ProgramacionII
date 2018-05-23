using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesJuego
{
    public class Ranking
    {
        public string NombreJugador1 { get; set; }
        public string NombreJugador2 { get; set; }
        public int VecesQueGanoElJugador1 { get; set; }
        public int VecesQueGanoElJugador2 { get; set; }

        public Ranking()
        {
            this.VecesQueGanoElJugador1 = 0;
            this.VecesQueGanoElJugador2 = 0;
        }
    }
}
