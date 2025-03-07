using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class TocadoEventArgs : EventArgs
    {
        public string Nombre { get;  set; }
        public Coordenada CoordenadaImpacto { get; set; }
        public TocadoEventArgs(string nombre, Coordenada coordenadaImpacto)
        {
            this.Nombre = nombre;
            this.CoordenadaImpacto = coordenadaImpacto;
        }
    }

    
    public class HundidoEventArgs : EventArgs
    {
        public string Nombre { get; set; }
        public HundidoEventArgs(string nombre)
        {
            Nombre = nombre;
        }
    }

}
