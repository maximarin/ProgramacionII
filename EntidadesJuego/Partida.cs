﻿using System;
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
            mazo = mazoElegido;
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


        private void MezclarCartas()
        {
            var listaRetornar = new List<Carta>();
            Random numeroNuevo = new Random();

            bool corte = true;
            while (corte == true)
            {
                int n = numeroNuevo.Next(1, mazo.Cartas.Count);

                if (listaRetornar == null) //Primer carta
                {
                    listaRetornar.Add(mazo.Cartas[n]);
                    mazo.Cartas.RemoveAt(n);
                }
                else
                {
                    bool ok = false;
                    foreach (var item in listaRetornar) //Me fijo si el random ya había salido
                    {
                        if (item.IdCarta == n)
                        {
                            ok = true;
                        }
                    }
                    if (ok == false) //Si no salío agrego la carta
                    {
                        listaRetornar.Add(mazo.Cartas[n]);
                        mazo.Cartas.RemoveAt(n);
                        if (mazo.Cartas.Count == 0) //Si el mazo está vacío se corta el while
                        {
                            corte = false;
                        }
                    }
                }
            }

            mazo.Cartas = listaRetornar;
        }

        public void RepartirCartas()
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

        public void Deja5Cartas(Jugador jugador)    //Modifico la lista del afectado
        {
            var Lista5cartas = new List<Carta>();
            int c = 0;
            while (c != 5)
            {
                Lista5cartas[c] = jugador.Cartas[c];
                c++;
            }
            jugador.Cartas = Lista5cartas;
        }

        public void ResolverCartasEspeciales(TipoDeCarta carta1, TipoDeCarta carta2, Jugador jugador1, Jugador jugador2)
        { //Doy por hecho que ambos se encuentran en la misma sala
            foreach (var item in jugadores)
            {
                if (item.IdConexion == jugador1.IdConexion) //encuentro la sala
                {
                    //Verde vs normal
                    if ((carta1 == TipoDeCarta.Especial) && (carta2 == TipoDeCarta.Normal))
                    {
                        Deja5Cartas(jugador2);
                    }
                    if ((carta2 == TipoDeCarta.Especial) && (carta1 == TipoDeCarta.Normal))
                    {
                        Deja5Cartas(jugador1);
                    }
                    //Roja vs Amarilla
                    if ((TipoDeCarta.Roja == carta1) && (TipoDeCarta.Amarilla == carta2))
                    {
                        jugador2.Cartas.RemoveAt(0);
                    }
                    if ((TipoDeCarta.Amarilla == carta1) && (TipoDeCarta.Roja == carta2))
                    {
                        jugador1.Cartas.RemoveAt(0);
                    }
                    //Verde vs Roja
                    if ((TipoDeCarta.Especial == carta1) && (TipoDeCarta.Roja == carta2))
                    {
                        Deja5Cartas(jugador2);
                        jugador1.Cartas.RemoveRange(0, 2);
                    }
                    if ((TipoDeCarta.Especial == carta2) && (TipoDeCarta.Roja == carta1))
                    {
                        Deja5Cartas(jugador1);
                        jugador2.Cartas.RemoveRange(0, 2);
                    }
                    //Verde vs amarilla
                    if ((TipoDeCarta.Especial == carta1) && (TipoDeCarta.Amarilla == carta2))
                    {
                        Deja5Cartas(jugador2);
                        jugador1.Cartas.RemoveAt(0);
                    }
                    if ((TipoDeCarta.Especial == carta2) && (TipoDeCarta.Amarilla == carta1))
                    {
                        Deja5Cartas(jugador1);
                        jugador2.Cartas.RemoveAt(0);
                    }

                    //Amarrilla vs Normal
                    if ((TipoDeCarta.Amarilla == carta1) && (TipoDeCarta.Normal == carta2))
                    {
                        jugador2.Cartas.RemoveAt(0);
                    }
                    if ((TipoDeCarta.Amarilla == carta2) && (TipoDeCarta.Normal == carta1))
                    {
                        jugador1.Cartas.RemoveAt(0);
                    }

                    //Roja vs Normal
                    if ((TipoDeCarta.Roja == carta1) && (TipoDeCarta.Normal == carta2))
                    {
                        jugador2.Cartas.RemoveRange(0, 2);
                    }
                    if ((TipoDeCarta.Roja == carta2) && (TipoDeCarta.Normal == carta1))
                    {
                        jugador1.Cartas.RemoveRange(0, 2);
                    }
                }
            }
        }




    }


}

