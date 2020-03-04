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
    public partial class GridSpielfeld : UserControl
    {
        public GridSpielfeld()
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
            for(int reihe = 1; reihe<=10; reihe++)
            {
                string reihenBuchstabe = alphabet[reihe-1];
                for(int spalte = 1; spalte <=10; spalte++)
                {
                    index++;
                    string TileStatus = Spielfeld.SpielfeldTiles.At(reihe, spalte).SchiffsteilStatus;
                    string TileName = reihenBuchstabe + spalte;
                    foreach(ToggleButton tb in spielbaresSpielfeld.Children)
                    {
                        if(tb.Name == TileName)
                        {
                            if(TileStatus == "0")
                            {
                                tb.Background = Brushes.Blue;
                                tb.Content = default;
                            }
                            else
                            {
                                tb.Background = default;
                                tb.Content = TileStatus;
                            }

                        }
                    }
                }
            }
        }
    }
}
