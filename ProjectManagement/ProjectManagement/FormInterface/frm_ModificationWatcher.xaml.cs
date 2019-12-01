using Autodesk.Revit.DB;
using ProjectManagement.Commun;


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Logique d'interaction pour frm_ModificationWatcher.xaml
    /// </summary>
    public partial class frm_ModificationWatcher : Window 
    {
      public static  ObservableCollection<ElementWatcher> colModifiedElement = new ObservableCollection<ElementWatcher>();
      //public static MainViewModel _main = new MainViewModel();
        public frm_ModificationWatcher()
        {
            InitializeComponent();
 
             //DataContext = _main;
            dgModifiedElement.ItemsSource = colModifiedElement;

        }
        public static void CountDeletedEvent(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
           
        }

        private void EleWatcher_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            dgModifiedElement.Height = EleWatcher.Height / 4;
        }
    }


    public class ElementWatcher
    {
        public ElementId Id { get; set; }
        public string Category { get; set; }
        public string Comment { get; set; }
    } 
    }

