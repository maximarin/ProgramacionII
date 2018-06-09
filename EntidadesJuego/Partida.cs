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
        public string nombre { get; set; }
        public Ranking resultado { get; set; }



        public Partida() //Test
        {
            this.jugadores = new List<Jugador>();
          
            this.resultado = new Ranking();
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

        public Partida Nombre(string nom)
        {
            this.nombre = nom;
            return this;
        }
        private void RevisarCantidadJugadores()
        {
            if (jugadores.Count() == 2)
            {
                this.EstaCompleto = true;
            }
            return;
        }


        private void MezclarCartas() //Test 
        {
            var listaCartasAuxiliar = new List<Carta>();
            List<int> lista = new List<int>();
            Random numeroNuevo = new Random();
            int n = 0;
            int cont = 1;

            while (this.mazo.Cartas.Count > 0)
            {
                bool enc = false;

                if (this.mazo.Cartas.Count == 1)
                {
                    n = numeroNuevo.Next(0, listaCartasAuxiliar.Count);
                    var aux = listaCartasAuxiliar[n];
                    listaCartasAuxiliar[n] = this.mazo.Cartas[0];
                    listaCartasAuxiliar.Add(aux);

                    break;
                }
                while (enc == false)
                {
                    n = numeroNuevo.Next(1, this.mazo.Cartas.Count + 1);

                    if (lista.Count == 0 && n != 1)
                    {
                        lista.Add(n);
                        listaCartasAuxiliar.Add(this.mazo.Cartas.Where(x => x.IdCarta == n).First());
                        this.mazo.Cartas.Remove(this.mazo.Cartas.Where(x => x.IdCarta == n).First());
                        enc = true;

                    }

                    if (lista.Count >= 1)

                        foreach (var item in lista)
                        {
                            if (item == n)
                            {
                                enc = true;

                                break;
                            }

                        }

                    if (enc == false)
                    {
                        lista.Add(n);
                        listaCartasAuxiliar.Add(this.mazo.Cartas.Where(x => x.IdCarta == n).First());
                        this.mazo.Cartas.Remove(this.mazo.Cartas.Where(x => x.IdCarta == n).First());
                        enc = true;
                    }
                }

            }


            mazo.Cartas = listaCartasAuxiliar;

        }



        public void RepartirCartas()
        {
            RevisarCantidadJugadores();
            if (this.mazo.Cartas != null && this.EstaCompleto == true)
            {

                //MezclarCartas(); //Mezclo el mazo asignado


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
        }

        public void Deja5Cartas(Jugador jespecial, Jugador jafectado)    //Modifico la lista del afectado
        {
            int i = 0;
            while (jafectado.Cartas.Count > 5)
            {
                jespecial.Cartas.Add(jafectado.Cartas[i]);
                jafectado.Cartas.Remove(jafectado.Cartas[i]);
                i++;
            }

            return;
        }

        public void BuscoAgregoBorro(Carta cartalost, Jugador jugadorlost, int cant, Carta cartawin, Jugador jugadorwin) //Metodo D10S
        {
            if (jugadorlost.Cartas.Count >= cant)
            {


                for (int i = 0; i < jugadorlost.Cartas.Count; i++)
                {
                    if (cartalost == jugadorlost.Cartas[i])
                    {
                        if (cant == 1)
                        {
                            if (cartalost.TipoCarta != TipoDeCarta.Amarilla && cartalost.TipoCarta != TipoDeCarta.Roja)
                            {
                                jugadorwin.Cartas.Add(jugadorlost.Cartas[i]);
                                jugadorlost.Cartas.Remove(jugadorlost.Cartas[i]);
                            }
                            else
                            {
                                if (i + 1 <= jugadorlost.Cartas.Count - 1)
                                {
                                    jugadorwin.Cartas.Add(jugadorlost.Cartas[i + 1]);
                                    jugadorlost.Cartas.Remove(jugadorlost.Cartas[i + 1]);
                                    jugadorlost.Cartas.Remove(jugadorlost.Cartas[i]);
                                }
                                else
                                {
                                    jugadorwin.Cartas.Add(jugadorlost.Cartas[0]);

                                    jugadorlost.Cartas.Remove(jugadorlost.Cartas[i]);
                                    jugadorlost.Cartas.Remove(jugadorlost.Cartas[0]);

                                }
                            }
                        }
                        if (cant == 2)
                        {
                            bool enc = false;
                            if (cartalost.TipoCarta != TipoDeCarta.Amarilla && cartalost.TipoCarta != TipoDeCarta.Roja)  //SI NO ES AMARILLA O ROJA SE AGREGA AL OTRO MAZO
                            {
                                jugadorwin.Cartas.Add(jugadorlost.Cartas[i]);




                                if (i + 1 <= jugadorlost.Cartas.Count - 1)  //Si supera la cantidad, tiene que volver a la primera
                                {
                                    var siguienteCarta = jugadorlost.Cartas[i + 1];
                                    jugadorwin.Cartas.Add(siguienteCarta);

                                    jugadorlost.Cartas.Remove(jugadorlost.Cartas[i]);
                                    jugadorlost.Cartas.Remove(siguienteCarta);
                                }
                                else
                                {
                                    jugadorwin.Cartas.Add(jugadorlost.Cartas[0]);

                                    jugadorlost.Cartas.Remove(jugadorlost.Cartas[i]);
                                    jugadorlost.Cartas.Remove(jugadorlost.Cartas[0]);
                                }

                            }
                            else   //ES AMARILLA O ROJA. ENTONCES HAY QUE AGREGAR AL OTRO MAZO LAS DOS SIGUIENTES Y ELIMINAR LA ESPECIAL
                            {
                                if (i + 2 <= jugadorlost.Cartas.Count - 1)
                                {
                                    jugadorwin.Cartas.Add(jugadorlost.Cartas[i + 1]);
                                    jugadorwin.Cartas.Add(jugadorlost.Cartas[i + 2]);

                                    jugadorlost.Cartas.Remove(jugadorlost.Cartas[i]);
                                    jugadorlost.Cartas.Remove(jugadorlost.Cartas[i + 1]);
                                    jugadorlost.Cartas.Remove(jugadorlost.Cartas[i + 2]);
                                }

                                else
                                {
                                    if (i + 1 <= jugadorlost.Cartas.Count - 1)
                                    {
                                        jugadorwin.Cartas.Add(jugadorlost.Cartas[i + 1]);
                                        jugadorwin.Cartas.Add(jugadorlost.Cartas[0]);

                                        jugadorlost.Cartas.Remove(jugadorlost.Cartas[i]);
                                        jugadorlost.Cartas.Remove(jugadorlost.Cartas[i + 1]);
                                        jugadorlost.Cartas.Remove(jugadorlost.Cartas[0]);
                                    }
                                    else
                                    {
                                        jugadorwin.Cartas.Add(jugadorlost.Cartas[0]);
                                        jugadorwin.Cartas.Add(jugadorlost.Cartas[1]);

                                        jugadorlost.Cartas.Remove(jugadorlost.Cartas[i]);
                                        jugadorlost.Cartas.Remove(jugadorlost.Cartas[0]);
                                        jugadorlost.Cartas.Remove(jugadorlost.Cartas[1]);
                                    }
                                }



                            }

                        }



                    }
                }
            }



            foreach (var item in jugadorwin.Cartas)//La carta ganadora la borro(las especiales se usan 1 vez nomas)
            {
                if (cartawin == item)
                {
                    jugadorwin.Cartas.Remove(item);
                    break;
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

        public bool HayCartas(Jugador jugador1, Jugador jugador2) //Metodo para verificar cada vez que se cambia de turno
        {
            bool terminar = false;

            if ((jugador1.Cartas.Count == 0) || (jugador2.Cartas.Count == 0))
            {
                terminar = true;
            }
            return terminar;
        }

        public Jugador DetectarJugadorGanador(Jugador jugador1, Jugador jugador2) //Cuando el metodo HayCartas devolvio un True 
        {
            if (jugador1.Cartas.Count != 0)
                return jugador1;
            else
            {
                return jugador2;
            }
        }


        public void ActualizarRanking() //Test  Modo super Dios
        {
            var ganador = new Jugador();

            resultado.NombreJugador1 = jugadores[0].nombre;
            resultado.NombreJugador2 = jugadores[1].nombre;


            if (HayCartas(jugadores[0], jugadores[1]))
            {
                ganador = DetectarJugadorGanador(jugadores[0], jugadores[1]);

                if (ganador == jugadores[0])
                {
                    resultado.VecesQueGanoElJugador1 = resultado.VecesQueGanoElJugador1 + 1;
                }
                else
                {
                    resultado.VecesQueGanoElJugador2 = resultado.VecesQueGanoElJugador2 + 1;
                }
            }            
        }

        public void Revancha()
        {
            foreach (var item in this.jugadores)
            {
                item.Cartas = null; 
                item.Cartas = new List<Carta>(); 
            }
            this.RepartirCartas();
        }
   

    }

}

