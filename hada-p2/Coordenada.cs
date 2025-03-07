using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class Coordenada
    {
        private int _fila;
        private int _columna;

        public int Fila 
        {
            get { return _fila; }
            set
            {
                if (value < 0 && value > Game.tamTablero)
                {
                    throw new Exception();
                }
                else
                {
                    _fila = value;
                }
            }
        }
        public int Columna 
        {
            get { return _columna; }
            set 
            {
                if (value < 0 && value > Game.tamTablero)
                {
                    throw new Exception();
                }
                else 
                {
                    _columna = value;
                }
            }
        }

        /*
         * Coonstrucciones de la clase Coordenada:
            * constuctor cuando no se le pasa nada
            * inicializa Fila y Columna a 0
         */
        public Coordenada(){
            Fila = 0;
            Columna = 0;
        }

        /*
         * Coonstrucciones de la clase Coordenada:
            * constuctor se le pasan dos ints
            * inicializa Fila = fila y Columna = columna  
         */
        public Coordenada(int fila, int columna){
            entre0ytamTablero(fila, columna);
            Fila = fila;
            Columna = columna;
        }

        /*
         * Coonstrucciones de la clase Coordenada:
            * constuctor se le pasan dos strings
            * inicializa Fila = fila y Columna = columna  con parse
         */
        public Coordenada(string fila, string columna) {
            int f = Int32.Parse(fila);
            int c = Int32.Parse(columna);
            entre0ytamTablero(f, c);
            Fila = f;
            Columna = c;
        }

        /*
         * Coonstrucciones de copia de la clase Coordenada
            * constuctor se le pasan un objeto Coordenada
            * copia Fila y Columna
         */
        public Coordenada(Coordenada c){
            Fila = c.Fila;
            Columna = c.Columna;
        }

        /*
         * Funcion que comprueba si la coordenada esta dentro del tablero
         */
        public static void entre0ytamTablero(int x, int y) {
            if (x < 0 && x > Game.tamTablero && y < 0 && y > Game.tamTablero){
                throw new Exception();
            }
        }

        public void fila(int fila) 
        {
            if (fila >= 0 || fila <= Game.tamTablero) 
            {
                Fila = fila;
            }
        }

        public void columna(int columna)
        {
            if (columna >= 0 || columna <= Game.tamTablero)
            {
                Columna = columna;
            }
        }

        /*
         * Sobreescritura del metodo ToString(), para mostrar la informacion del barco actual
         */
        override
        public string ToString() {
            return "(" + Fila + "," + Columna + ")";
        }

        override
        public int GetHashCode() {
            return this.Fila.GetHashCode() ^ this.Columna.GetHashCode();
        }
        public bool Equals(Coordenada coordenada) {
            if (this.Fila == coordenada.Fila && this.Columna == coordenada.Columna) {
                return true;
            }
            return false;
        }
        override
        public bool Equals(object objeto) {
            if (objeto == null || GetType() != objeto.GetType())
            {
                return false;
            }

            Coordenada other = (Coordenada)objeto;
            return this.Fila == other.Fila && this.Columna == other.Columna;
        }

    }
}
