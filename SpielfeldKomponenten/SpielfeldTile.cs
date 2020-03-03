using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schiffeversenken
{
    public class SpielfeldTile
    {
        public Koordinaten koordinaten { get; set; }
        public Teilbelegung Teilbelegung { get; set; }


        public SpielfeldTile(int reihe, int spalte)
        {
            koordinaten = new Koordinaten(reihe, spalte);
            Teilbelegung = Teilbelegung.Leer;
        }
        public string SchiffsteilStatus
        {
            get
            {
                return Teilbelegung.GetAttributeOfType<DescriptionAttribute>().Description;
            }

        }
        public bool istBesetzt
        {
            get
            {
                return Teilbelegung == Teilbelegung.Battleship
                    || Teilbelegung == Teilbelegung.AircraftCarrier
                    || Teilbelegung == Teilbelegung.Cruiser
                    || Teilbelegung == Teilbelegung.Destroyer
                    || Teilbelegung == Teilbelegung.Submarine;
            }
        }
        public bool istTeilDiagonalVerfügbar
        {
            get
            {
                return (koordinaten.Reihe % 2 == 0 && koordinaten.Spalte % 2 == 0)
                    || (koordinaten.Reihe % 2 == 1 && koordinaten.Spalte % 2 == 1);
            }
        }
    }
}
