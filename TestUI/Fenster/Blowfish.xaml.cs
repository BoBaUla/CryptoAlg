using KryptoAlg;
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
    /// Interaktionslogik für Blowfish.xaml
    /// </summary>
    public partial class BlowfishUI : Window
    {
        Krypt<ulong> _krypt = new Krypt<ulong>(new Blowfish(), new Translation(), new MessageSplitter());

        public BlowfishUI()
        {
            InitializeComponent();
        }
        
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnEncrypt_Click(object sender, RoutedEventArgs e)
        {
            tbEncrypted.Text = _krypt.EncryptMessage(tbMessage.Text);
        }

        private void btnDecrypt_Click(object sender, RoutedEventArgs e)
        {
            tbDecrypted.Text = _krypt.DecryptMessage(tbEncrypted.Text);
        }
    }
}
