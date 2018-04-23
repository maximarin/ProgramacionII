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

        public Partida() //Test
        {
            jugadores = new List<Jugador>();

        }

        public Partida Jugador(Jugador jugador) //Test
        {
            jugadores.Add(jugador);
            return this;
        }

        public Partida Mazo(Mazo mazoElegido) //Test
        {
            mazo = mazoElegido;
            return this;
        }

        public void RevisarCantidadJugadores() //Test
        {
            if (jugadores.Count() == 2)
            {
                this.EstaCompleto = true;
            }
            return;
        }


        public void MezclarCartas() //Test  (Esta publico porque sino el test no me lo tomaba)
        {
            var listaCartasAuxiliar = new List<Carta>();
            Random numeroNuevo = new Random();

            while (this.mazo.Cartas.Count > 0)
            {
                int n = numeroNuevo.Next(0, this.mazo.Cartas.Count);
                listaCartasAuxiliar.Add(mazo.Cartas[n]);
                this.mazo.Cartas.RemoveAt(n);
            }

            mazo.Cartas = listaCartasAuxiliar;

        }


        public void RepartirCartas() //Test
        {
            MezclarCartas(); //Mezclo el mazo asignado


            int Cont = 1;
            foreach (var item in this.mazo.Cartas)
            {
                //Asigna una carta a cada uno hasta que se terminen
                if ((Cont % 2) != 0)
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

        public void Deja5Cartas(Jugador jespecial, Jugador jafectado)    //Modifico la lista del afectado
        {
            int i = 0;
           while (jafectado.Cartas.Count > 5 )
            {
                jespecial.Cartas.Add(jafectado.Cartas[i]);
                jafectado.Cartas.Remove(jafectado.Cartas[i]);
                i++;
            }
            
            return;
        }

        public void BuscoAgregoBorro(Carta cartalost, Jugador jugadorlost, int cant, Carta cartawin, Jugador jugadorwin) //Metodo D10S
        {
            for (int i = 0; i < jugadorlost.Cartas.Count; i++)
            {
                if (cartalost == jugadorlost.Cartas[i])
                {
                    if (cant == 1)
                    {
                        jugadorwin.Cartas.Add(jugadorlost.Cartas[i]);
                        jugadorlost.Cartas.Remove(jugadorlost.Cartas[i]);
                    }
                    if (cant == 2)
                    {
                        jugadorwin.Cartas.Add(jugadorlost.Cartas[i]);
                        jugadorwin.Cartas.Add(jugadorlost.Cartas[i + 1]);

                        jugadorlost.Cartas.Remove(jugadorlost.Cartas[i]);
                        jugadorlost.Cartas.Remove(jugadorlost.Cartas[i + 1]);
                    }
                }
            }

            foreach (var item in jugadorwin.Cartas)//La carta ganadora la borro(las especiales se usan 1 vez nomas)
            {
                if (cartawin == item)
                {
                    jugadorwin.Cartas.Remove(item);
                }
            }
        }

        private void ResolverCartasEspeciales(Carta carta1, Carta carta2, Jugador jugador1, Jugador jugador2)
        { //Doy por hecho que ambos se encuentran en la misma sala


            //Verde vs normal
            if ((carta1.TipoCarta == TipoDeCarta.Especial) && (carta2.TipoCarta == TipoDeCarta.Normal))
            {
                Deja5Cartas(jugador1, jugador2);
                return;
            }
            if ((carta2.TipoCarta == TipoDeCarta.Especial) && (carta1.TipoCarta == TipoDeCarta.Normal))
            {
                Deja5Cartas(jugador2, jugador1);
                return;
            }

            //Roja vs Amarilla 
            if ((TipoDeCarta.Roja == carta1.TipoCarta) && (TipoDeCarta.Amarilla == carta2.TipoCarta))
            {
                BuscoAgregoBorro(carta2, jugador2, 1, carta1, jugador1);
                return;
            }
            if ((TipoDeCarta.Amarilla == carta1.TipoCarta) && (TipoDeCarta.Roja == carta2.TipoCarta))
            {
                BuscoAgregoBorro(carta1, jugador1, 1, carta2, jugador2);
                return;
            }

            //Verde vs Roja
            if ((TipoDeCarta.Especial == carta1.TipoCarta) && (TipoDeCarta.Roja == carta2.TipoCarta))
            {
                Deja5Cartas(jugador1, jugador2);
                BuscoAgregoBorro(carta1, jugador1, 2, carta2, jugador2);
                return;
            }
            if ((TipoDeCarta.Especial == carta2.TipoCarta) && (TipoDeCarta.Roja == carta1.TipoCarta))
            {
                Deja5Cartas(jugador2, jugador1);
                BuscoAgregoBorro(carta2, jugador2, 2, carta1, jugador1);
                return;
            }

            //Verde vs amarilla
            if ((TipoDeCarta.Especial == carta1.TipoCarta) && (TipoDeCarta.Amarilla == carta2.TipoCarta))
            {
                Deja5Cartas(jugador1, jugador2);
                BuscoAgregoBorro(carta1, jugador1, 1, carta2, jugador2);
                return;
            }
            if ((TipoDeCarta.Especial == carta2.TipoCarta) && (TipoDeCarta.Amarilla == carta1.TipoCarta))
            {
                Deja5Cartas(jugador2, jugador1);
                BuscoAgregoBorro(carta2, jugador2, 1, carta1, jugador1);
                return;
            }

            //Amarrilla vs Normal
            if ((TipoDeCarta.Amarilla == carta1.TipoCarta) && (TipoDeCarta.Normal == carta2.TipoCarta))
            {
                BuscoAgregoBorro(carta2, jugador2, 1, carta1, jugador1);
                return;
            }
            if ((TipoDeCarta.Amarilla == carta2.TipoCarta) && (TipoDeCarta.Normal == carta1.TipoCarta))
            {
                BuscoAgregoBorro(carta1, jugador1, 1, carta2, jugador2);
                return;
            }

            //Roja vs Normal
            if ((TipoDeCarta.Roja == carta1.TipoCarta) && (TipoDeCarta.Normal == carta2.TipoCarta))
            {
                BuscoAgregoBorro(carta2, jugador2, 2, carta1, jugador1);
                return;
            }
            if ((TipoDeCarta.Roja == carta2.TipoCarta) && (TipoDeCarta.Normal == carta1.TipoCarta))
            {
                BuscoAgregoBorro(carta1, jugador1, 2, carta2, jugador2);
                return;
            }
        }

        public string ResolverCartasNormales(string atributo, int iDCartaJugador1, int iDCartaJugador2) //Test
        {
            string posible;

            var cartaJugador1 = jugadores[0].Cartas.Where(x => x.IdCarta == iDCartaJugador1).First().Atributos.Where(x => x.Nombre == atributo).FirstOrDefault();
            var cartaJugador2 = jugadores[1].Cartas.Where(x => x.IdCarta == iDCartaJugador2).First().Atributos.Where(x => x.Nombre == atributo).FirstOrDefault();

            //Se verifica que ambas cartas tengan la opcion elegida 
            if (cartaJugador1 == null || cartaJugador2 == null)
            {
                return posible = "ELEGIR OTRO ATRIBUTO"; //No es posible, debe elegir otro atributo.
            }
            else
            {


                if (cartaJugador1.Valor > cartaJugador2.Valor) //GANA JUGADOR 1
                {
                    int indice = jugadores[1].Cartas.IndexOf(jugadores[1].Cartas.Where(x => x.IdCarta == iDCartaJugador2).First());
                    jugadores[0].Cartas.Add(jugadores[1].Cartas.First(x => x.IdCarta == iDCartaJugador2));
                    jugadores[1].Cartas.RemoveAt(indice);  //PREGUNTAR SI GANA ESA CARTA O UNA CUALQUIERA DEL MAZO DEL JUGADOR 2
                    return posible = "GANADOR JUGADOR 1";
                }
                else
                {
                    if (cartaJugador2.Valor > cartaJugador1.Valor) //GANA JUGADOR 2
                    {
                        int indice = jugadores[0].Cartas.IndexOf(jugadores[0].Cartas.Where(x => x.IdCarta == iDCartaJugador1).First());
                        jugadores[1].Cartas.Add(jugadores[0].Cartas.First(x => x.IdCarta == iDCartaJugador1));
                        jugadores[0].Cartas.RemoveAt(indice);
                        return posible = "GANADOR JUGADOR 2";
                    }
                    else
                    {
                        return posible = "EMPATE";
                    }
                }


            }
        }


    }


}

