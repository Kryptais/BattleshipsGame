using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schiffeversenken
{
    public enum Teilbelegung
    {
        [Description("0")]
        Leer,

        [Description("B")]
        Battleship,

        [Description("C")]
        Cruiser,

        [Description("D")]
        Destroyer,

        [Description("S")]
        Submarine,

        [Description("A")]
        AircraftCarrier,

        [Description("X")]
        Getroffen,

        [Description("M")]
        Miss,
    }
    public enum SchussErgebnis
    {
        Miss,
        Hit
    }
}
