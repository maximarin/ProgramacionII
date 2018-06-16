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

            partidaCreada.SetNombre(partida).Jugador(jugador1);
            partidaCreada.SetMazo(juego.BuscarMazo(mazo));
            juego.AgregarPartida(partidaCreada);
            juego.Jugadores.Add(jugador1);
            // Notifico a los otros usuarios de la nueva partida.

            var newMatch = new PartidasHub
            {
                Mazo = partidaCreada.Mazo.Nombre,
                Nombre = partidaCreada.Nombre,
                Usuario = partidaCreada.jugadores[0].nombre
            };

            Clients.Others.agregarPartida(newMatch);

            Clients.Caller.esperarJugador();                      
        }

        public void UnirsePartida(string usuario, string partida)
        {       

            var jugador2 = new Jugador();
            jugador2.Nombre(usuario).IdConexion(Context.ConnectionId).Numero(NumJugador.dos);
            juego.Jugadores.Add(jugador2);

            //AgregarAlSegundoJugador, agrega al jugador a la partida que elige y devuelve la partida que es
            var partidaEncontrada = juego.Partidas.Where(z => z.Nombre == partida).First().Jugador(jugador2);
                            
            partidaEncontrada.RepartirCartas();
            
            //Dibujar
            var x = juego.DibujarTablero(partidaEncontrada);

            Clients.All.eliminarPartida(partidaEncontrada.Nombre);

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
            var jugadorOponente = juego.Jugadores.Where(x => x.idConexion != Context.ConnectionId).FirstOrDefault();

            var partidaEcontrada = juego.Partidas.Where(x => x.jugadores[0].idConexion == jugadorTurno.idConexion || x.jugadores[1].idConexion == jugadorTurno.idConexion).FirstOrDefault();
            
            string idGanador = "";
            string idPerdedor = "";

            var cartaJugadorTurno = partidaEcontrada.jugadores.Where(x => x.idConexion == jugadorTurno.idConexion).First().Cartas[0];
            var cartaJugadorOponente = partidaEcontrada.jugadores.Where(x => x.idConexion == jugadorOponente.idConexion).First().Cartas[0];

            if (jugadorTurno.NumeroJugador == NumJugador.uno)
            {              
               idGanador = partidaEcontrada.AnalizarCartas(jugadorTurno.Cartas.Where(x => x.IdCarta == idCarta).First(), jugadorOponente.Cartas.First(), idAtributo); 
                
            }
            else
            {
                idGanador = partidaEcontrada.AnalizarCartas(jugadorOponente.Cartas.First(), jugadorTurno.Cartas.Where(x => x.IdCarta == idCarta).First(), idAtributo);
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
                if (cartaJugadorTurno.TipoCarta == TipoDeCarta.Amarilla)
                {
                    Clients.Caller.ganarManoPorTarjetaAmarilla();
                    Clients.Client(idPerdedor).perderMano();
                }
                else if (cartaJugadorTurno.TipoCarta == TipoDeCarta.Roja)
                {
                    Clients.Caller.ganarManoPorTarjetaRoja();
                    Clients.Client(idPerdedor).perderManoPorTarjetaRoja();                    
                }
                else
                {
                    Clients.Caller.ganarMano();
                    Clients.Client(idPerdedor).perderMano();
                }

            }
            else
            {
                if (cartaJugadorOponente.IdCarta == "amarilla")
                {
                    Clients.Client(idGanador).ganarManoPorTarjetaAmarilla();
                    Clients.Caller.perderManoPorTarjetaAmarilla();
                }
                else if (cartaJugadorOponente.IdCarta == "roja")
                {
                    Clients.Client(idGanador).ganarManoPorTarjetaRoja();
                    Clients.Caller.perderManoPorTarjetaRoja();                    
                }
                else
                {
                    Clients.Client(idGanador).ganarMano();
                    Clients.Caller.perderMano();
                }
            }
            if (partidaEcontrada.HayCartas(partidaEcontrada.jugadores[0], partidaEcontrada.jugadores[1]))
            {
                Clients.Caller.ganar();
                Clients.Client(idPerdedor).perder();
            }
        }
    }
}