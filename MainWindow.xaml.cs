using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Random random = new Random();
        Spiel spiel;
        //GameField PlayerPlacement;
        //GameField Player;
        //GameField AI;
        //bool shipsPlaced = false;
        //public PlayerVsComputer playerVsComputer;
        //bool PlayerhasSHOT = false;
        public MainWindow()
        {
            InitializeComponent();



            //PlayerPlacement = new GameField(10, random, "Player");
            //PlayerPlacement.drawOwnGameField();
            //(PlaceShips.Content as Grid).Children.Add(PlayerPlacement.playfield);
            //Grid.SetColumn(PlayerPlacement.playfield, 2);
            //Grid.SetRow(PlayerPlacement.playfield, 1);

            //if (PlayerhasSHOT == true)
            //{
            //    //Thread.Sleep(1000);
            //    playerVsComputer.AIShots();
            //    PlayerhasSHOT = false;
            //}

        }
        private void PlacingPhase_Clicked(object sender, RoutedEventArgs e)
        {
            tabcontrol.SelectedItem = PlaceShips;
            spiel = new Spiel();
        }

        private void Settings_Clicked(object sender, RoutedEventArgs e)
        {
            tabcontrol.SelectedItem = Settings;
        }

        private void ExitClicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Start_Game(object sender, RoutedEventArgs e)
        {
            tabcontrol.SelectedItem = Playfield;
            spiel.Spieler1.ZeichneSpielfeld(SchiffFeld);
            spiel.Spieler2.ZeichneSpielfeld(Schussfeld);
            //AI = new GameField(10, random, "Computer");
            //AI.generateRandomPlayfield();
            //AI.drawEnemyGameField();
            //(Playfield.Content as Grid).Children.Add(AI.playfield);
            //Grid.SetColumn(AI.playfield, 3);
            //Grid.SetRow(AI.playfield, 3);

            //Player = new GameField(10, random, "Player");
            //Player.intArrayField = PlayerPlacement.intArrayField;
            //Player.drawOwnGameField();
            //(Playfield.Content as Grid).Children.Add(Player.playfield);
            //Grid.SetColumn(Player.playfield, 1);
            //Grid.SetRow(Player.playfield, 3);
            //playerVsComputer = new PlayerVsComputer(5, 5, Player, AI, random);
            //playerVsComputer.PLayerhasShot += PLayerShoot;//new PlayerVsComputer.MyEventHandler(PLayerShoot);

        }
        //public void PLayerShoot()
        //{
        //    PlayerhasSHOT = true;

        //    int[,] test = playerVsComputer.AIShots();
        //    int x = test[0, 0];
        //    int y = test[0, 1];
        //    var myWin = (MainWindow)Application.Current.MainWindow;
        //    foreach (Schiff s in myWin.playerVsComputer.Player.shipList)
        //    {
        //        s.updateshipcoord(y, x);

        //    }
        //}
        private void Place_Ships_Random(object sender, RoutedEventArgs e)
        {
            spiel.Spieler1.bereinigeSpielfeld();
            spiel.Spieler1.platziereSchiffe();
            spiel.Spieler1.ZeichneSpielfeld(TestPLayerSpielfeld);
            //(PlaceShips.Content as Grid).Children.Clear();
            //PlayerPlacement = new GameField(10, random, "Player");
            //PlayerPlacement.generateRandomPlayfield();
            //PlayerPlacement.drawOwnGameField();
            //(PlaceShips.Content as Grid).Children.Add(PlayerPlacement.playfield);
            //Grid.SetColumn(PlayerPlacement.playfield, 2);
            //Grid.SetRow(PlayerPlacement.playfield, 1);


            FinishPlacement.IsEnabled = true;
        }

    }

}

