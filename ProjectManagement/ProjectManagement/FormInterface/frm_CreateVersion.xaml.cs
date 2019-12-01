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

namespace ProjectManagement.FormInterface
{
    /// <summary>
    /// Logique d'interaction pour frm_CreateVersion.xaml
    /// </summary>
    public partial class frm_CreateVersion : Window
    {
        public frm_CreateVersion()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            CmdRevit.CmdCreateVersion.auteur = txbAuteur.Text;
            CmdRevit.CmdCreateVersion.comment = txbComment.Text;
        }
    }
}
