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

        //private void ActualizarRanking()
        //{
        //    var ListaRetornar = new List<Ranking>();

        //    foreach (var item in Partidas)
        //    {

        //    }
        //}

 
    }
}
