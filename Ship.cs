using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schiffeversenken
{
   public class Ship
    {
        public int shiplenght;
        public string name;
        int[,] shipcoords;
        int counter = 0;
        int yarraycolumn = 0;
        int xarraycolumn = 1;
        int shipPartConditionArrayColumn = 2;
        bool shipISAlive = true;

        public Ship(int shiplenght)
        {
            this.shiplenght = shiplenght;
            set_Name();
            shipcoords = new int[shiplenght, 3];

        }
        public void set_Name()
        {
            switch (shiplenght)
            {
                case 1:
                    this.name = "submarine";
                    break;
                case 2:
                    this.name = "Destroyer";
                    break;
                case 3:
                    this.name = "Cruiser";
                    break;
                case 4:
                    this.name = "Battleship";
                    break;
                case 5:
                    this.name = "Carrier";
                    break;
            }
        }
        public void addshipcoord(int y, int x)
        {
            shipcoords[counter, yarraycolumn] = y;
            shipcoords[counter, xarraycolumn] = x;
            counter++;
        }
        public void updateshipcoord(int y , int x, int condition)
        {
            int index;
            for(int i = 0; i < shiplenght; i++)
            {
                    if(shipcoords[i,0].Equals(y) && shipcoords[i, 1].Equals(x))
                    {
                    index = i;
                    shipcoords[index, shipPartConditionArrayColumn] = condition;
                    }
            }
            checkShipstatus();
    
        }
        private void checkShipstatus()
        {
            int counterHitParts = 0;
            for(int i = 0; i< shiplenght; i++)
            {
                if (shipcoords[i, 2].Equals(1))
                {
                    counterHitParts++;
                }
            }
            if(counterHitParts == shiplenght)
            {
                shipISAlive = false;
            }
        }
    }
}
