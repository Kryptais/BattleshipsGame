using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schiffeversenken.Schiffe
{
    public class Submarine : Schiffe
    {
        public Submarine()
        {
            Name = "Submarine";
            Laenge = 2;
            Teilbelegung = Teilbelegung.Submarine;
        }

    }
}
