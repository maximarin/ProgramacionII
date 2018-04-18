﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesJuego
{
    public class Juego
    {
        public List<Partida> Partidas { get; set; }

        //HAY UN SOLO JUEGO, POR LO TANTO SE APLICA SINGLETON

        private Juego()
        {
            Partidas = new List<Partida>();
        }

        private static Juego nuevoJuego;

        public static Juego CrearJuego ()
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




    }
}