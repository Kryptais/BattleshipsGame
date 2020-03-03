using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schiffeversenken.Schiffe
{
    public class AircraftCarrier : Schiffe
    {
        public AircraftCarrier()
        {
            Name = "AircraftCarrier";
            Laenge = 5;
            Teilbelegung = Teilbelegung.AircraftCarrier;
        }
    }
}
