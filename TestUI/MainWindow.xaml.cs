using KryptoAlg;
using KryptoAlg.Klassen;
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
using TestUI.Fenster;

namespace TestUI
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSerial_Click(object sender, RoutedEventArgs e)
        {
            SerialKey serialkey = new SerialKey();
            serialkey.ShowDialog();
        }

        private void btnBlowfish_Click(object sender, RoutedEventArgs e)
        {
            BlowfishUI blow = new BlowfishUI();
            blow.ShowDialog();
        }

        private void btnSpline_Click(object sender, RoutedEventArgs e)
        {
            Spline spline = new Spline();
            spline.ShowDialog();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

    }
}
