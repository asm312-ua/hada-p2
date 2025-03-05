using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class Coordenada
    {
        private int _fila;
        private int _columna;

        public int Fila 
        {
            get { return _fila; }
            set
            {
                if (value < 0 && value > 9)
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
                if (value < 0 && value > 9)
                {
                    throw new Exception();
                }
                else 
                {
                    _columna = value;
                }
            }
        }

        public Coordenada(){
            Fila = 0;
            Columna = 0;
        }
        public Coordenada(int fila, int columna){
            entre1y9(fila, columna);
            Fila = fila;
            Columna = columna;
        }

        public Coordenada(string fila, string columna) {
            int f = Int32.Parse(fila);
            int c = Int32.Parse(columna);
            entre1y9(f, c);
            Fila = f;
            Columna = c;
        }

        public Coordenada(Coordenada c){
            Fila = c.Fila;
            Columna = c.Columna;
        }

        public static void entre1y9(int x, int y) {
            if (x < 0 && x > 9 && y < 0 && y > 9){
                throw new Exception();
            }
        }

        public void fila(int fila) 
        {
            if (fila >= 0 || fila < 10) 
            {
                Fila = fila;
            }
        }

        public void columna(int columna)
        {
            if (columna >= 0 || columna < 10)
            {
                Columna = columna;
            }
        }

        override
        public string ToString() {
            return "(" + Fila + "," + Columna + ")";
        }

        override
        public int GetHashCode() {
            return this.Fila.GetHashCode() ^ this.Columna.GetHashCode();
        }
        public bool Equals(Coordenada coordenada) {
            if (Fila == coordenada.Fila && Columna == coordenada.Columna) {
                return true;
            }
            return false;
        }
        override
        public bool Equals(object objeto) {
            return false;
        }

    }
}
