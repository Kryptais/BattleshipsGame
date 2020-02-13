using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
    /// Interaktionslogik für PlayField.xaml
    /// </summary>
    public partial class PlayField : UserControl
    {
        public PlayField()
        {
            InitializeComponent();
            string[] alphabet = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            //for(int i = 1; i<=11; i++)
            //{
            //    for(int j = 1; j<=11; j++)
            //    {
            //        ContentControl contentcontrol = new ContentControl();
            //        Rectangle rectangle = new Rectangle();
            //        contentcontrol.Content = rectangle;
            //        PlayGrid.Children.Add(contentcontrol);
            //        Grid.SetRow(contentcontrol, i);
            //        Grid.SetColumn(contentcontrol, j);
            //        //contentcontrol.Content = (i-1) + (j-1);
            //        contentcontrol.VerticalAlignment = VerticalAlignment.Center;
            //        contentcontrol.HorizontalAlignment = HorizontalAlignment.Center;
            //        contentcontrol.Margin = new Thickness(2);
            //        rectangle.Fill = Brushes.DeepSkyBlue;
            //        contentcontrol.MouseDown += Contentcontrol_MouseDown;
                    
            //    }
            //}

            //for(int l = 0; l < 10; l++)
            //{
            //    int k = 0;
            //    int l2 = l + 1;
            //    Label label = new Label();
            //    label.Content = alphabet[l];
            //    PlayGrid.Children.Add(label);
            //    label.HorizontalAlignment = HorizontalAlignment.Center;
            //    label.VerticalAlignment = VerticalAlignment.Center;
            //    label.FontSize = 18;
            //    Grid.SetColumn(label, l2);
            //    Grid.SetRow(label, k);
            //}
            //for(int m = 0; m< 10; m++)
            //{
            //    int n = 0;
            //    int m2 = m + 1;
            //    Label label = new Label();
            //    label.Content = m2+"";
            //    PlayGrid.Children.Add(label);
            //    label.HorizontalAlignment = HorizontalAlignment.Center;
            //    label.VerticalAlignment = VerticalAlignment.Center;
            //    label.FontSize = 18;
            //    Grid.SetColumn(label, n);
            //    Grid.SetRow(label, m2);
            //}
        }
        private void MouseOverEvent(object sender, MouseEventHandler e)
        {

        }
        private void Contentcontrol_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //((sender as ContentControl).Content as Rectangle).Fill = Brushes.Gray;
        }
    }
}
