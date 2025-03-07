using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class Game
    {
        private bool finPartida;
        public static int tamTablero = 0;
        public static int tamTableroMax = 15;
        public static int tamTableroMin = 4;



        public Game() 
        {
            finPartida = false;
            this.gameLoop();
        }

        private bool ColocarBarco(Barco barcoAColocar, List<Barco> barcosHechos)
        {
            foreach (var barco in barcosHechos)
            {
                foreach (var coordenada in barcoAColocar.CoordenadasBarco)
                {
                    if (barco.CoordenadasBarco.ContainsKey(coordenada.Key))
                    {
                        return false;
                    }
                }
            }

            barcosHechos.Add(barcoAColocar);
            return true;
        }
        private void gameLoop() 
        {
            List<string> nombres = new List<string>();

            nombres.Add("THOR");
            nombres.Add("LOKI");
            nombres.Add("MAYA");
            nombres.Add("MARY");
            nombres.Add("WILLY");

            do
            {
                if (tamTablero < tamTableroMin || tamTablero > tamTableroMax)
                {
                    Console.WriteLine($"Introduce un tamaño de tablero entre {tamTableroMin} y {tamTableroMax}");
                    if (!Int32.TryParse(Console.ReadLine(), out tamTablero))
                    {
                        tamTablero = 0;
                    }
                }
            } while (tamTablero < tamTableroMin || tamTablero > tamTableroMax);
            List<Barco> barcos = new List<Barco>();
            Random rnd = new Random();
            int i = 1;
            while (i<=3)//i<=3 represena la cantidad de barcos que se van a crear
            {
                int fila = (rnd.Next(0, tamTablero-i+1));
                int columna = (rnd.Next(0, tamTablero-i+1));
                char direccion;
                if (rnd.Next(0, 2) == 1)
                {
                    direccion = 'v';
                }
                else
                {
                    direccion = 'h';
                }
                if (ColocarBarco(new Barco(nombres[i-1], i, direccion, new Coordenada(fila, columna)), barcos))
                {
                    i++;
                }
            }


            Tablero tablero = new Tablero(tamTablero, barcos);
            
            int filaPedida;
            int columnaPedida;
            Coordenada coordenadaPedida = new Coordenada();
            tablero.eventoFinPartida += cuandoEventoFinPartida; //sin importar si cambia el evento, esto debería funcionar
            while (!finPartida) 
            {
                Console.WriteLine(tablero);
                Console.WriteLine("Introduce la coordenada a la que disparar FILA,COLUMNA ('S' para salir)");
                string respuesta = Console.ReadLine();
                var respuestaPartida = respuesta.Split(',');
                while (respuestaPartida.Length != 2 || !Int32.TryParse(respuestaPartida[0], out  filaPedida) || !Int32.TryParse(respuestaPartida[1], out  columnaPedida)) //comprueba si las coordenadas se han colocado como se pide
                {
                    
                    if (!(respuesta == "") && (respuesta[0] == 's' || respuesta[0] == 'S' || finPartida))
                    {
                        //                    cuandoEventoFinPartida(EventArgs );
                        return; //Provisional
                    }
                    Console.WriteLine("Introduce la coordenada a la que disparar FILA,COLUMNA ('S' para salir)");
                    respuesta = Console.ReadLine();
                    respuestaPartida = respuesta.Split(',');
                }
                Console.Clear();
                coordenadaPedida.Columna= columnaPedida;
                coordenadaPedida.Fila = filaPedida;
                tablero.Disparar(coordenadaPedida);
                if (finPartida) { Console.WriteLine(tablero); }
            }
        }

        private void cuandoEventoFinPartida(object algo,EventArgs finDePartida) { //Si cambia el delegado del evento eventoFinPartida, cambiar parámetros
            Console.WriteLine("PARTIDA FINALIZADA!!");
            finPartida = true;
            

        }
    }
}
