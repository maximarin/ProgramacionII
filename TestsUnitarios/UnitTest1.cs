using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntidadesJuego;
using System.Collections.Generic;

namespace TestsUnitarios
{
    [TestClass]
    public class PartidasTest
    {
    
        [TestMethod]
        public void DeberiaGanarJugadroDosPorAtributoConValorMasAlto()
        {
            var PartidaPrueba = new Partida();
            var Jugador1 = new Jugador().IdConexion("Id1").Nombre("Maxi").Numero(NumJugador.uno);
            var Jugador2 = new Jugador().IdConexion("Id2").Nombre("Lautaro").Numero(NumJugador.dos);
            PartidaPrueba.jugadores.Add(Jugador1);
            PartidaPrueba.jugadores.Add(Jugador2);

            var Atributo1 = new Atributo() { Nombre = "Fuerza", Valor = 20 };
            var Atributo2 = new Atributo() { Nombre = "Velocidad", Valor = 30 };
            var Atributo3 = new Atributo() { Nombre = "Fuerza", Valor = 24 };
            var Atributo4 = new Atributo() { Nombre = "Resistencia", Valor = 40 };

            List<Atributo> Lista1 = new List<Atributo>();
            Lista1.Add(Atributo1); Lista1.Add(Atributo3);
            List<Atributo> Lista2 = new List<Atributo>();
            Lista2.Add(Atributo3); Lista2.Add(Atributo4);

            var Carta1 = new Carta() { IdCarta = 1, Atributos = Lista1 };
            var Carta2 = new Carta() { IdCarta = 2, Atributos = Lista2 };
            var Carta3 = new Carta() { IdCarta = 3, Atributos = Lista1 };
            var Carta4 = new Carta() { IdCarta = 4, Atributos = Lista2 };

            Jugador1.Cartas.Add(Carta1); Jugador1.Cartas.Add(Carta2);
            Jugador2.Cartas.Add(Carta3); Jugador2.Cartas.Add(Carta4);

            Assert.AreEqual(PartidaPrueba.ResolverCartasNormales("Fuerza", 1, 4), "GANADOR JUGADOR 2");
            Assert.AreEqual(PartidaPrueba.jugadores[0].Cartas.Count , 1);
            Assert.AreEqual(PartidaPrueba.jugadores[1].Cartas.Count, 3);

        }

        [TestMethod]
        public void DeberiaHaberEmpateEntroDosCartasNormalesConElMismoValorEnElAtributo()
        {
            var PartidaPrueba = new Partida();
            var Jugador1 = new Jugador().IdConexion("Id1").Nombre("Maxi").Numero(NumJugador.uno);
            var Jugador2 = new Jugador().IdConexion("Id2").Nombre("Lautaro").Numero(NumJugador.dos);
            PartidaPrueba.jugadores.Add(Jugador1);
            PartidaPrueba.jugadores.Add(Jugador2);

            var Atributo1 = new Atributo() { Nombre = "Fuerza", Valor = 20 };
            var Atributo2 = new Atributo() { Nombre = "Velocidad", Valor = 30 };
            var Atributo3 = new Atributo() { Nombre = "Fuerza", Valor = 20};
            var Atributo4 = new Atributo() { Nombre = "Resistencia", Valor = 40 };

            List<Atributo> Lista1 = new List<Atributo>();
            Lista1.Add(Atributo1); Lista1.Add(Atributo3);
            List<Atributo> Lista2 = new List<Atributo>();
            Lista2.Add(Atributo3); Lista2.Add(Atributo4);

            var Carta1 = new Carta() { IdCarta = 1, Atributos = Lista1 };
            var Carta2 = new Carta() { IdCarta = 2, Atributos = Lista2 };
            var Carta3 = new Carta() { IdCarta = 3, Atributos = Lista1 };
            var Carta4 = new Carta() { IdCarta = 4, Atributos = Lista2 };

            Jugador1.Cartas.Add(Carta1); Jugador1.Cartas.Add(Carta2);
            Jugador2.Cartas.Add(Carta3); Jugador2.Cartas.Add(Carta4);
            Assert.AreEqual(PartidaPrueba.ResolverCartasNormales("Fuerza", 1, 4), "EMPATE");
            Assert.AreEqual(PartidaPrueba.jugadores[0].Cartas.Count, 2);
            Assert.AreEqual(PartidaPrueba.jugadores[1].Cartas.Count, 2);
        }

        [TestMethod]
        public void NoDeberiaDejarJugarCuandoSeEligeUnAtributoQueEnLaOtraCartaNoEsta()
        {
            var PartidaPrueba = new Partida();
            var Jugador1 = new Jugador().IdConexion("Id1").Nombre("Maxi").Numero(NumJugador.uno);
            var Jugador2 = new Jugador().IdConexion("Id2").Nombre("Lautaro").Numero(NumJugador.dos);
            PartidaPrueba.jugadores.Add(Jugador1);
            PartidaPrueba.jugadores.Add(Jugador2);

            var Atributo1 = new Atributo() { Nombre = "Fuerza", Valor = 20 };
            var Atributo2 = new Atributo() { Nombre = "Velocidad", Valor = 30 };
            var Atributo3 = new Atributo() { Nombre = "Fuerza", Valor = 20 };
            var Atributo4 = new Atributo() { Nombre = "Resistencia", Valor = 40 };

            List<Atributo> Lista1 = new List<Atributo>();
            Lista1.Add(Atributo1); Lista1.Add(Atributo3);
            List<Atributo> Lista2 = new List<Atributo>();
            Lista2.Add(Atributo3); Lista2.Add(Atributo4);

            var Carta1 = new Carta() { IdCarta = 1, Atributos = Lista1 };
            var Carta2 = new Carta() { IdCarta = 2, Atributos = Lista2 };
            var Carta3 = new Carta() { IdCarta = 3, Atributos = Lista1 };
            var Carta4 = new Carta() { IdCarta = 4, Atributos = Lista2 };

            Jugador1.Cartas.Add(Carta1); Jugador1.Cartas.Add(Carta2);
            Jugador2.Cartas.Add(Carta3); Jugador2.Cartas.Add(Carta4);
            Assert.AreEqual(PartidaPrueba.ResolverCartasNormales("Resistencia", 1, 4), "ELEGIR OTRO ATRIBUTO");
            Assert.AreEqual(PartidaPrueba.jugadores[0].Cartas.Count, 2);
            Assert.AreEqual(PartidaPrueba.jugadores[1].Cartas.Count, 2);
        }

        [TestMethod]
        public void DeberiaPoderCrearPartida()
        {
            var nuevapartida = new Partida();
            nuevapartida.Turno = "Uno";
            nuevapartida.EstaCompleto = true;

            Assert.AreEqual(true, nuevapartida.EstaCompleto);
            Assert.AreEqual("Uno", nuevapartida.Turno);
            Assert.AreEqual(0, nuevapartida.jugadores.Count);
            Assert.AreEqual(null, nuevapartida.mazo);
        }

        [TestMethod]
        public void DeberiaPoderAgregarJugadoresALaPartida()
        {
            var nuevapartida = new Partida();
            var jugador1 = new Jugador();
            var jugador2 = new Jugador();

            nuevapartida.jugadores.Add(jugador1);
            nuevapartida.jugadores.Add(jugador2);

            Assert.AreEqual(2, nuevapartida.jugadores.Count);
        }

        [TestMethod]
        public void DeberiaPoderAsignarUnMazoAUnaPartida()
        {
            var nuevapartida = new Partida();
            var mazoxmen = new Mazo();

            nuevapartida.mazo = mazoxmen;

            Assert.AreEqual(mazoxmen, nuevapartida.mazo);
        }

        [TestMethod]
        public void DeberiaPoderControlarCanrtidadDeJugadores()
        {
            var nuevapartida = new Partida();
            var jugador1 = new Jugador();
            var jugador2 = new Jugador();

            nuevapartida.jugadores.Add(jugador1);
            nuevapartida.jugadores.Add(jugador2);

            nuevapartida.EstaCompleto = false;
            nuevapartida.RevisarCantidadJugadores();

            Assert.AreEqual(true, nuevapartida.EstaCompleto);

        }

        [TestMethod]
        public void SeDeberianMezclarLasCartas()
        {
            var nuevapartida = new Partida();
            var carta1 = new Carta(); carta1.IdCarta = 1;
            var carta2 = new Carta(); carta2.IdCarta = 2;
            var carta3 = new Carta(); carta3.IdCarta = 3;
            var carta4 = new Carta(); carta4.IdCarta = 4;
            var carta5 = new Carta(); carta5.IdCarta = 5;

            var mazzo = new Mazo();
            mazzo.Cartas.Add(carta1);
            mazzo.Cartas.Add(carta2);
            mazzo.Cartas.Add(carta3);
            mazzo.Cartas.Add(carta4);
            mazzo.Cartas.Add(carta5);

            nuevapartida.mazo = mazzo;
            nuevapartida.MezclarCartas();
            //Ver porque si sale la id 1 el test no va a funcionar (aunque las cartas se mezclen)
            Assert.AreNotEqual(1, nuevapartida.mazo.Cartas[1]);
        }

        [TestMethod]
        public void SeDeberiaRepartirTodasLasCartas()
        {
            var nuevapartida = new Partida();
            var carta1 = new Carta(); carta1.IdCarta = 1;
            var carta2 = new Carta(); carta2.IdCarta = 2;
            var carta3 = new Carta(); carta3.IdCarta = 3;
            var carta4 = new Carta(); carta4.IdCarta = 4;

            var mazzo = new Mazo();
            mazzo.Cartas.Add(carta1);
            mazzo.Cartas.Add(carta2);
            mazzo.Cartas.Add(carta3);
            mazzo.Cartas.Add(carta4);

            nuevapartida.mazo = mazzo;

            var jugador1 = new Jugador();
            jugador1.NumeroJugador = NumJugador.uno;
            var jugador2 = new Jugador();
            jugador1.NumeroJugador = NumJugador.dos;

            nuevapartida.jugadores.Add(jugador1);
            nuevapartida.jugadores.Add(jugador2);

            nuevapartida.RepartirCartas();

            Assert.AreEqual(2, jugador1.Cartas.Count);
        }
    }
}
