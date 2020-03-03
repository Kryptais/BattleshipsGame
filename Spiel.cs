using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schiffeversenken
{
    public class Spiel
    {
        public Spieler Spieler1 { get; set; }
        public Spieler Spieler2 { get; set; }

        public Spiel()
        {
            Spieler1 = new Spieler("Simon");
            Spieler2 = new Spieler("AI");

            Spieler2.platziereSchiffe();


        }
    }
}
