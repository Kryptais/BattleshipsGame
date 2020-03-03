﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schiffeversenken.Schiffe;
namespace Schiffeversenken
{
    public class Spieler
    {
        public string Name { get; set; }
        public Spielfeld Spielfeld { get; set; }
        public schussSpielfeld schussSpielfeld { get; set; }
        public List<Schiffe.Schiffe> Schiffe { get; set; }
        public bool hatVerloren
        {
            get
            {
                return Schiffe.All(x => x.istGesunken);
            }
        }
        public Spieler(string name)
        {
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
        public void platziereSchiffe()
        {
            //Random class creation stolen from http://stackoverflow.com/a/18267477/106356
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            foreach (var schiff in Schiffe)
            {
                bool isOpen = true;
                while (isOpen)
                {
                    var startSpalte = rand.Next(1,11);
                    var startReihe = rand.Next(1, 11);
                    int endReihe = startReihe, endSpalte = startSpalte;
                    var orientation = rand.Next(1, 101) % 2; //0 für Horizontal

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
                    if(endReihe > 10 || endSpalte > 10)
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
                        spielfeldTile.Teilbelegung =schiff.Teilbelegung;
                    }
                    isOpen = false;
                }
            }
        }
         public Koordinaten FireShot()
        {
            // Wenn auf dem Spielfeld fleder sind die getroffen sind und Nachbarn keine schüsse haben, sollten wir zuerst dadrauf schiessen.
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

        public SchussErgebnis VerarbeiteSchuss(Koordinaten koords)
        {
            var Tile = Spielfeld.SpielfeldTiles.At(koords.Reihe, koords.Spalte);
            if(!Tile.istBesetzt)
            {
               // Console.WriteLine(Name + " says: \"Miss!\"");
                return SchussErgebnis.Miss;
            }
            var schiff = Schiffe.First(x => x.Teilbelegung == Tile.Teilbelegung);
            schiff.Treffer++;
            //Console.WriteLine(Name + " says: \"Hit!\"");
            if (schiff.istGesunken)
            {
                //Console.WriteLine(Name + " says: \"You sunk my " + ship.Name + "!\"");
            }
            return SchussErgebnis.Hit;
        }

        public void VerarbeiteSchussErgebniss(Koordinaten koords, SchussErgebnis result)
        {
            var Tile = schussSpielfeld.SpielfeldTiles.At(koords.Reihe, koords.Spalte);
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