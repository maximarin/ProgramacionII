using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesJuego
{
    public class DibujarTableroHub
    {
        public JugadorHub Jugador1 { get; set; }
        public JugadorHub Jugador2 { get; set; }
        public MazoHub Mazo { get; set; }

        public DibujarTableroHub()
        {
            this.Jugador1 = new JugadorHub();
            this.Jugador2 = new JugadorHub();
            this.Mazo = new MazoHub();
        }
    }
}
