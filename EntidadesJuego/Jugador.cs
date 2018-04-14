using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesJuego
{
  public  class Jugador
    {
        public string IdConecxion { get; set; }
        public string Nombre { get; set; }
        public List<Carta> Cartas { get; set; }
        //VER AVATAR 

        public Jugador ()
        {
            Cartas = new List<Carta>();
        }

       
        

    }
}
