using Autodesk.Revit.UI;
using GalaSoft.MvvmLight;
using Autodesk.Revit.ApplicationServices;
using ProjectManagement.Tools.Project;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ProjectManagement.Tools.Auth;
using ProjectManagement.FormInterface;

namespace ProjectManagement.Tools
{
   public class PaletteViewModel: ViewModelBase
    {
        public ObservableCollection<TabItem> TabItems { get; set; } = new ObservableCollection<TabItem>();

        public PaletteViewModel()
        {

            TabItems.Add(new TabItem
            {
                Content = new ProjectView() { DataContext = new ProjectViewModel()}  ,
                Header = "Connect"
            });
        }
    }
}
