using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class Tablero
    { 
        public event EventHandler<EventArgs> eventoFinPartida;

        public int TamTablero { get; }
        private List<Coordenada> coordenadasDisparadas;
        private List<Coordenada> coordenadasTocadas;
        private List<Barco> barcos;
        private List<Barco> barcosEliminados;
        private Dictionary<Coordenada, string> casillasTablero;

        public Tablero(int tamTablero, List<Barco> barcos)
        {
            if (tamTablero >= Game.tamTableroMin && tamTablero <= Game.tamTableroMax)
            {
                TamTablero = tamTablero;
            }
            else
            {
                throw new Exception();
            }

            coordenadasDisparadas = new List<Coordenada>();
            coordenadasTocadas = new List<Coordenada>();
            barcosEliminados = new List<Barco>();
            casillasTablero = new Dictionary<Coordenada, string>();

            this.barcos = new List<Barco>(barcos);
            foreach (var barco in barcos)
            {
                barco.eventoTocado += cuandoEventoTocado;
                barco.eventoHundido += cuandoEventoHundido;
            }

            iniciaCasillasTablero();
        }

        private void iniciaCasillasTablero()//Revisar la forma de buscar, condicion if y dentro del if
        {
            bool hayUnBarco;
            for (int i = 0; i < TamTablero; i++)
            {
                for (int j = 0; j < TamTablero; j++)
                {
                    Coordenada casilla = new Coordenada(i, j);
                    hayUnBarco = false;
                    foreach (var barco in barcos)
                    {
                        if (barco.CoordenadasBarco.ContainsKey(casilla))
                        {
                            casillasTablero.Add(casilla, barco.Nombre);
                            hayUnBarco = true;
                        }
                    }
                    if (!hayUnBarco) 
                    {
                        casillasTablero.Add(casilla, "AGUA");
                    }
                }
            }
        }

        private void cuandoEventoTocado(object sender, TocadoEventArgs e)
        {
            casillasTablero[e.CoordenadaImpacto] = e.Nombre + "_T";
            if (!coordenadasTocadas.Contains(e.CoordenadaImpacto)) //No lo he comprobado pero posíblemente esta línea no funciona porque Contains comprueba que el objeto está en coordenadasTocadas, no si hay un objeto con fila y columna idénticas
            {
                coordenadasTocadas.Add(new Coordenada(e.CoordenadaImpacto));
            }
            Console.WriteLine($"TABLERO: Barco {e.Nombre} tocado en Coordenada: [{e.CoordenadaImpacto}]");
        }
        private void cuandoEventoHundido(object sender, HundidoEventArgs e)
        {
            Console.WriteLine($"TABLERO: Barco {e.Nombre} hundido!!");
            Barco barcoHundido = (Barco) sender;//TODO revisar seguramente este mal esta relacion
            barcosEliminados.Add(barcoHundido);
            if (barcosEliminados.Count == barcos.Count)
            {
                eventoFinPartida?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Disparar(Coordenada c)
        {
            if ((c.Columna >= 0 && c.Columna <= TamTablero) && (c.Fila <= TamTablero && c.Fila >= 0))
            {
                bool disparoRepetido = false;
                foreach (var coordenada in coordenadasDisparadas)
                {
                    if (coordenada.Equals(c))
                    {
                        disparoRepetido = true;
                    }
                }
                coordenadasDisparadas.Add(new Coordenada(c));
                if (!disparoRepetido)
                {

                    foreach (var barco in barcos)
                    {
                        if (barco.CoordenadasBarco.ContainsKey(c))
                        {
                            coordenadasTocadas.Add(new Coordenada(c));
                            barco.disparo(c);
                        }
                    }
                }
            }
            else
            {
                Console.Write("La coordenada (" + c.Fila + "," + c.Columna + ") está fuera de\r\nlas dimensiones del tablero.");
            }
        }
        public string DibujarTablero()
        {
            string tableroDibujado = "";
            int fila = 0;

            foreach (var casillas in casillasTablero)
            {
                if (fila < TamTablero)
                {
                    tableroDibujado += $"[{casillas.Value}]";
                    fila++;
                }
                else
                {
                    fila = 1;
                    tableroDibujado += $"\r\n[{casillas.Value}]";
                }
            }
            return tableroDibujado;
        }
        override
        public string ToString()
        {
            string outputInformacionBarcos = "";
            foreach (var barco in barcos)
            {
                outputInformacionBarcos += $"{barco.ToString()} \n";//Aparentemente se podria poner como: $"{barco} \r\n"
            }

            outputInformacionBarcos += "\n Coordenadas disparadas:";
            foreach (var coordenada in coordenadasDisparadas)
            {
                outputInformacionBarcos += $"{coordenada.ToString()} ";
            }

            outputInformacionBarcos += " \n Coordenadas tocadas: ";
            foreach (var coordenada in coordenadasTocadas)
            {
                outputInformacionBarcos += $"{coordenada.ToString()} ";
            }
            outputInformacionBarcos += " \n\nCASILLAS TABLERO \n-------";
            outputInformacionBarcos += $"\n{DibujarTablero()}";

            return outputInformacionBarcos;
        }
    }


}