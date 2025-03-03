using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class Tablero
    {
        public int TamTablero { get; }

        private List<Coordenada> coordenadasDisparadas;
        private List<Coordenada> coordenadasTocadas;
        private List<Barco> barcos;
        private List<Barco> barcosEliminados;
        private Dictionary<Coordenada, string> casillasTablero;

        public Tablero(int tamTablero, List<Barco> barcos)
        {
            if (tamTablero >= 4 && tamTablero <= 9)
            {
                TamTablero = tamTablero;
            }
            else
            {
                throw new Exception();
            }
            this.barcos = new List<Barco>(barcos);
            coordenadasDisparadas = new List<Coordenada>();
            coordenadasTocadas = new List<Coordenada>();
            barcosEliminados = new List<Barco>();
            casillasTablero = new Dictionary<Coordenada, string>();
        }

        private void iniciaCasillasTablero()
        {
            for (int i = 0; i < TamTablero; i++)
            {
                for (int j = 0; j < TamTablero; j++)
                {
                    foreach (var barco in barcos)
                    {
                        Coordenada casilla = new Coordenada(i, j);
                        if (barco.CoordenadasBarco.ContainsKey(casilla))
                        {
                            casillasTablero.Add(casilla, barco.Nombre);
                        }
                        else
                        {
                            casillasTablero.Add(casilla, "AGUA");
                        }
                    }
                }
            }
        }

        public void Disparar(Coordenada c)
        {
            if (c.Columna > TamTablero && c.Fila > TamTablero)
            {
                foreach (var barco in barcos)
                {
                    if (barco.CoordenadasBarco.ContainsKey(c))
                    {
                        coordenadasDisparadas.Add(c);
                        //barco.disparar(c);
                    }

                }
            }
            else
            {
                Console.Write("La coordenada (" + c.Fila + "," + c.Columna + ") está fuera de\r\nlas dimensiones del tablero.");
            }
        }


    }