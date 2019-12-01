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
using ProjectManagement.Commun;

namespace ProjectManagement.FormInterface
{
    /// <summary>
    /// Logique d'interaction pour frm_SendData.xaml
    /// </summary>
    public partial class frm_SendData : Window
    {
        public frm_SendData()
        {
            InitializeComponent();
            for (int i = 1; i > VersionCommun.VersionInDatabase.Count; i++) cbxSelectProject.Items.Add(i);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            string sent= comment.Text;
            string[] stringArray = comment.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            string ddd = "";
            int count = stringArray.Length;
            for (int i = 0; i < count-2; i++)
            {
                ddd += stringArray[i] + "\\n";
            }
            ddd += stringArray[count - 1];
            //foreach (string item in stringArray)
            //{
            //    ddd += item + "\\n";
            //}
            CmdRevit.CmdSendData.auteur = auteur.Text;
            CmdRevit.CmdSendData.comment = ddd;
            if (version.IsChecked == true) CmdRevit.CmdSendData.version = (VersionCommun.VersionInDatabase.Count+1).ToString();
            else CmdRevit.CmdSendData.version = VersionCommun.VersionInDatabase.Count.ToString();
            Close();
        }

    }
}
