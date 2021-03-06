﻿using System;
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
        public MainWindow()
        {
            InitializeComponent();
            spiel = new Spiel(this);
            foreach(Canvas canvas in SchussFeld.spielbaresSpielfeld.Children)
            {
                canvas.MouseDown += Canvas_MouseDown;
            }
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Canvas canvas = sender as Canvas;
            string name = canvas.Name;
            //string TileStatus = spiel.Spieler2.Spielfeld.SpielfeldTiles.Find(x => x.name == name).SchiffsteilStatus;
            //if (TileStatus == "0")
            //{
            //    canvas.Background = Brushes.Gray;
            //}
            //else if (TileStatus == "B"
            //      || TileStatus == "C"
            //      || TileStatus == "A"
            //      || TileStatus == "S"
            //      || TileStatus == "D")
            //{
            //    canvas.Background = Brushes.Red;
            //}
            canvas.MouseDown -= Canvas_MouseDown;
            spiel.koordinaten = convertCanvasNameToKoordinaten(name);
            spiel.currentState = GameState.PlayerHasShot;
            spiel.Update();
        }

        public Koordinaten convertCanvasNameToKoordinaten(string name)
        {
            String[] alphabet = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
                string test = name.Substring(0, 1);
                int reihe = Array.IndexOf(alphabet, test);
            string test2 = "";
            if(name.Length == 2)
            {
                 test2 = name.Substring(1, 1);
            }
            else if(name.Length == 3)
            {
                 test2 = name.Substring(1, 2);
            }
                int spalte = (int.Parse(test2)-1);

                Koordinaten result = new Koordinaten(reihe, spalte);
                return result;
        }
        private void PlacingPhase_Clicked(object sender, RoutedEventArgs e)
        {
            tabcontrol.SelectedItem = PlaceShips;

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
            //spiel.Spieler1.ZeichneSpielfeldNoButton(SchiffFeld);
            SchiffFeld.updateGUI(spiel.Spieler1.Spielfeld);
            spiel.currentState = GameState.PlayerTurn;
            spiel.Update();
        }
        private void Place_Ships_Random(object sender, RoutedEventArgs e)
        {
            spiel.Spieler2.bereinigeSpielfeld();
            spiel.Spieler2.platziereSchiffe();
            spiel.Spieler2.ZeichneSpielfeldVonGegner(SchussFeld);

            spiel.Spieler1.bereinigeSpielfeld();
            spiel.Spieler1.platziereSchiffe();
            spiel.Spieler1.ZeichneSpielfeld(TestPLayerSpielfeld);
            FinishPlacement.IsEnabled = true;
        }

        private void ExitAfterGame_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            ClearGame();
            tabcontrol.SelectedItem = PlaceShips;
        }

        private void BackToMenue_Click(object sender, RoutedEventArgs e)
        {
            tabcontrol.SelectedItem = MainMenu;
        }
        public void ClearGame()
        {
            spiel = null;
            spiel = new Spiel(this);
            EventBox.Text = "";
            foreach (Canvas canvas in SchussFeld.spielbaresSpielfeld.Children)
            {
                canvas.MouseDown += Canvas_MouseDown;
                canvas.Children.Clear();
            }
            foreach (Canvas canvas in TestPLayerSpielfeld.spielbaresSpielfeld.Children)
            {
                canvas.Background = Brushes.Blue;
                canvas.MouseDown += Canvas_MouseDown;
            }
            foreach (Canvas canvas in SchiffFeld.spielbaresSpielfeld.Children)
            {
                canvas.Children.Clear();
                canvas.Background = Brushes.Blue;
            }
            FinishPlacement.IsEnabled = false;
            Brush brush = Brushes.Gray;
            ACControl.SetColor(brush);
            BSControl.SetColor(brush);
            CControl.SetColor(brush);
            DControl.SetColor(brush);
            SControl.SetColor(brush);
            
        }
    }

}

