using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesJuego
{
    public class Juego
    {
        public List<Partida> Partidas { get; set; }
        public List<Ranking> Rankings { get; set; }
        public List<Jugador> Jugadores { get; set; }
        public List<Mazo> Mazos { get; set; }

        public Juego()
        {
            this.Partidas = new List<Partida>();
            this.Mazos = new List<Mazo>();
            this.Mazos = AgregarMazos();
            this.Jugadores = new List<Jugador>();
        }

        public Partida AgregarPartida(Partida nuevaPartida)
        {
            Partidas.Add(nuevaPartida);
            return nuevaPartida;
        }

        public List<Mazo> AgregarMazos()
        {
            var deckFolder = Directory.GetDirectories(@"C:\Users\maxi_\Desktop\Juego\Cromy.web\Mazos");           

            foreach (var deck in deckFolder)
            {
                var lines = File.ReadAllLines(deck + "\\informacion.txt");
                int cont = 0;
                var nuevoMazo = new Mazo();
                string[] datos;
                List<Atributo> Atributos = new List<Atributo>();

                var cartaRoja = new Carta();
                cartaRoja.IdCarta = "roja";
                cartaRoja.TipoCarta = TipoDeCarta.Roja;
                cartaRoja.Atributos = null;

                var cartaAmarilla = new Carta();
                cartaAmarilla.IdCarta = "amarilla";
                cartaAmarilla.TipoCarta = TipoDeCarta.Amarilla;
                cartaAmarilla.Atributos = null;

                nuevoMazo.Cartas.Add(cartaRoja);
                nuevoMazo.Cartas.Add(cartaAmarilla);

                foreach (var line in lines)   //LEO EL ARCHIVO
                {
                    if (cont == 0)
                    {
                        nuevoMazo.Nombre = line;  //SI ESTOY EN LA PRIMER LÍNEA DEFINO EL NOMBRE DEL MAZO 
                    }
                    else
                    {
                        datos = line.Split('|');

                        if (cont == 1)     //SI ES LA SEGUNDA LÍNEA AÑADO LOS ATRIBUTOS A UN VECTOR 
                        {

                            for (int i = 0; i < datos.Length; i++)
                            {
                                var atrib = new Atributo();
                                atrib.Nombre = datos[i];
                                Atributos.Add(atrib);
                            }
                        }
                        else
                        {
                            var nuevaCarta = new Carta();        //A PARTIR DE LA TERCER LÍNEA VOY CREANDO LAS CARTAS Y ASIGNANDO LOS VALORES A LOS ATRIBUTOS
                            List<Atributo> nuevosAtributos = new List<Atributo>();
                            foreach (var item in Atributos)
                            {
                                Atributo atribut = new Atributo();
                                atribut.Nombre = item.Nombre;
                                nuevosAtributos.Add(atribut);
                            }

                            nuevaCarta.Atributos = nuevosAtributos;
                            nuevaCarta.TipoCarta = TipoDeCarta.Normal;
                            int j = 0;
                            for (int i = 0; i < datos.Length; i++)
                            {
                                if (i == 0)
                                {
                                    nuevaCarta.IdCarta = datos[i];
                                }
                                else
                                {
                                    if (i == 1)
                                    {
                                        nuevaCarta.Nombre = datos[i];

                                    }
                                    else
                                    {
                                        nuevaCarta.Atributos[j].Valor = Convert.ToDouble(datos[i]);
                                        j++;
                                    }
                                }
                            }
                            nuevoMazo.Cartas.Add(nuevaCarta);
                        }
                    }
                    cont++;
                  
                }

                Mazos.Add(nuevoMazo);

            }


            return this.Mazos;
        }

        public List<PartidasHub> RetornarPartidas()
        {
            List<PartidasHub> ListaRetornar = new List<PartidasHub>();

            foreach (var item in this.Partidas)
            {
                if (item.EstaCompleto == false)
                {
                    var x = new PartidasHub();
                    x.Usuario = item.jugadores[0].nombre;
                    x.Mazo = item.Mazo.Nombre;
                    x.Nombre = item.Nombre;

                    ListaRetornar.Add(x);
                }
            }
            return ListaRetornar;
        }

        public Mazo BuscarMazo(string nombre)
        {
            var mazo = Mazos.Where(x => x.Nombre == nombre).FirstOrDefault();
            return mazo;
        }

        //public Partida AgregarSegundoJugador(Jugador jug, string parti)
        //{
        //    var par = new Partida();
        //    foreach (var item in this.Partidas)
        //    {
        //         if (item.nombre==parti)
        //        {
        //            item.jugadores.Add(jug);
        //            par = item;
        //        }
        //    }
        //    return par;
        //}

        public List<string> NombreDeLosMazos()
        {
            var ListaRertornar = new List<string>();

            foreach (var item in this.Mazos)
            {
                ListaRertornar.Add(item.Nombre);
            }

            return ListaRertornar;
        }

        public DibujarTableroHub DibujarTablero(Partida p)
        {
            var Retornar = new DibujarTableroHub();
            Retornar.Jugador1.Nombre = p.jugadores[0].nombre;
            Retornar.Jugador2.Nombre = p.jugadores[1].nombre;

            foreach (var item in p.jugadores[0].Cartas)
            {
                var x = new CartaHub();
                x.Codigo = item.IdCarta.ToString();
                x.Nombre = item.Nombre;
                Retornar.Jugador1.Cartas.Add(x);
            }

            foreach (var item in p.jugadores[1].Cartas)
            {
                var x = new CartaHub();
                x.Codigo = item.IdCarta.ToString();
                x.Nombre = item.Nombre;
                Retornar.Jugador2.Cartas.Add(x);
            }

            Retornar.Mazo.Nombre = p.Mazo.Nombre;

            foreach (var item in p.Mazo.Cartas)
            {
                if (item.TipoCarta == TipoDeCarta.Normal)
                {
                    foreach (var item2 in item.Atributos)
                    {
                        Retornar.Mazo.NombreAtributos.Add(item2.Nombre);
                    }
                    break;
                }
            }

            return Retornar;
        }



    }
}
