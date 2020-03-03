using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Schiffeversenken
{
   public class PlayerVsComputer
    {
        List<string> shots = new List<string>();
        int shipCountAI = 0;
        int shipCountPlayer = 0;
        public int shotsFiredAI = 0;
        public  int shotsFiredPlayer = 0;
        Random random;
        public GameField AI;
        public GameField Player;
        public delegate void MyEventHandler();
        public event MyEventHandler PLayerhasShot;
   


        public PlayerVsComputer(int shipCountAI, int shipCountPlayer,GameField Player, GameField AI, Random random)
        {
            this.shipCountAI = shipCountAI;
            this.shipCountPlayer = shipCountPlayer;
            this.random = random;
            this.AI = AI;
            this.Player = Player;
            for (int i = 0; i < 100; i++)
            {
                shots.Add(i+"");
            }
        }
        public void startGame()
        {
           
        }
        public void PlayerSHots()
        {
            
            AI.playfield.PlayGrid.Children.Clear();
            AI.drawGameFieldNoButtons();
            shotsFiredPlayer++;
            PLayerhasShot();
            //foreach (Ship s in Player.shipList)
            //{
            //    for (int i = 0; i < s.shiplenght; i++)
            //    {
            //        if (s.shipcoords[i, 0] == y && s.shipcoords[i, 1] == x)
            //        {
            //            s.updateshipcoord(y, x, 1);
            //        }
            //    }

            //}

        }
        public  int[,] AIShots()
        {
            // 0 = water
            //1 = ship
            //2 = hit but no ship
            //3 = hit ship
            int x = 0;
            int y = 0;
            bool result = false;
            while (result == false)
            {
                string coord = randomcoordinate();
                char[] test = coord.ToCharArray();
                y = int.Parse(test[0].ToString());
                x = int.Parse(test[1].ToString());
                if (int.Parse(coord) < 10)
                {
                    coord = int.Parse(coord).ToString();

                }
                shots.Remove(coord);
                result = true;
                //foreach (Ship s in Player.shipList)
                //{
                //    for (int i = 0; i < s.shiplenght; i++)
                //    {
                //        if (s.shipcoords[i, 0] == y && s.shipcoords[i, 1] == x)
                //        {
                //            s.updateshipcoord(y, x);
                //        }
                //    }

                //}

            }
            if (Player.intArrayField[y, x] == 0)
                {
                    Player.intArrayField[y, x] = 2;
                }
                if (Player.intArrayField[y, x] == 1)
                {
                    Player.intArrayField[y, x] = 3;
                }
                Player.playfield.PlayGrid.Children.Clear();
                Player.drawOwnGameField();
                shotsFiredAI++;
            AI.playfield.PlayGrid.Children.Clear();
            AI.drawEnemyGameField();
            int[,] shotcoords = new int[1, 2];
            shotcoords[0, 0] = y;
            shotcoords[0, 1] = x;
            return shotcoords;

        }
        public string randomcoordinate()
        {
            
            string result = shots[random.Next(0, shots.Count)];

            if (int.Parse(result) < 10)
            {
                result = 0 + result;
            }
            return result;
        }
    }
}
