using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schiffeversenken.Schiffe
{
    public class Cruiser : Schiffe
    {
        public Cruiser()
        {
            Name = "Cruiser";
            Laenge = 3;
            Teilbelegung = Teilbelegung.Cruiser;
        }
    }
}
