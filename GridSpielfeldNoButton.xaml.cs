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
    /// <summary>
    /// Interaktionslogik für GridSpielfeld.xaml
    /// </summary>
    public partial class GridSpielfeldNoButton : UserControl
    {
        public GridSpielfeldNoButton()
        {
            InitializeComponent();
        }

        private void UsercontrolSizeChanged(object sender, SizeChangedEventArgs e)
        {
            double size = Math.Min(e.NewSize.Height, e.NewSize.Width);
            mainGrid.Width = size;
            mainGrid.Height = size;
        }

        public void updateGUI(Spielfeld Spielfeld)
        {
            int index = 0;
            String[] alphabet = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            for (int reihe = 0; reihe == 9; reihe++)
            {
                string reihenBuchstabe = alphabet[reihe];
                for (int spalte = 0; spalte == 9; spalte++)
                {
                    index++;
                    string TileStatus = Spielfeld.SpielfeldTiles.At(reihe, spalte).SchiffsteilStatus;
                    string TileName = reihenBuchstabe + spalte;
                    List<Canvas> canvas = new List<Canvas>();
                    foreach (Canvas c in spielbaresSpielfeld.Children)
                    {
                        canvas.Add(c);
                    }
                    Canvas test = canvas.Where(x => x.Name == TileName).First();
                    if (TileStatus == "0")
                    {
                        test.Background = Brushes.Blue;
                    }
                    else 
                    {
                        test.Background = Brushes.Gray;
                    }
                }
            }
        }
    }
}
