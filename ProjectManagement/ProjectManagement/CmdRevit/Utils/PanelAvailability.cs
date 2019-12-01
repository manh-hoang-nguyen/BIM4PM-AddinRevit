using System.Windows;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.CmdRevit.Utils
{
    public class PanelAvailability
    {
        public static void ShowAll(string tabName)
        {

            foreach (Autodesk.Windows.RibbonTab tab in Autodesk.Windows.ComponentManager.Ribbon.Tabs)
            {
                if (tab.Id == tabName)
                {
                    for (int i = 1; i < tab.Panels.Count; i++)
                    {
                        Autodesk.Windows.RibbonPanel rp = tab.Panels[i];
                        rp.IsVisible = true;
                    }
                   
                }
            }
        }
        public static void HideAll(string tabName)
        {

            foreach (Autodesk.Windows.RibbonTab tab in Autodesk.Windows.ComponentManager.Ribbon.Tabs)
            {
                if (tab.Id == tabName)
                {
                    for (int i = 1; i < tab.Panels.Count; i++)
                    {
                        Autodesk.Windows.RibbonPanel rp = tab.Panels[i];
                        rp.IsVisible = false;
                    }

                }
            }
        }
        public  static void Hide(string tabName, string panelName )
        {

            foreach (Autodesk.Windows.RibbonTab tab in Autodesk.Windows.ComponentManager.Ribbon.Tabs)
            {
                if (tab.Id == tabName)
                {
                    Autodesk.Windows.RibbonPanel rp = (from e in tab.Panels
                                                       where e.Source.Title == panelName
                                                       select e).FirstOrDefault();


                    rp.IsVisible = false;
                }
            }
        }
        public static void Desactive(string tabName, string panelName)
        {

            foreach (Autodesk.Windows.RibbonTab tab in Autodesk.Windows.ComponentManager.Ribbon.Tabs)
            {
                if (tab.Id == tabName)
                {
                    Autodesk.Windows.RibbonPanel rp = (from e in tab.Panels
                                                       where e.Source.Title == panelName
                                                       select e).FirstOrDefault();


                    rp.IsEnabled = false;
                }
            }
        }
        public static void Visible(string tabName, string panelName)
        {

            foreach (Autodesk.Windows.RibbonTab tab in Autodesk.Windows.ComponentManager.Ribbon.Tabs)
            {
                if (tab.Id == tabName)
                {
                    Autodesk.Windows.RibbonPanel rp = (from e in tab.Panels
                                                       where e.Source.Title == panelName
                                                       select e).FirstOrDefault();


                    rp.IsVisible = true;
                }
            }
        }
        public static void Enable(string tabName, string panelName)
        {

            foreach (Autodesk.Windows.RibbonTab tab in Autodesk.Windows.ComponentManager.Ribbon.Tabs)
            {
                if (tab.Id == tabName)
                {
                    Autodesk.Windows.RibbonPanel rp = (from e in tab.Panels
                                                       where e.Source.Title == panelName
                                                       select e).FirstOrDefault();


                    rp.IsEnabled = true;
                }
            }
        }
    }
}
