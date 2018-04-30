using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesJuego
{
    public class Ranking
    {
        public string Nombre { get; set; }
        public int VecesQueGano { get; set; }

        public Ranking()
        {
            this.VecesQueGano = 0;
        }
    }
}
