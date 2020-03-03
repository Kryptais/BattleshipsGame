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
            for (int i = 1; i <= 10; i++)
            {
                for(int j = 1; j <= 10; j++)
                {
                    SpielfeldTiles.Add(new SpielfeldTile(i, j));
                }
            }
        }
    }
}
