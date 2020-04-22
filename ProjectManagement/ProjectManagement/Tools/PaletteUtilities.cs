using Autodesk.Revit.UI;
using GalaSoft.MvvmLight;
using BIM4PM.UI.Commun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace BIM4PM.UI.Tools
{
  public static  class PaletteUtilities
    {
        public static void RegisterPalette(UIControlledApplication app)
        {
            var view = new PaletteMainView() { DataContext = new PaletteViewModel(EventProvider.Instance.EventAggregator) };
            App.PaletteWindow = view;
            var unused = new DockablePaneProviderData
            {
                FrameworkElement = App.PaletteWindow,
                InitialState = new DockablePaneState
                {
                    DockPosition = DockPosition.Tabbed,
                    TabBehind = DockablePanes.BuiltInDockablePanes.ProjectBrowser
                }
            };
            var dpid = new DockablePaneId(new Guid(Properties.Resources.PaletteGuid));
            try
            {
                // It's possible that a dockable panel with the same id already exists
                // This ensures that we don't get an exception here. 
                app.RegisterDockablePane(dpid, "Home", App.PaletteWindow);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// Due to all asynch stuff some data might not be available right away so we use this callback to instantiate the Palette.
        /// </summary>
        public static void LaunchPanel()
        {
            
           
                App.PaletteWindow.MainControl.Dispatcher.Invoke(() =>
            {
                // (Konrad) We have to make sure that we unregister from all Messaging before reloading UI.
                if (App.PaletteWindow.DataContext != null)
                {
                    var tabItems = App.PaletteWindow.MainControl.Items.SourceCollection;
                    foreach (var ti in tabItems)
                    {
                        var content = ((UserControl)((TabItem)ti).Content).DataContext as ViewModelBase;
                        content?.Cleanup();
                    }
                }

                // (Konrad) Now we can reset the ViewModel
                //App.PaletteWindow.DataContext = new PaletteViewModel();
                //if (App.PaletteWindow.MainControl.Items.Count > 0)
                //{
                //    App.PaletteWindow.MainControl.SelectedIndex = 0;
                //}
            }, DispatcherPriority.Normal);
        }

        public static void ClearPanel()
        {
            if (App.PaletteWindow.MainControl.Items.Count > 0)
            {
                App.PaletteWindow.MainControl.Items.Clear();
            }

          
        }
    }
}
