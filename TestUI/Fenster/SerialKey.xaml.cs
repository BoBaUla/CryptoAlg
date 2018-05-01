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
            DateTime date = (DateTime)calenderDate.SelectedDate;
            SerialCreator serial = new SerialCreator(new Serial(), new Blowfish(), new Translation());
            tbSerial.Text = serial.CreateSerial(productID, customerID, date);
        }

        private void btnDecrypt_Click(object sender, RoutedEventArgs e)
        {
            SerialCreator serial = new SerialCreator(new Serial(), new Blowfish(), new Translation());
            lblDateDecypted.Content = serial.GetDate(tbSerial.Text).ToString();
            lblCustIDDecrypt.Content = serial.GetCustomerID(tbSerial.Text).ToString();
            lblProdIDDecrypt.Content = serial.GetProductID(tbSerial.Text).ToString();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void tbProductID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            uint val = 0;
            e.Handled = !(uint.TryParse(e.Text, out val));
            uint temp = 0;
            if(!string.IsNullOrWhiteSpace(((TextBox)sender).Text))
                e.Handled = !(uint.TryParse(((TextBox)sender).Text, out temp));
            if(temp != 0)
                e.Handled = temp + val > 0xFFFF;
        }

        private void tbCustomerID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            uint val = 0;
            e.Handled = !(uint.TryParse(e.Text, out val));
            uint temp = 0;
            if (!string.IsNullOrWhiteSpace(((TextBox)sender).Text))
                e.Handled = !(uint.TryParse(((TextBox)sender).Text, out temp));
            if (temp != 0)
                e.Handled = temp + val > 0xFFFF;
        }
    }
}
