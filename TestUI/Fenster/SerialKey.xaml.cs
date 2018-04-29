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
using System.Windows.Shapes;

namespace TestUI.Fenster
{
    /// <summary>
    /// Interaktionslogik für SerialKey.xaml
    /// </summary>
    public partial class SerialKey : Window
    {
        public SerialKey()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            uint productID = uint.Parse(tbProductID.Text);
            uint customerID = uint.Parse(tbCustomerID.Text);
            DateTime date = calenderDate.DisplayDate;
            SerialCreator serial = new SerialCreator(new Serial(), new Blowfish(), new Translation());
            tbSerial.Text = serial.CreateSerial(productID, customerID, date);
        }

        private void btnDecrypt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
