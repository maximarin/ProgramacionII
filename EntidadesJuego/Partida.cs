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
        public Mazo mazo { get; set; }
        public string Turno { get; set; }

        public Partida()
        {
            jugadores = new List<Jugador>();
            
        }

        public Partida Jugador(Jugador jugador)
        {
            jugadores.Add(jugador);
            return this;
        }

        public Partida Mazo(Mazo mazoElegido)
        {
            mazo = mazoElegido ;
            return this;
        }

        
        public void MezclarCartas ()
        {
            return;
        }
        public void RepartirCartas ()
        {
            
            return;
        }





    
    }

   
}

