using System;
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


        public void CrearPartida(string usuario, string partida, string mazo)
        {
            var partidaCreada = new Partida();
            var jugador1 = new Jugador();
            jugador1.Nombre(usuario).Numero(NumJugador.uno).IdConexion(Context.ConnectionId);

            partidaCreada.Nombre(partida).Jugador(jugador1);
            partidaCreada.Mazo(juego.BuscarMazo(mazo));
            juego.AgregarPartida(partidaCreada);
            juego.Jugadores.Add(jugador1);
            // Notifico a los otros usuarios de la nueva partida.
            Clients.Others.agregarPartida(partidaCreada);

            Clients.Caller.esperarJugador();                      
        }

        public void UnirsePartida(string usuario, string partida)
        {            
            var jugador2 = new Jugador();
            jugador2.Nombre(usuario).IdConexion(Context.ConnectionId).Numero(NumJugador.dos);
            juego.Jugadores.Add(jugador2);

            //AgregarAlSegundoJugador, agrega al jugador a la partida que elige y devuelve la partida que es
            var partidaEncontrada = juego.AgregarSegundoJugador(jugador2 , partida);

            partidaEncontrada.RepartirCartas();
            
            //Dibujar
            var x = juego.DibujarTablero(partidaEncontrada);

            Clients.All.eliminarPartida(partidaEncontrada.nombre);

            Clients.Client(partidaEncontrada.jugadores[0].idConexion).dibujarTablero(x.Jugador1,x.Jugador2,x.Mazo);
            Clients.Client(partidaEncontrada.jugadores[1].idConexion).dibujarTablero(x.Jugador1, x.Jugador2, x.Mazo);

               
        }

        public void ObtenerPartidas()
        {

            Clients.Caller.agregarPartidas(juego.RetornarPartidas());        

        }

        public void ObtenerMazos()
        {
            Clients.Caller.agregarMazos(juego.NombreDeLosMazos());

        }

        public void Cantar(string idAtributo, string idCarta)
        {
            var jugadorTurno = juego.Jugadores.Where(x => x.idConexion == Context.ConnectionId).FirstOrDefault();
            var partidaEcontrada = juego.Partidas.Where(x => x.jugadores[0] == jugadorTurno || x.jugadores[1] == jugadorTurno).FirstOrDefault();
            var jugadorOponente = new Jugador();
            string idGanador = "";
            string idPerdedor = "";
            
            if (jugadorTurno.NumeroJugador == NumJugador.uno)
            {
                jugadorOponente = partidaEcontrada.jugadores[1];
               

               idGanador = partidaEcontrada.AnalizarCartas(jugadorTurno.Cartas.Where(x => x.IdCarta == int.Parse(idCarta)).First(), jugadorOponente.Cartas.First(), idAtributo);
   
            }
            else
            {
                jugadorOponente = partidaEcontrada.jugadores[0];
                idGanador = partidaEcontrada.AnalizarCartas(jugadorOponente.Cartas.First(), jugadorTurno.Cartas.Where(x => x.IdCarta == int.Parse(idCarta)).First(), idAtributo);
            }
                
            if (idGanador == jugadorTurno.idConexion)
            {
                idPerdedor = jugadorOponente.idConexion;
            }
            else
            {
                idPerdedor = jugadorTurno.idConexion;
            }


            if (idGanador == Context.ConnectionId)
            {
               // Clients.Caller.ganarMano(resultado, false);
              //  Clients.Client(idPerdedor).perderMano(resultado, false);

            }
            else
            {
               // Clients.Client(idGanador).ganarMano(resultado, false);
                //Clients.Caller.perderMano(resultado, false);

            }
            if (partidaEcontrada.HayCartas(partidaEcontrada.jugadores[0], partidaEcontrada.jugadores[1]))
            {
                Clients.Caller.ganar();
                Clients.Client(idPerdedor).perder();
            }
        }
    }
}