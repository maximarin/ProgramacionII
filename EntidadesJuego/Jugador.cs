﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesJuego
{
    public enum NumJugador { uno, dos }
    public  class Jugador
    {
        public string IdConexion { get; set; }
        public string Nombre { get; set; }
        public List<Carta> Cartas { get;  }
        public NumJugador NumeroJugador { get; set; }
        //VER AVATAR 

        public Jugador ()
        {
            Cartas = new List<Carta>();
        }

       
        

    }
}