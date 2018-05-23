﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using EntidadesJuego;

namespace Cromy.web.Hubs
{
    public class JuegoHub : Hub
    {
        private static Juego juego = new Juego();
        public static Partida partidaCreada = new Partida();

        public void CrearPartida(string usuario, string partida, string mazo)
        {
            var jugador1 = new Jugador();
            jugador1.Nombre(usuario).Numero(NumJugador.uno).IdConexion(Context.ConnectionId);



            
            partidaCreada.Nombre(partida).Jugador(jugador1);

            // Notifico a los otros usuarios de la nueva partida.
            Clients.Others.agregarPartida(partidaCreada);

            Clients.Caller.esperarJugador();
        }

        public void UnirsePartida(string usuario, string partida)
        {
            var jugador2 = new Jugador();
            jugador2.Nombre(usuario).IdConexion(Context.ConnectionId).Numero(NumJugador.dos);
            var partidaEncontrada = juego.Partidas.Where(x => x.nombre == partida).First();
            
            Clients.All.eliminarPartida(partidaEncontrada);
            partidaEncontrada.RepartirCartas();


            Clients.Client(partidaEncontrada.jugadores[0].idConexion).dibujarTablero(partidaEncontrada.jugadores[0], partidaEncontrada.jugadores[1], partidaEncontrada.mazo);
            Clients.Client(partidaEncontrada.jugadores[1].idConexion).dibujarTablero(partidaEncontrada.jugadores[0], partidaEncontrada.jugadores[1], partidaEncontrada.mazo);

        }

        public void ObtenerPartidas()
        {
            Clients.Caller.agregarPartidas(juego.Partidas);
        }

        public void ObtenerMazos()
        {
            Clients.Caller.agregarMazos();
        }

        public void Cantar(string idAtributo, string idCarta)
        {
            if (jugada.connectionIdGanador == Context.ConnectionId)
            {
                Clients.Caller.ganarMano(resultado, false);
                Clients.Client(jugada.connectionIdPerdedor).perderMano(resultado, false);

            }
            else
            {
                Clients.Client(jugada.connectionIdGanador).ganarMano(resultado, false);
                Clients.Caller.perderMano(resultado, false);

            }
            if (jugada.finalizoJuego)
            {
                Clients.Caller.ganar();
                Clients.Client(jugada.connectionIdPerdedor).perder();
            }
        }
    }
}