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

    }
}
