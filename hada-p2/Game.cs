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
        public static int tamTableroMax = 9;// El tamaño maximo es ampliable pero el numero maximo de barcos estara definido por la siguiente regla 56 >= 3 + 0.3*tamTablero es decir 176x176
        public static int tamTableroMin = 4;// Preferiblemente 4, ya que en caso de ser menor seria posible que al intentar buscarle una coordenada en el tablero el bucle sea infinito.
        private Random rnd = new Random();


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

        /* Funcion que se encarga de gestionar el bucle del juego
         * se encarga inicializar tablero quien crea los barcos
         * y gestiona las entradas de la consola
         */
        private void gameLoop() 
        {
            List<string> nombres = new List<string>()
            {
                "THOR","LOKI","ODIN","FREYA","BALDER","HEIMDALL","FREYR","TYR","VIDAR","NJORD","ULLR","FORSETI","BRAGI","HELA"
                ,"FENRIR","JORMUNGANDR","SURTR","FREYJA","SIGYN","SKADI","SIF","IDUN","NANNA","FRIGG","HEIDRUN","GULLINBURSTI"
                ,"SLEIPNIR","GRANI","HOENIR","MIMIR","RAN","AEGIR","NJOERD","RA","ANUBIS","ISIS","SERKET","HORUS","SETH","BASTET"
                ,"THOTH","HATHOR","AMON","NEPHTHYS","KUKULKAN","ITZAMNA","IXCHEL","HUNABKU","KINICH","KUK","YUM","KAAX","BOLON"
                ,"CHAC","IXBALANKE","CABRAKAN"
            };
            int tamMaxTablero = (int)Math.Truncate((nombres.Count - 3) / 0.3);// Indica el tamaño maximo del tablero segun la lista actual

            do
            {
                if (tamTablero < tamTableroMin || tamTablero > tamTableroMax)
                {
                    Console.WriteLine($"Introduce un tamaño de tablero entre {tamTableroMin} y {tamTableroMax}");
                    if (!Int32.TryParse(Console.ReadLine(), out tamTablero))
                    {
                        tamTablero = 0;
                    }
                    else if (tamTablero > tamMaxTablero)// si es mayor que el numero maximo de barcos en la lista
                    {
                        Console.WriteLine($"El numero maximo de barcos es: {nombres.Count}, para ese tablero serian necesarios: {(tamTablero) * 0.3 + 3 } Barcos \nAñade elementos a la lista o cambia las dimensiones del tablero\nEl tamaño maximo con la lista actual es: {tamMaxTablero}");
                        tamTablero = 0;
                    }
                }
            } while (tamTablero < tamTableroMin || tamTablero > tamTableroMax);

            List<Barco> barcos = new List<Barco>();

            int i = 1;
            while (i<=numeroRandomBarcos())//i<=3 represena la cantidad de barcos que se van a crear
            {
                int fila = rnd.Next(0, tamTablero-i+1);
                int columna = rnd.Next(0, tamTablero-i+1);
                char direccion;
                if (rnd.Next(0, 2) == 1)
                {
                    direccion = 'v';
                }
                else
                {
                    direccion = 'h';
                }
                if (ColocarBarco(new Barco(nombres[i-1], longitudRandomBarco(), direccion, new Coordenada(fila, columna)), barcos))
                {
                    i++;
                }
            }

            Tablero tablero = new Tablero(tamTablero, barcos);
            
            int filaPedida;
            int columnaPedida;
            Coordenada coordenadaPedida = new Coordenada();
            tablero.eventoFinPartida += cuandoEventoFinPartida;

            while (!finPartida) 
            {
                Console.WriteLine(tablero);
                Console.WriteLine("Introduce la coordenada a la que disparar FILA,COLUMNA ('S' para salir)");
                string respuesta = Console.ReadLine();
                if(respuesta == "s" || respuesta == "S")
                {
                    cuandoEventoFinPartida(this, EventArgs.Empty);
                }
                else
                {
                    var respuestaPartida = respuesta.Split(',');
                    while ((respuesta != "s" || respuesta != "S") && respuestaPartida.Length != 2 || !Int32.TryParse(respuestaPartida[0], out filaPedida) || !Int32.TryParse(respuestaPartida[1], out columnaPedida)) //comprueba si las coordenadas se han colocado como se pide
                    {

                        Console.WriteLine("Introduce la coordenada a la que disparar FILA,COLUMNA ('S' para salir)");
                        respuesta = Console.ReadLine();
                        if (respuesta == "s" || respuesta == "S") { cuandoEventoFinPartida(this, EventArgs.Empty); }
                        respuestaPartida = respuesta.Split(',');
                    }
                    Console.Clear();
                    coordenadaPedida.Columna = columnaPedida;
                    coordenadaPedida.Fila = filaPedida;
                    tablero.Disparar(coordenadaPedida);
                    if (finPartida) { Console.WriteLine(tablero); }
                }
            }
        }

        /*
         *  Funcion que se encarga de gestionar el evento EventoFinPartida 
         *  el cual termina el blucle del juego
         */
        private void cuandoEventoFinPartida(object algo,EventArgs finDePartida) { //Si cambia el delegado del evento eventoFinPartida, cambiar parámetros
            Console.WriteLine("PARTIDA FINALIZADA!!");
            finPartida = true;
        }

        //Funcion para crear el numero de barcos aleatorios
        private int numeroRandomBarcos()
        {
            if (tamTablero <= 4)
            {
                return 3;
            }
            else
            {
                return (int)Math.Round(3 + tamTablero * 0.3);
            }
        }

        //Funcion para crear la longitud de un barco de fomra aleatoria
        private int longitudRandomBarco()
        {
            return rnd.Next(0, (int)Math.Round(2 + tamTablero * 0.2));
        }
    }
}
