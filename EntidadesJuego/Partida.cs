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

        public void RevisarCantidadJugadores()
        {
            if (jugadores.Count() == 2)
            {
                this.EstaCompleto = true;
            }
            return;
        }

        //public void RepartirCartas ()
        //{
        //    //SE VERIFICA POR LAS DUDAS QUE LA CANTIDAD DE CARTAS SEA PAR
        //    if (mazo.Cartas.Count() % 2 == 0 )   // "%" DEVUELVE EL RESTO DE LA DIVISIÓN
        //    {   
        //        int contador = 1;
        //        foreach (var item in MezclarCartas().Cartas)
        //        {
        //            if (contador % 2 != 0)
        //            {
        //                this.jugadores[1].Cartas.Add( item) ;
        //            }
        //            else
        //            {
        //                this.jugadores[2].Cartas.Add(item);
        //            }
        //        }
        //    }

        //    return;
        //}


        private void MezclarCartas(Mazo pmazzo)
        {
            List<int> IdCartasAleatorias = new List<int>();
            Random numeroNuevo = new Random();

            bool corte = true;
            while (corte == true)
            {
                int n = numeroNuevo.Next(1, pmazzo.Cartas.Count);

                if (IdCartasAleatorias == null) //Primer carta
                {
                    IdCartasAleatorias.Add(n);
                }
                else
                {
                    bool ok = false;
                    foreach (var item in IdCartasAleatorias) //Me fijo si el random del numero ya salío
                    {
                        if (item == n)
                        {
                            ok = true;
                        }
                    }
                    if (ok) //Si no salío agredo el id al final
                    {
                        IdCartasAleatorias.Add(n);
                        if (IdCartasAleatorias.Count == pmazzo.Cartas.Count) //Si la lista de id tiene el mismo total que de cartas corto el while
                        {
                            corte = false;
                        }
                    }
                }
            }
            var listaRetornar = new List<Carta>();
            foreach (var item in IdCartasAleatorias) //Reacomodo la lista de cartas
            {
                listaRetornar.Add(pmazzo.Cartas.First(x => x.IdCarta == item));
            }
            pmazzo.Cartas = listaRetornar;
        }

        public void RepartirCartas()
        {
            MezclarCartas(this.mazo); //Mezclo el mazo asignado

            //Ver porque podriamos poner 2 varibles/enumerador de jugadores (y no una lista) y asignarle la mitad de cartas a cada uno

            int Cont = 1;
            foreach (var item in this.mazo.Cartas)
            {
                //Asigna una carta a cada uno hasta que se terminen
                if ((Cont % 2) == 0)
                {
                    var participante1 = jugadores.First(x => x.NumeroJugador == NumJugador.uno);
                    participante1.Cartas.Add(item);
                }
                else
                {
                    var participante2 = jugadores.First(x => x.NumeroJugador == NumJugador.dos);
                    participante2.Cartas.Add(item);
                }
                Cont = Cont + 1;
            }
        }




    }


}

