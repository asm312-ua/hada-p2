using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class Game
    {
        private bool finPartida;

        private void gameLoop() 
        {
            Coordenada coordenadaThor = new Coordenada(0, 0);
            Coordenada coordenadaLoki = new Coordenada(1, 2);
            Coordenada coordenadaMaya = new Coordenada(3, 1);

            Barco Thor = new Barco("THOR",1,'h',coordenadaThor);
            Barco Loki = new Barco("LOKI",2,'h',coordenadaLoki); 
            Barco Maya = new Barco("MAYA",3,'v',coordenadaMaya);

            List<Barco> barcos = new List<Barco>();
            barcos.Add(Thor);
            barcos.Add(Loki);
            barcos.Add(Maya);

            Tablero tablero = new Tablero(4,barcos);

            int filaPedida;
            int columnaPedida;
            Coordenada coordenadaPedida = new Coordenada();
            while (true) 
            {
                Console.WriteLine("Introduce la coordenada a la que disparar FILA,COLUMNA ('S' para salir");
                string respuesta = Console.ReadLine();
                while (!Int32.TryParse(char.ToString(respuesta[0]),out filaPedida) || respuesta[1]!=',' || !Int32.TryParse(char.ToString(respuesta[2]), out columnaPedida)) //comprueba si las coordenadas se han colocado como se pide
                {
                    Console.WriteLine("Introduce la coordenada a la que disparar FILA,COLUMNA ('S' para salir");
                    respuesta = Console.ReadLine();
                }
                coordenadaPedida.Columna=columnaPedida;
                coordenadaPedida.Fila = filaPedida;

            }
        }
    }
}
