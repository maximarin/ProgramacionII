using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesJuego
{
    public class Juego
    {
        public List<Partida> Partidas { get; set; }
        public List<Ranking> Rankings { get; set; }

        //HAY UN SOLO JUEGO, POR LO TANTO SE APLICA SINGLETON

        private Juego()
        {
            this.Partidas = new List<Partida>();
            this.Rankings = new List<Ranking>();
        }

        private static Juego nuevoJuego;

        public static Juego CrearJuego()
        {
            if (nuevoJuego == null)
            {
                nuevoJuego = new Juego();
            }

            return nuevoJuego;

        }

        public static Partida AgregarPartida(Partida nuevaPartida)
        {
            nuevoJuego.Partidas.Add(nuevaPartida);
            return nuevaPartida;
        }

        public void ActualizarRanking()
        {
            var ListaRetornar = new List<Ranking>();

            foreach (var item in Partidas)
            {
                if (item.HayCartas(item.jugadores.First(x => x.NumeroJugador == NumJugador.uno), item.jugadores.First(x => x.NumeroJugador == NumJugador.dos)))
                {
                    var jugadoralrankig = item.DetectarJugadorPerdedor(item.jugadores.First(x => x.NumeroJugador == NumJugador.uno), item.jugadores.First(x => x.NumeroJugador == NumJugador.dos));

                    Jugador agregar = new Jugador();

                    if (jugadoralrankig == item.jugadores.First(x => x.NumeroJugador == NumJugador.uno))
                    {
                        agregar = jugadoralrankig;
                    }
                    else
                    {
                        if (jugadoralrankig == item.jugadores.First(x => x.NumeroJugador == NumJugador.dos))
                        {
                            agregar = jugadoralrankig;
                        }
                    }

                    Ranking nuevo = new Ranking();
                    nuevo.Nombre = agregar.nombre;                    

                    if (this.Rankings.Count == 0)
                    {
                        nuevo.VecesQueGano = 1;
                        ListaRetornar.Add(nuevo);                        
                    }
                    else
                    {
                        bool ok = false;
                        foreach (var item2 in ListaRetornar)
                        {
                            if (item2.Nombre == nuevo.Nombre)
                            {
                                item2.VecesQueGano = item2.VecesQueGano + 1;
                                ok = true;
                            }
                        }

                        if (ok == false)
                        {
                            nuevo.VecesQueGano = 1;
                            ListaRetornar.Add(nuevo);                            
                        }
                    }
                }
            }
            ListaRetornar.OrderByDescending(x => x.VecesQueGano);
            this.Rankings = ListaRetornar;
        }


    }
}
