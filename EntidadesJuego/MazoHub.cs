using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesJuego
{
    public class MazoHub
    {
        public string Nombre { get; set; }
        public List<string> NombreAtributos { get; set; }

        public MazoHub()
        {
            this.NombreAtributos = new List<string>();
        }
    }
}
