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
            //foreach (Canvas canvas in spielbaresSpielfeld.Children)
            //{
            //    canvas.MouseDown += Canvas_MouseDown;
            //}
        }

        //private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        //{
            
        //    Canvas canvas = sender as Canvas;


        //}
        public List<Canvas> GetGridSpielfeldCanvas()
        {
            List<Canvas> result = new List<Canvas>();
            foreach(Canvas canvas in spielbaresSpielfeld.Children)
            {
                result.Add(canvas);
            }
            return result;
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
            for(int reihe = 0; reihe <= 9; reihe++)
            {
                string reihenBuchstabe = alphabet[reihe];
                for(int spalte = 0; spalte <= 9; spalte++)
                {
                    index++;
                    string TileStatus = Spielfeld.SpielfeldTiles.At(reihe, spalte).SchiffsteilStatus;
                    string TileName = reihenBuchstabe + (spalte+1);
                    foreach(Canvas c in spielbaresSpielfeld.Children)
                    {
                        if(c.Name == TileName)
                        {
                            if(TileStatus == "0")
                            {

                                c.Background = Brushes.Blue;
                            }
                            else
                            {
                                c.Background = Brushes.Gray;
                            }
                            TextBox test = new TextBox();
                            test.Text = TileName;
                            test.Background = Brushes.White;

                            c.Children.Add(test);
                        }

                    }
                }
            }
        }
        public void updateGUIGegner(Spielfeld Spielfeld)
        {
            foreach (Canvas c in spielbaresSpielfeld.Children)
            {

                c.Background = Brushes.Blue;
            }
        }
    }
}
