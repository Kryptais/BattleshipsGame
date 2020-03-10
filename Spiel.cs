using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
                    break;

                case GameState.PlayerTurn:
                    main.SchussFeld.Visibility = Visibility.Visible;
                    main.SchussFeldNoTurn.Visibility = Visibility.Hidden;
                    break;

                case GameState.PlayerHasShot:
                    var koordinatenSpielerSchuss = koordinaten;
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
                                c.Background = Brushes.Yellow;
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
                    //System.Threading.Thread.Sleep(2000);
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
                                c.Background = Brushes.Yellow;
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
                    }
                    this.Update();
                    break;

                case GameState.GameOver:

                    break;

                default:
                    MessageBox.Show("Game is in an invalid state!");
                    return;
            }
        }
    }
}
