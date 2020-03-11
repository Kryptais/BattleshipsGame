using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Schiffeversenken.Schiffe;
namespace Schiffeversenken
{
    public class Spieler
    {
        public string Name { get; set; }
        public Spielfeld Spielfeld { get; set; }
        public schussSpielfeld schussSpielfeld { get; set; }
        public List<Schiffe.Schiffe> Schiffe { get; set; }
        MainWindow Main;
        public bool hatVerloren
        {
            get
            {
                return Schiffe.All(x => x.istGesunken);
            }
        }
        public Spieler(string name, MainWindow main)
        {
            Main = main;
            Name = name;
            Schiffe = new List<Schiffe.Schiffe>()
            {
                new Battleship(),
                new AircraftCarrier(),
                new Cruiser(),
                new Destroyer(),
                new Submarine()
            };
            Spielfeld = new Spielfeld();
            schussSpielfeld = new schussSpielfeld();
        }
        public void ZeichneSpielfeld(GridSpielfeld gridSpielfeld)
        {
            gridSpielfeld.updateGUI(Spielfeld);
        }
        public void ZeichneSpielfeldNoButton(GridSpielfeldNoButton gridSpielfeldNoButton)
        {
            gridSpielfeldNoButton.updateGUI(Spielfeld); 
        }
        public void ZeichneSpielfeldVonGegner(GridSpielfeld gridSpielfeld)
        {
            gridSpielfeld.updateGUIGegner(schussSpielfeld);
        }
        public void bereinigeSpielfeld()
        {
            foreach(SpielfeldTile st in Spielfeld.SpielfeldTiles)
            {
                st.Teilbelegung = Teilbelegung.Leer;
            }
        }
        public void platziereSchiffe()
        {
            //Random class creation stolen from http://stackoverflow.com/a/18267477/106356
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            foreach (var schiff in Schiffe)
            {
                bool isOpen = true;
                while (isOpen)
                {
                    var startSpalte = rand.Next(0,9);
                    var startReihe = rand.Next(0, 9);
                    int endReihe = startReihe, endSpalte = startSpalte;
                    var orientation = rand.Next(0, 100) % 2; //0 für Horizontal

                    List<int> schiffsteilNummern = new List<int>();
                    if (orientation == 0)
                    {
                        for (int i = 1; i < schiff.Laenge; i++)
                        {
                            endReihe++;
                        }
                    }
                    else
                    {
                        for (int i = 1; i < schiff.Laenge; i++)
                        {
                            endSpalte++;
                        }
                    }

                    //Schiffe können nicht über das Spielfeld hinaus
                    if(endReihe > 9 || endSpalte > 9)
                    {
                        isOpen = true;
                        continue;
                    }

                    //Sind die Felder bereits belegt?
                    var affectedPanels = Spielfeld.SpielfeldTiles.Reichweite(startReihe, startSpalte, endReihe, endSpalte);
                    if(affectedPanels.Any(x=>x.istBesetzt))
                    {
                        isOpen = true;
                        continue;
                    }

                    foreach(var spielfeldTile in affectedPanels)
                    {
                        spielfeldTile.Teilbelegung = schiff.Teilbelegung;
                    }
                    isOpen = false;
                }
            }
        }
         public Koordinaten FireShot()
        {
            
            // Wenn auf dem Spielfeld felder sind die getroffen sind und Nachbarn keine schüsse haben, sollten wir zuerst dadrauf schiessen.
            var getroffeneNachbarn = schussSpielfeld.GetGetroffeneNachbarn();
            Koordinaten koords;
            if(getroffeneNachbarn.Any())
            {
                koords = ZielsuchenderSchuss();
            }
            else
            {
                koords = ZufaelligerSchuss();
            }

            //Console.WriteLine(Name + " says: \"Firing shot at " + coords.Row.ToString() + ", " + coords.Column.ToString() + "\"");
            return koords;
        }


        private Koordinaten ZufaelligerSchuss()
        {
            var verfügbareSpielfeldTiles = schussSpielfeld.GetUebrigeRandomSpielfeldTiles();
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            var TileID = rand.Next(verfügbareSpielfeldTiles.Count);
            return verfügbareSpielfeldTiles[TileID];
        }

        private Koordinaten ZielsuchenderSchuss()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            var getroffeneNachbarn = schussSpielfeld.GetGetroffeneNachbarn();
            var NachbarID = rand.Next(getroffeneNachbarn.Count);
            return getroffeneNachbarn[NachbarID];
        }

        public SchussErgebnis  VerarbeiteSchuss(Koordinaten koords)
        {
            int index = koords.Reihe + koords.Spalte;
            SpielfeldTile Tile = Spielfeld.SpielfeldTiles.At(koords.Reihe, koords.Spalte);// [index];//.At(koords.Reihe, koords.Spalte);
            if(!Tile.istBesetzt)
            {
                
                //Main.EventBox.Text += Environment.NewLine + Name + " says: \"Miss!\"";
                return SchussErgebnis.Miss;
            }
            else
            {
                var schiff = Schiffe.First(x => x.Teilbelegung == Tile.Teilbelegung);
                schiff.Treffer++;
                if (schiff.istGesunken)
                {
                    Brush brush = Brushes.Red;
                    //Main.EventBox.Text += Environment.NewLine + Name + " says: \" Du hast mein " + schiff.Name + " versenkt!";
                    if(schiff.Teilbelegung == Teilbelegung.AircraftCarrier && this.Name == "AI")
                    {
                        Main.ACControl.SetColor(brush);
                    }
                    else if (schiff.Teilbelegung == Teilbelegung.Battleship && this.Name == "AI")
                    {
                        Main.BSControl.SetColor(brush);
                    }
                    else if (schiff.Teilbelegung == Teilbelegung.Cruiser && this.Name == "AI")
                    {
                        Main.CControl.SetColor(brush);
                    }
                    else if (schiff.Teilbelegung == Teilbelegung.Destroyer && this.Name == "AI")
                    {
                        Main.DControl.SetColor(brush);
                    }
                    else if (schiff.Teilbelegung == Teilbelegung.Submarine && this.Name == "AI")
                    {
                        Main.SControl.SetColor(brush);
                    }
                }
                else
                {
                    //Main.EventBox.Text += Environment.NewLine + Name + " says: \"Treffer!\"";
                }
                return SchussErgebnis.Hit;
            }
            
        }

        public void VerarbeiteSchussErgebniss(Koordinaten koords, SchussErgebnis result)
        {
            int index = koords.Reihe + koords.Spalte;
            var Tile = this.schussSpielfeld.SpielfeldTiles.At(koords.Reihe, koords.Spalte);// schussSpielfeld.SpielfeldTiles[index];//.At(koords.Reihe, koords.Spalte);
            switch (result)
            {
                case SchussErgebnis.Hit:
                    Tile.Teilbelegung = Teilbelegung.Getroffen;
                    break;

                default:
                    Tile.Teilbelegung = Teilbelegung.Miss;
                    break;
            }
        }
    }
}
