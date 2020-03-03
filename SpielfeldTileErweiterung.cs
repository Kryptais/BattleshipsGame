using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schiffeversenken
{
    public static class SpielfeldTileErweiterung
    {
        public static SpielfeldTile At(this List<SpielfeldTile> spielfeldTiles, int reihe, int spalte)
        {
            return spielfeldTiles.Where(x => x.koordinaten.Reihe == reihe && x.koordinaten.Spalte == spalte).First();
        }

        public static List<SpielfeldTile> Reichweite(this List<SpielfeldTile> spielfeldTiles, int startReihe, int startSpalte, int endReihe, int endSpalte)
        {
            return spielfeldTiles.Where(x => x.koordinaten.Reihe >= startReihe
                                        && x.koordinaten.Spalte >= startSpalte
                                        && x.koordinaten.Reihe <= endReihe
                                        && x.koordinaten.Spalte <= endSpalte).ToList();
        }
    }
}
