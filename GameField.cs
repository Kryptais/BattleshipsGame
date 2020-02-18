using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Schiffeversenken
{
    public class GameField
    {
        public int fieldlenght;
        public Random random;
        public int[,] intArrayField { get; set; }
        public PlayField playfield = new PlayField();
        public List<Ship> shipList = new List<Ship>();

        public GameField(int fieldlenght, Random random)
        {
            this.fieldlenght = fieldlenght;
            intArrayField = new int[fieldlenght, fieldlenght];
            this.random = random;
            for (int i = 0; i<5; i++)
            {
                Ship ship = new Ship(i+1);
                shipList.Add(ship);
            }
        }
        public void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int x = int.Parse(button.Tag.ToString().Split('/')[0]);
            int y = int.Parse(button.Tag.ToString().Split('/')[1]);
            int value = 0;
            switch (checkCoordinateButton(sender))
            {
                case 0:
                    this.intArrayField[x, y] = 2;
                    value = 0;
                    break;
                case 1:
                    this.intArrayField[x, y] = 3;
                    value = 1;
                    break;
            }
            playfield.PlayGrid.Children.Remove(button);
            // Player2.PlayGrid.Children.Remove(button);
            Rectangle rectangle = new Rectangle();
            if (value == 0)
            {
                rectangle.Fill = Brushes.Gray;
            }
            if (value == 1)
            {
                rectangle.Fill = Brushes.Red;
            }
            rectangle.Margin = new Thickness(2);
            playfield.PlayGrid.Children.Add(rectangle);

            Grid.SetColumn(rectangle, y + 1);
            Grid.SetRow(rectangle, x + 1);

            var myWin = (MainWindow)Application.Current.MainWindow;
            myWin.playerVsComputer.PlayerSHots();

        }

        private int checkCoordinateButton(object sender)
        {
            Button button = sender as Button;
            if (this.getIntValue(int.Parse(button.Tag.ToString().Split('/')[0]), int.Parse(button.Tag.ToString().Split('/')[1])) == 0)
            {
                return 0;
            }
            else if (this.getIntValue(int.Parse(button.Tag.ToString().Split('/')[0]), int.Parse(button.Tag.ToString().Split('/')[1])) == 1)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
        private int checkCoordinate(GameField gamefield, int y, int x)
        {
            if (gamefield.intArrayField[y, x] == 0)
            {
                return 0;
            }
            else if (gamefield.intArrayField[y, x] == 1)
            {
                return 1;
            }
            else if (gamefield.intArrayField[y, x] == 2)
            {
                return 2;
            }
            else if (gamefield.intArrayField[y, x] == 3)
            {
                return 3;
            }
            else
            {
                return 4;
            }
             
        }
        public void drawEnemyGameField()
        {
            string[] alphabet = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
                for (int i = 1; i < 11; i++)
                {
                    for (int j = 1; j < 11; j++)
                    {
                        // 0 = water
                        //1 = ship
                        //2 = hit but no ship
                        //3 = hit ship
                        switch (checkCoordinate(this, j - 1, i - 1))
                        {
                            case 0:
                                Button button = new Button();
                                playfield.PlayGrid.Children.Add(button);
                                Grid.SetColumn(button, i);
                                Grid.SetRow(button, j);
                                button.Margin = new Thickness(2);
                                button.Background = Brushes.Blue;
                                button.Click += Button_Click;
                                string name = (j - 1) + "/" + (i - 1);
                                button.Tag = name;
                                break;
                            case 1:
                                Button button2 = new Button();
                                playfield.PlayGrid.Children.Add(button2);
                                Grid.SetColumn(button2, i);
                                Grid.SetRow(button2, j);
                                button2.Margin = new Thickness(2);
                                button2.Background = Brushes.Blue;
                                button2.Click += Button_Click;
                                string name2 = (j - 1) + "/" + (i - 1);
                                button2.Tag = name2;
                                break;
                            case 2:
                                Rectangle rectangle = new Rectangle();
                                playfield.PlayGrid.Children.Add(rectangle);
                                Grid.SetColumn(rectangle, i);
                                Grid.SetRow(rectangle, j);
                                rectangle.Margin = new Thickness(2);
                                rectangle.Fill = Brushes.Black;
                                break;
                            case 3:
                                Rectangle rectangle2 = new Rectangle();
                                playfield.PlayGrid.Children.Add(rectangle2);
                                Grid.SetColumn(rectangle2, i);
                                Grid.SetRow(rectangle2, j);
                                rectangle2.Margin = new Thickness(2);
                                rectangle2.Fill = Brushes.Red;
                                break;

                        }
                    }

                }
            for (int i = 0; i < 10; i++)
            {
                Label label = new Label();
                label.HorizontalAlignment = HorizontalAlignment.Center;
                label.VerticalAlignment = VerticalAlignment.Center;
                playfield.PlayGrid.Children.Add(label);
                label.Content = alphabet[i];
                Grid.SetColumn(label, i+1);
                Grid.SetRow(label, 0);
                label.FontSize = 20;

                Label label2 = new Label();
                label2.HorizontalAlignment = HorizontalAlignment.Center;
                label2.VerticalAlignment = VerticalAlignment.Center;
                playfield.PlayGrid.Children.Add(label2);
                label2.Content = i + 1 + "";
                Grid.SetColumn(label2, 0);
                Grid.SetRow(label2, i+1);
                label2.FontSize = 20;
            }
        }
        public void drawOwnGameField()
        {
            // 0 = water
            //1 = ship
            //2 = hit but no ship
            //3 = hit ship
            string[] alphabet = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            for (int i = 1; i < 11; i++)
            {
                for (int j = 1; j < 11; j++)
                {

                    Rectangle rectangle = new Rectangle();
                    playfield.PlayGrid.Children.Add(rectangle);
                    Grid.SetColumn(rectangle, i);
                    Grid.SetRow(rectangle, j);
                    rectangle.Margin = new Thickness(2);
                    if (this.intArrayField[j - 1, i - 1] == 0)
                    {
                        rectangle.Fill = System.Windows.Media.Brushes.Blue;
                    }
                    else if (this.intArrayField[j - 1, i - 1] == 1)
                    {
                        rectangle.Fill = System.Windows.Media.Brushes.Gray;
                    }
                    else if (this.intArrayField[j - 1, i - 1] == 2)
                    {
                        rectangle.Fill = System.Windows.Media.Brushes.Black;
                    }
                    else if (this.intArrayField[j - 1, i - 1] == 3)
                    {
                        rectangle.Fill = System.Windows.Media.Brushes.Red;
                    }
                }

            }
            for (int i = 0; i < 10; i++)
            {
                Label label = new Label();
                label.HorizontalAlignment = HorizontalAlignment.Center;
                label.VerticalAlignment = VerticalAlignment.Center;
                label.Content = alphabet[i];
                playfield.PlayGrid.Children.Add(label);
                Grid.SetColumn(label, i+1);
                Grid.SetRow(label, 0);
                label.FontSize = 20;

                Label label2 = new Label();
                label2.HorizontalAlignment = HorizontalAlignment.Center;
                label2.VerticalAlignment = VerticalAlignment.Center;
                label2.Content = i+1+"";
                playfield.PlayGrid.Children.Add(label2);
                Grid.SetColumn(label2, 0);
                Grid.SetRow(label2, i+1);
                label2.FontSize = 20;
            }
        }
        public void drawGameFieldNoButtons()
        {
            // 0 = water
            //1 = ship
            //2 = hit but no ship
            //3 = hit ship
            string[] alphabet = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            for (int i = 1; i < 11; i++)
            {
                for (int j = 1; j < 11; j++)
                {

                    Rectangle rectangle = new Rectangle();
                    playfield.PlayGrid.Children.Add(rectangle);
                    Grid.SetColumn(rectangle, i);
                    Grid.SetRow(rectangle, j);
                    rectangle.Margin = new Thickness(2);
                    if (this.intArrayField[j - 1, i - 1] == 0)
                    {
                        rectangle.Fill = System.Windows.Media.Brushes.Blue;
                    }
                    else if (this.intArrayField[j - 1, i - 1] == 1)
                    {
                        rectangle.Fill = System.Windows.Media.Brushes.Blue;
                    }
                    else if (this.intArrayField[j - 1, i - 1] == 2)
                    {
                        rectangle.Fill = System.Windows.Media.Brushes.Black;
                    }
                    else if (this.intArrayField[j - 1, i - 1] == 3)
                    {
                        rectangle.Fill = System.Windows.Media.Brushes.Red;
                    }
                }

            }
            for (int i = 0; i < 10; i++)
            {
                Label label = new Label();
                label.HorizontalAlignment = HorizontalAlignment.Center;
                label.VerticalAlignment = VerticalAlignment.Center;
                label.Content = alphabet[i];
                playfield.PlayGrid.Children.Add(label);
                Grid.SetColumn(label, i + 1);
                Grid.SetRow(label, 0);
                label.FontSize = 20;

                Label label2 = new Label();
                label2.HorizontalAlignment = HorizontalAlignment.Center;
                label2.VerticalAlignment = VerticalAlignment.Center;
                label2.Content = i + 1 + "";
                playfield.PlayGrid.Children.Add(label2);
                Grid.SetColumn(label2, 0);
                Grid.SetRow(label2, i + 1);
                label2.FontSize = 20;
            }
        }
        public void generateRandomPlayfield()
        {
            for(int i = 0; i < 5; i++)
            {
                placeShipRandom(shipList[i]);
            }
        }
        private bool placeShipRandom(Ship ship)
        {
            bool result = false;
            int resultingDirection = 4;
            int[,] shipStern = new int[1, 2];
            while (result == false)
            {
                int shipDirection = generateRandomDirection(random);
                shipStern = generateRandomCoordinate(random);
                switch (shipDirection)
                {
                    case 0:
                        result = checkNDirection(shipStern, ship.shiplenght);
                        if (result == true)
                        {
                            resultingDirection = 0;
                        }
                        break;
                    case 1:
                        result = checkEDirection(shipStern, ship.shiplenght);
                        if (result == true)
                        {
                            resultingDirection = 1;
                        }
                        break;
                    case 2:
                        result = checkSDirection(shipStern, ship.shiplenght);
                        if (result == true)
                        {
                            resultingDirection = 2;
                        }
                        break;
                    case 3:
                        result = checkWDirection(shipStern, ship.shiplenght);
                        if (result == true)
                        {
                            resultingDirection = 3;
                        }
                        break;
                    default:
                        break;
                }
            }
            switch (resultingDirection)
            {
                case 0:
                    placeShipInDirNorth(shipStern, ship.shiplenght);
                    break;
                case 1:
                    placeShipInDirEast(shipStern, ship.shiplenght);
                    break;
                case 2:
                    placeShipInDirSouth(shipStern, ship.shiplenght);
                    break;
                case 3:
                    placeShipInDirWest(shipStern, ship.shiplenght);
                    break;
                case 4:
                    MessageBox.Show("Fehler beim platzieren der Schiffe");
                    break;
            }


            return result;
        }
        private void placeShipInDirNorth(int[,] shipStern, int shiplenght)
        {
            for (int i = 0; i < shiplenght; i++)
            {
                intArrayField[shipStern[0, 0] - i, shipStern[0, 1]] = 1;
            }
        }
        private void placeShipInDirEast(int[,] shipStern, int shiplenght)
        {
            for (int i = 0; i < shiplenght; i++)
            {
                intArrayField[shipStern[0, 0], shipStern[0, 1] + i] = 1;
            }
        }
        private void placeShipInDirSouth(int[,] shipStern, int shiplenght)
        {
            for (int i = 0; i < shiplenght; i++)
            {
                intArrayField[shipStern[0, 0] + i, shipStern[0, 1]] = 1;
            }
        }
        private void placeShipInDirWest(int[,] shipStern, int shiplenght)
        {
            for (int i = 0; i < shiplenght; i++)
            {
                intArrayField[shipStern[0, 0], shipStern[0, 1] - i] = 1;
            }
        }
        private bool checkNDirection(int[,] shipStern, int shiplenght)
        {
            bool checker = true;
            int counter = 0;


            for (int i = 0; i < shipStern[0, 0] - 1; i++)
            {
                if (intArrayField[(shipStern[0, 0] - i), (shipStern[0, 1])].Equals(1))
                {
                    checker = false;
                    break;
                }
                else
                {
                    counter++;
                    if (counter == shiplenght)
                    {
                        break;
                    }
                }
            }
            if (checker == true && counter == shiplenght)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        private bool checkEDirection(int[,] shipStern, int shiplenght)
        {
            bool checker = true;
            int counter = 0;
            for (int i = 0; i < fieldlenght - shipStern[0, 1] - 1  ; i++)
            {
                if (intArrayField[(shipStern[0, 0]), (shipStern[0, 1] + i)].Equals(1))
                {
                    checker = false;
                    break;
                }
                else
                {
                    counter++;
                    if (counter == shiplenght)
                    {
                        break;
                    }
                }
            }
            if (checker == true && counter == shiplenght)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool checkSDirection(int[,] shipStern, int shiplenght)
        {
            bool checker = true;
            int counter = 0;


            for (int i = 0; i < fieldlenght - shipStern[0, 0] - 1; i++)
            {
                if (intArrayField[(shipStern[0, 0] + i), (shipStern[0, 1])].Equals(1))
                {
                    checker = false;
                    break;
                }
                else
                {
                    counter++;
                    if (counter == shiplenght)
                    {
                        break;
                    }
                }
            }
            if (checker == true && counter == shiplenght)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool checkWDirection(int[,] shipStern, int shiplenght)
        {
            bool checker = true;
            int counter = 0;


            for (int i = 0; i < shipStern[0, 1] - 1 ; i++)
            {
                if (intArrayField[(shipStern[0, 0]), (shipStern[0, 1] - 1)].Equals(1))
                {
                    checker = false;
                    break;
                }
                else
                {
                    counter++;
                    if (counter == shiplenght)
                    {
                        break;
                    }
                }
            }
            if (checker == true && counter == shiplenght)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private int generateRandomDirection(Random random)
        {
            int direction = random.Next(0, 3);
            return direction;
        }
        private int[,] generateRandomCoordinate(Random random)
        {
            int[,] shipStern = new int[1, 2];
            shipStern[0, 0] = generateRandomXCoordinate(random);
            shipStern[0, 1] = generateRandomYCoordinate(random);
            return shipStern;
        }
        private int generateRandomXCoordinate(Random random)
        {
            int xcoord = random.Next(0, fieldlenght - 1);
            return xcoord;
        }
        private int generateRandomYCoordinate(Random random)
        {
            int ycoord = random.Next(0, fieldlenght - 1);
            return ycoord;
        }
        public string printArray()
        {
            string result = null;
            int counter = 0;
            for (int i = 0; i < fieldlenght; i++)
            {
                for (int j = 0; j < fieldlenght; j++)
                {
                    result += intArrayField[i, j];
                    if (counter % 10 == 9)
                    {
                        result += Environment.NewLine;
                    }
                    counter++;
                }

            }
            return result;
        }
        public int getIntValue(int y, int x)
        {
            int result = intArrayField[y, x];
            return result;
        }
    }
}