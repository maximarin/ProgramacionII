using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesJuego
{
    public class Partida
    {
        public List<Jugador> jugadores { get; set; }
        public List<Carta> mazo { get; set; }
        public string Turno { get; set; }

        public Partida ()
        {
            jugadores = new List<Jugador>();
            mazo = new List<Carta>();
        }

        public Partida Jugador (Jugador jugador)
        {
            jugadores.Add(jugador);
            return this;
        }

        public Partida Mazo (List<Carta> cartas)
        {
            mazo = cartas;
            return this;
        }






    
    }

   
}

