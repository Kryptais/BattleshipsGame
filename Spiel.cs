using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Schiffeversenken
{
    public enum GameState { PlayerPlacement, PlayerTurn, PlayerHasShot, EnemyTurn, GameOver };
    public class Spiel
    {
        public Spieler Spieler1 { get; set; }
        public Spieler Spieler2 { get; set; }
        MainWindow main { get; set; }
        public Koordinaten koordinaten { get; set; }
        public GameState currentState;
        public int runde = 1;
        public Spiel(MainWindow main)
        {
            this.main = main;
            Spieler1 = new Spieler("Simon", main);
            Spieler2 = new Spieler("AI", main);
            currentState = GameState.PlayerPlacement;
            this.Update();
        }
        public void Update()
        {
            switch (currentState)
            {
                case GameState.PlayerPlacement:
                    //Ai Placement  
                    //Spieler2.platziereSchiffe();
                    Spieler1.ZeichneSpielfeldNoButton(main.SchiffFeld);
                    break;

                case GameState.PlayerTurn:
                    main.SchussFeld.Visibility = Visibility.Visible;
                    main.SchussFeldNoTurn.Visibility = Visibility.Hidden;

                    break;

                case GameState.PlayerHasShot:
                    var koordinatenSpielerSchuss = koordinaten;
                    main.EventBox.Text = "Runde " + runde + ":";
                    var result = Spieler2.VerarbeiteSchuss(koordinatenSpielerSchuss);

                    Spieler1.VerarbeiteSchussErgebniss(koordinatenSpielerSchuss, result);
                    main.SchussFeld.Visibility = Visibility.Hidden;
                    main.SchussFeldNoTurn.Visibility = Visibility.Visible;
                    foreach (Canvas c in (main.SchussFeld.spielbaresSpielfeld.Children))
                    {
                        if (c.Name == koordinatenSpielerSchuss.convertKoordinatenToTileName())
                        {
                            if (result == SchussErgebnis.Hit)
                            {
                                c.Background = Brushes.Red;
                            }
                            else if (result == SchussErgebnis.Miss)
                            {

                                


                                c.Background = Brushes.Blue;
                                //Image image = new Image();
                                //string packUri = "C:\\Users\\simso\\Source\\Repos\\BattleshipsGame\\Resources\\XMLFile1.xml";
                                //image.Height = c.ActualHeight;
                                //image.Margin = new Thickness(1);
                                //image.Width = c.ActualWidth;
                                //image.Source = new ImageSourceConverter().ConvertFromString(packUri) as ImageSource;
                                newCircle(c);
                                //newLine(c);
                            }
                            break;
                        }
                        
                    }
                    if (Spieler2.hatVerloren)
                    {
                        currentState = GameState.GameOver;
                    }
                    else
                    {
                        currentState = GameState.EnemyTurn;
                    }
                    this.Update();
                    break;

                case GameState.EnemyTurn:

                    var koordinatenAISchuss = Spieler2.FireShot();
                    var ergebnis = Spieler1.VerarbeiteSchuss(koordinatenAISchuss);
                    Spieler2.VerarbeiteSchussErgebniss(koordinatenAISchuss, ergebnis);
                    foreach(Canvas c in (main.SchiffFeld.spielbaresSpielfeld.Children))
                    {
                        if(c.Name == koordinatenAISchuss.convertKoordinatenToTileName())
                        {
                            if(ergebnis == SchussErgebnis.Hit)
                            {
                                c.Background = Brushes.Red;
                            }
                            else if(ergebnis == SchussErgebnis.Miss)
                            {
                                newLine(c);
                            }
                            break;
                        }

                    }
                    if (Spieler1.hatVerloren)
                    {
                        currentState = GameState.GameOver;
                    }
                    else
                    {
                        currentState = GameState.PlayerTurn;
                        runde++;
                    }
                    this.Update();
                    break;

                case GameState.GameOver:
                    main.SchussFeld.Visibility = Visibility.Visible;
                    main.SchussFeldNoTurn.Visibility = Visibility.Hidden;
                    if (Spieler1.hatVerloren)
                    {
                        if(MessageBox.Show("Du hast Verloren!") == MessageBoxResult.OK)
                        {
                            main.GameOverLabel.Content = "Du hast Verloren!";
                            main.tabcontrol.SelectedItem = main.AfterGameTabMenue;
                        }

                    }
                    else if (Spieler2.hatVerloren)
                    {
                        if (MessageBox.Show("Du hast Gewonnen!") == MessageBoxResult.OK)
                        {
                            main.GameOverLabel.Content = "Du hast Gewonnen";
                            main.tabcontrol.SelectedItem = main.AfterGameTabMenue;
                        }

                    }

                    break;

                default:
                    MessageBox.Show("Game is in an invalid state!");
                    return;
            }
            main.SizeChanged += Main_SizeChanged;
        }

        private void Main_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            foreach (Canvas c in main.SchussFeld.spielbaresSpielfeld.Children)
            {
                double size = c.ActualWidth;
                foreach (Ellipse ellipse in c.Children)
                {
                    ellipse.Width = size - 4;
                    ellipse.Height = size - 4;
                }
            }

        }

        void newLine(Canvas canvas) 
        {
            Line line = new Line();
            line.X1 = 5;
            line.Y1 = 5;

            line.Y2 = canvas.ActualHeight -5;
            line.X2 = canvas.ActualWidth -5;

            line.StrokeThickness = 3;
            Brush brush = Brushes.Black;
            line.Stroke = brush;
            line.SnapsToDevicePixels = true;
            line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Unspecified);

            Line line2 = new Line();
            line2.X2 = 5;
            line2.Y2 = canvas.ActualHeight -5;
                
            line2.Y1 = 5;
            line2.X1 = canvas.ActualWidth -5;
                
            line2.StrokeThickness = 3;
            line2.Stroke = brush;
            line2.SnapsToDevicePixels = true;
            line2.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Unspecified);
            line.ClipToBounds = true;
            line2.ClipToBounds = true;

            canvas.Children.Add(line);
            canvas.Children.Add(line2);

        }
        void newCircle(Canvas c)
        {
            
            Ellipse ellipse = new Ellipse();
            ellipse.Height = c.ActualHeight-4;
            ellipse.Width = c.ActualWidth-4;
            ellipse.HorizontalAlignment = HorizontalAlignment.Center;
            ellipse.VerticalAlignment = VerticalAlignment.Center;
            ellipse.Stretch = Stretch.Fill;
            Brush brush = Brushes.Black;
            ellipse.Stroke = brush;
            ellipse.Margin = new Thickness(2, 2, 2, 2);
            ellipse.StrokeThickness = 3;
            c.Children.Add(ellipse);

        }
        
    }
}
