using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class TocadoArgs : EventArgs
    {
        public TocadoArgs(string nombre, string coordenadas)
        {
            Console.Write($"TABLERO: Barco {nombre} tocado en Coordenada: [{coordenadas}]");
        }
        
    }
    public class HundidoArgs : EventArgs
    {
        public HundidoArgs(string nombre)
        {
            Console.Write(", y ha sido hundido");
        }
    }
}
