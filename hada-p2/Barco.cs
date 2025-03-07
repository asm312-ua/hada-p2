using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class Barco
    {
        public event EventHandler<TocadoEventArgs> eventoTocado;
        public event EventHandler<HundidoEventArgs> eventoHundido;
        public Dictionary<Coordenada, string> CoordenadasBarco { get; private set; }
        public string Nombre { get; }
        public int NumDanyos { get; private set; }

        /*
         * Funcion constuctor de la clase Barco:
           * Se encarga de crear un barco segun su longitud y orientacion
           * iniclializa las coordenadas y tiene en cuenta el borde derecho del mapa
           * el izquierdo ya esta gestionado en coordenadas
           * Inicializa tambien tanto su diccionario de coordenadas como su numero de daños y nombre.
         */
        public Barco(string nombre, int longitud, char orientacion, Coordenada coordenadaInicio)
        {
            Nombre = nombre;
            NumDanyos = 0;
            CoordenadasBarco = new Dictionary<Coordenada, string>();
            switch (orientacion)
            {
                case 'v':
                    if (coordenadaInicio.Columna + longitud > Game.tamTablero) 
                    {//Si el barco se saliera del tablero lo ponemos inversamente [0,9] [0,8] [0,7] [0,6] longitud 4
                        for (int i = 1; i < longitud; i++)
                        {
                            Coordenada nuevaCoordenada = new Coordenada(coordenadaInicio.Fila, Game.tamTablero - i);
                            CoordenadasBarco.Add(nuevaCoordenada, Nombre);
                        }
                    }
                    else
                    {
                        CoordenadasBarco.Add(coordenadaInicio, Nombre);
                        for (int i = 1; i < longitud; i++)
                        {// En caso contrario lo ponemos normal [0,3] [0,4] [0,5] [0,6] longitud 4
                            Coordenada nuevaCoordenada = new Coordenada(coordenadaInicio.Fila, coordenadaInicio.Columna + i);
                            CoordenadasBarco.Add(nuevaCoordenada, Nombre);
                        }
                    }
                    break;

                case 'h':
                    if (coordenadaInicio.Fila + longitud > Game.tamTablero)
                    {
                        for (int i = 1; i < longitud; i++)
                        {
                            Coordenada nuevaCoordenada = new Coordenada(Game.tamTablero - i, coordenadaInicio.Columna);
                            CoordenadasBarco.Add(nuevaCoordenada, Nombre);
                        }
                    }
                    else
                    {
                        CoordenadasBarco.Add(coordenadaInicio, Nombre);
                        for (int i = 1; i < longitud; i++)
                        {
                            Coordenada nuevaCoordenada = new Coordenada(coordenadaInicio.Fila + i, coordenadaInicio.Columna);
                            CoordenadasBarco.Add(nuevaCoordenada, Nombre);
                        }
                    }
                    break;
            }
        }

        /*
         * Funcion de Barco que envia un evento de TocadoEventArgs si la coordenada esta en el diccionario de coordenadas del barco
         * y añade un daño al barco, si el barco esta hundido envia un evento de HundidoEventArgs
         */
        public void disparo(Coordenada c){

            if (CoordenadasBarco.TryGetValue(c, out string etiqueta)){
                CoordenadasBarco[c] = etiqueta + "_T";

                if (eventoTocado != null)
                {
                    eventoTocado(this, new TocadoEventArgs(this.Nombre, c));
                }

                NumDanyos++;

                if (hundido()) { eventoHundido(this, new HundidoEventArgs(this.Nombre)); }
            }
        }

        /*
         * Funcion que comprueba si el barco actual esta hundido
         */
        public bool hundido(){
            foreach (var parteBarco in CoordenadasBarco)
            {
                if(!parteBarco.Value.EndsWith("_T"))
                    return false;
            }
            return true;
        }

        /*
         * Sobreescritura del metodo ToString(), para mostrar la informacion del barco actual
         */
        override
        public string ToString(){
            string output;
            output = $"[{Nombre}] - Daños [{NumDanyos}] - HUNDIDO: [{hundido()}] - COORDENADAS: ";
            foreach (var parteBarco in CoordenadasBarco)
            {
                output += $"[{parteBarco.Key.ToString()} : {parteBarco.Value}] ";
            }
            return output;
        }
    }
}
