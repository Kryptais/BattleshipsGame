using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Schiffeversenken
{
   public class Schiff
    {
        public int shiplenght;
        public string name;
        public int[,] shipcoords;
        int counter = 0;
        int hitCounter = 0;
        bool alreadynotifyed = false;
        string playerName = "test";

        public Schiff(int shiplenght, string playername)
        {
            this.shiplenght = shiplenght;
            set_Name();
            shipcoords = new int[shiplenght, 3];
            this.playerName = playername;
        }
        public void set_Name()
        {
            switch (shiplenght)
            {
                case 1:
                    this.name = "Submarine";
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
            shipcoords[counter, 0] = y;
            shipcoords[counter, 1] = x;
            counter++;
        }
        public void updateshipcoord(int x, int y)
        {
            for (int i = 0; i < shiplenght; i++)
            {
                if (shipcoords[i, 0] == y && shipcoords[i, 1] == x)
                {
                    shipcoords[i, 2] = 1;
                    hitCounter++;
                }
            }

            if (hitCounter == shiplenght)
            {
                notifyGUI();
            }

        }
        private void notifyGUI()
        {
            if(alreadynotifyed == false)
            {
                var myWin = (MainWindow)Application.Current.MainWindow;
                myWin.EventBox.Text += name + " von " + playerName + " wurde versenkt. \n";
                alreadynotifyed = true;
            }

        }
    }
}
