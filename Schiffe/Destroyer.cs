using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schiffeversenken.Schiffe
{
    public class Destroyer : Schiffe
    {
        public Destroyer()
        {
            Name = "Destroyer";
            Laenge = 2;
            Teilbelegung = Teilbelegung.Destroyer;
        }
    }
}
