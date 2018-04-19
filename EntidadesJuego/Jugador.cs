using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesJuego
{
    public enum NumJugador { uno, dos }
    public class Jugador
    {
        public string idConexion { get; set; }
        public string nombre { get; set; }
        public List<Carta> Cartas { get; set; }
        public NumJugador NumeroJugador { get; set; }
      
        //VER AVATAR 

        public Jugador()
        {
            Cartas = new List<Carta>();
        }

        public Jugador IdConexion(string id)
        {
            this.idConexion = id;
            return this;
        }

        public Jugador Nombre (string nombre)
        {
            this.nombre = nombre;
            return this;
        }
        
        public Jugador Numero (NumJugador num)
        {
            this.NumeroJugador = num;
            return this;
        }
        
    }
}
