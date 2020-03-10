using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schiffeversenken
{
    public class Spielfeld
    {
        public List<SpielfeldTile> SpielfeldTiles { get; set; }
        public Spielfeld()
        {
            SpielfeldTiles = new List<SpielfeldTile>();
            for (int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    SpielfeldTiles.Add(new SpielfeldTile(i, j));
                }
            }
        }
    }
}
