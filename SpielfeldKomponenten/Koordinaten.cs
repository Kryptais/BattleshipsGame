using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schiffeversenken
{
    public class Koordinaten
    {
        public int Reihe { get; set; }
        public int Spalte { get; set; }

        public Koordinaten(int reihe, int spalte)
        {
            Reihe = reihe;
            Spalte = spalte;
        }
        public string convertKoordinatenToTileName()
        {
            int spalte2 = Spalte +1 ;
            int reihe2 = Reihe;
            string[] alphabet = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            string name = alphabet[reihe2] + spalte2;
            return name;

        }
    }
}
