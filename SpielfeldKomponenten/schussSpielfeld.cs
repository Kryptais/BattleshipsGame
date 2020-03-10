using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schiffeversenken
{
    public class schussSpielfeld : Spielfeld
    {
        public List<Koordinaten> GetUebrigeRandomSpielfeldTiles()
        {
            return SpielfeldTiles.Where(x => x.Teilbelegung == Teilbelegung.Leer && x.istTeilDiagonalVerfügbar).Select(x => x.koordinaten).ToList();
        }

        public List<Koordinaten> GetGetroffeneNachbarn()
        {
            List<SpielfeldTile> spielfeldTiles = new List<SpielfeldTile>();
            var trefferSammlung = SpielfeldTiles.Where(x => x.Teilbelegung == Teilbelegung.Getroffen);
            foreach(var treffer in trefferSammlung)
            {
                spielfeldTiles.AddRange(GetNachbarn(treffer.koordinaten).ToList());
            }
            return spielfeldTiles.Distinct().Where(x => x.Teilbelegung == Teilbelegung.Leer).Select(x => x.koordinaten).ToList();
        }
        public List<SpielfeldTile> GetNachbarn(Koordinaten koordinaten)
        {
            int reihe = koordinaten.Reihe;
            int spalte = koordinaten.Spalte;
            List<SpielfeldTile> spielfeldTiles = new List<SpielfeldTile>();
            if(spalte > 0)
            {
                spielfeldTiles.Add(SpielfeldTiles.At(reihe, spalte - 1));
            }
            if(reihe > 0)
            {
                spielfeldTiles.Add(SpielfeldTiles.At(reihe - 1, spalte));
            }
            if(reihe < 9)
            {
                spielfeldTiles.Add(SpielfeldTiles.At(reihe, spalte + 1));
            }
            return spielfeldTiles;
        }
    }
}
