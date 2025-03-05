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
        public string Coordenadas { get;  set; }
    }

    public class TocadoArgs
    {
        public delegate void TocadoEventHandler(object sender, TocadoEventArgs args);
        public event TocadoEventHandler Tocado;

        public void TocarBarco(string name, string coordenadas)
        {
            OnTocado(name, coordenadas);
        }

        protected virtual void OnTocado(string nombre, string coordenadas)
        {
            Tocado?.Invoke(this, new TocadoEventArgs() { Nombre = nombre, Coordenadas = coordenadas });
        }
    }
    public class HundidoEventArgs : EventArgs
    {
        public string Nombre { get; set; }
    }

    public class HundidoArgs
    {
        public delegate void HundidoEventHandler(object sender, HundidoEventArgs args);
        public event HundidoEventHandler Hundido;
        public void HundirBarco(string name)
        {
            OnHundido(name);
        }
        protected virtual void OnHundido(string nombre)
        {
            Hundido?.Invoke(this, new HundidoEventArgs() { Nombre = nombre });
        }
    }

}
