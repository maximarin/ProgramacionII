﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesJuego
{
   public class Mazo
    {
        public List<Carta> Cartas { get; set; }

        public Mazo ()
        {
            Cartas = new List<Carta>(); 
        }

        public void AgregarCartaAlMazo (Carta carta)
        {      
            Cartas.Add(carta);
            return;
        }

    }
}
