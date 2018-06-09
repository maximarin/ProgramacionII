using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesJuego
{
    public class JugadorHub
    {
        public List<CartaHub> Cartas { get; set; }
        public string Nombre { get; set; }

        public JugadorHub()
        {
            this.Cartas = new List<CartaHub>();
        }
    }
}
