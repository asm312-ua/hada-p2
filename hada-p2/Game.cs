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

        public Game() 
        {
            finPartida = false;
            this.gameLoop();
        }
        private void gameLoop() 
        {
            Barco Thor = new Barco("THOR",1,'h', new Coordenada(0, 0));
            Barco Loki = new Barco("LOKI",2,'h', new Coordenada(1, 2)); 
            Barco Maya = new Barco("MAYA",3,'v', new Coordenada(3, 1));

            List<Barco> barcos = new List<Barco>();
            barcos.Add(Thor);
            barcos.Add(Loki);
            barcos.Add(Maya);

            Tablero tablero = new Tablero(4,barcos);

            int filaPedida;
            int columnaPedida;
            Coordenada coordenadaPedida = new Coordenada();
            tablero.eventoFinPartida += cuandoEventoFinPartida; //sin importar si cambia el evento, esto debería funcionar
            while (true) 
            {
                Console.WriteLine("Introduce la coordenada a la que disparar FILA,COLUMNA ('S' para salir)");
                string respuesta = Console.ReadLine();
                if (respuesta[0] == 's' || respuesta[0] == 'S' || finPartida)
                {
//                    cuandoEventoFinPartida(EventArgs );
                    break; //Provisional
                }
                while (respuesta.Length<3 || !Int32.TryParse(char.ToString(respuesta[0]),out filaPedida) || respuesta[1]!=',' || !Int32.TryParse(char.ToString(respuesta[2]), out columnaPedida)) //comprueba si las coordenadas se han colocado como se pide
                {
                    Console.WriteLine("Introduce la coordenada a la que disparar FILA,COLUMNA ('S' para salir)");
                    respuesta = Console.ReadLine();
                }

                coordenadaPedida.Columna=columnaPedida;
                coordenadaPedida.Fila = filaPedida;
                tablero.Disparar(coordenadaPedida);
                Console.WriteLine(tablero);
            }
        }

        private void cuandoEventoFinPartida(object algo,EventArgs finDePartida) { //Si cambia el delegado del evento eventoFinPartida, cambiar parámetros
            Console.WriteLine("PARTIDA FINALIZADA!!");
            finPartida = true;
        }
    }
}
