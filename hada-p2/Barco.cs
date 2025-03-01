using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class Barco
    {
        //Quitar los comentarios a los objetos cuando este hecha la clase evento
        event EventHandler<TocadoArgs> eventoTocado;
        event EventHandler<HundidoArgs> eventoHundido;
        public Dictionary<Coordenada, string> CoordenadasBarco { get; private set; }
        public string Nombre { get; }
        public int NumDanyos { get; private set; }

        public Barco(string nombre, int longitud, char orientacion, Coordenada coordenadaInicio)
        {//Pensar en lanzar una excepcion si la suma y resta de la longitud a la posicion del barco hacen que se salga del tablero
            Nombre = nombre;
            NumDanyos = 0;
            CoordenadasBarco = new Dictionary<Coordenada, string>();

            CoordenadasBarco.Add(coordenadaInicio, Nombre);
            switch (orientacion)
            {
                case 'v':
                    if (coordenadaInicio.Columna + longitud > 9)
                    {//Si el barco se saliera del tablero lo ponemos inversamente [0,9] [0,8] [0,7] [0,6] longitud 4
                        for (int i = 1; i < longitud; i++)
                        {
                            Coordenada nuevaCoordenada = new Coordenada(coordenadaInicio.Fila, coordenadaInicio.Columna - i);
                            CoordenadasBarco.Add(nuevaCoordenada, Nombre);
                        }
                    }
                    else
                    {
                        for (int i = 1; i < longitud; i++)
                        {// En caso contrario lo ponemos normal [0,3] [0,4] [0,5] [0,6] longitud 4
                            Coordenada nuevaCoordenada = new Coordenada(coordenadaInicio.Fila, coordenadaInicio.Columna + i);
                            CoordenadasBarco.Add(nuevaCoordenada, Nombre);
                        }
                    }
                    break;

                case 'h':
                    if (coordenadaInicio.Fila + longitud > 9)
                    {
                        for (int i = 1; i < longitud; i++)
                        {
                            Coordenada nuevaCoordenada = new Coordenada(coordenadaInicio.Fila - i, coordenadaInicio.Columna);
                            CoordenadasBarco.Add(nuevaCoordenada, Nombre);
                        }
                    }
                    else
                    {
                        for (int i = 1; i < longitud; i++)
                        {
                            Coordenada nuevaCoordenada = new Coordenada(coordenadaInicio.Fila + i, coordenadaInicio.Columna);
                            CoordenadasBarco.Add(nuevaCoordenada, Nombre);
                        }
                    }
                    break;
            }
        }

        public void disparo(Coordenada c){
            if (CoordenadasBarco.TryGetValue(c, out string etiqueta)){
                CoordenadasBarco[c] = etiqueta + "_T";
                eventoTocado?.Invoke(this, new TocadoArgs(Nombre, c.ToString()));
                NumDanyos++;

                if (hundido()) {eventoHundido?.Invoke(this, new HundidoArgs(Nombre));}
                Console.WriteLine("");//añadimos el \n
            }

        }
        public bool hundido(){
            int auxContador = 0;
            foreach (var parteBarco in CoordenadasBarco)
            {
                if(parteBarco.Value.EndsWith("_T"))
                    auxContador++;
            }
            if(auxContador == CoordenadasBarco.Count) { return true; }
            return false;
        }
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
