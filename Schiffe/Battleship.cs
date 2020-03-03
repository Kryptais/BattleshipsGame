using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schiffeversenken.Schiffe
{
    public class Battleship : Schiffe
    {
        public Battleship()
        {
            Name = "Battleship";
            Laenge = 4;
            Teilbelegung = Teilbelegung.Battleship;
        }
    }
}
