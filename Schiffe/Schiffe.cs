using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schiffeversenken.Schiffe
{
    public class Schiffe
    {
        public string Name { get; set; }
        public int Laenge { get; set; }
        public int Treffer { get; set; }
        public Teilbelegung Teilbelegung { get; set; }
        public bool istGesunken
        {
            get
            {
                return Treffer >= Laenge;
            }
        }
    }
}
