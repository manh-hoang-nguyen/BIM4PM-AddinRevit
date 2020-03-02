using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
using System.Windows;

namespace ProjectManagement.FormInterface
{
    /// <summary>
    /// Logique d'interaction pour frm_Login.xaml
    /// </summary>
    public partial class frm_Login : Window
    {
        private UIApplication _uiapp;
        public static Document _doc;

        public frm_Login(UIApplication uiapp)
        {
            InitializeComponent();
            _uiapp = uiapp;

            foreach (Autodesk.Windows.RibbonTab tab in Autodesk.Windows.ComponentManager.Ribbon.Tabs)
            {
                if (tab.Id == "Modify")
                {
                    tab.PropertyChanged += PanelProprety.PanelEvent;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        public static void OnDocumentChanged(object sender, DocumentChangedEventArgs e)
        {
        }
    }
}