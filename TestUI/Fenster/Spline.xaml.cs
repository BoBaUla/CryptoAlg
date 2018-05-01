using OxyPlot;
using OxyPlot.Series;
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
using System.Windows.Shapes;
using TestUI.Controls;

namespace TestUI.Fenster
{
    /// <summary>
    /// Interaktionslogik für Spline.xaml
    /// </summary>
    public partial class Spline : Window
    {
        public Spline()
        {
            InitializeComponent();
            LineSeries ser = new LineSeries(); 
            for (int i = 0; i < 20; i++)
                ser.Points.Add(new DataPoint(i, Math.Sqrt(i)));

            ((PlotterViewModel)PlotterSpline.DataContext).MyModel.Series.Add(ser);
        }

        private void btnAddValues_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClearValues_Click(object sender, RoutedEventArgs e)
        {
            ((PlotterViewModel)PlotterSpline.DataContext).MyModel.Series.Clear();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
