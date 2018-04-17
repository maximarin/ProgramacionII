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
        public bool EstaCompleto { get; set; }

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

        public void RevisarCantidadJugadores ()
        {
            if (jugadores.Count() == 2)
            {
                this.EstaCompleto = true;
            }
            return;
        }
        private Mazo MezclarCartas ()
        {   
            
            return this.mazo;
        }
        public void RepartirCartas ()
        {
            //SE VERIFICA POR LAS DUDAS QUE LA CANTIDAD DE CARTAS SEA PAR
            if (mazo.Cartas.Count() % 2 == 0 )   // "%" DEVUELVE EL RESTO DE LA DIVISIÓN
            {   
                int contador = 1;
                foreach (var item in MezclarCartas().Cartas)
                {
                    if (contador % 2 != 0)
                    {
                        this.jugadores[1].Cartas.Add( item) ;
                    }
                    else
                    {
                        this.jugadores[2].Cartas.Add(item);
                    }
                }
            }
                     
            return;
        }





    
    }

   
}

