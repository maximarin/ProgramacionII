using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntidadesJuego;
using System.Collections.Generic;

namespace TestsUnitarios
{
    [TestClass]
    public class UnitTest1
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


    }
}
