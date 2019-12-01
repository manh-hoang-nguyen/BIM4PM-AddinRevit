using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
using ProjectManagement.CmdRevit.Utils;
using ProjectManagement.Commun;
using ProjectManagement.Controllers;
using ProjectManagement.Models;
using ProjectManagement.Routes;
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
    /// Logique d'interaction pour frm_Login.xaml
    /// </summary>
    public partial class frm_Login : Window
    {
        UIApplication _uiapp;
        public static Document _doc;
        PanelProprety _panelProprety = null;
        static bool _subscribed = false;
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
            DialogResult = true;
            try
            {
                UserData.authentication = LoginController.GetTokenAndUserId(email.Text, password.Password);
                UserData.user = UserController.GetUser();
            }
            catch
            {
                return;
            }
            UserData.userProject = UserProjectController.GetUserProject(UserRouter.GetUserProject, UserData.authentication.token, UserData.authentication.userId);
            App._uicapp.ControlledApplication.DocumentChanged += new EventHandler<DocumentChangedEventArgs>(OnDocumentChanged);
            frm_SelectProject frm_SelectProject = new frm_SelectProject(_uiapp, _doc);
            frm_SelectProject.ShowDialog();
            if (frm_SelectProject.DialogResult.HasValue && frm_SelectProject.DialogResult.Value)
            {
                _doc = MethodeRevitApi.GetDocumentByTitle(_uiapp, ModificationTracker.ProjectName);
                Close();
               
            }
            else { return; }
              
                
            
        }
        public static void OnDocumentChanged(object sender, DocumentChangedEventArgs e)
        {
             
            
            if (e.GetDocument().Title == ModificationTracker.ProjectName)
            {
              
               foreach (ElementId item in e.GetDeletedElementIds())
                {
                    ModificationTracker.deletedElement.Add(item);
                    //frm_ModificationWatcher._main.SetNbrDeletedElement();
               
                } 

                foreach (ElementId item in e.GetAddedElementIds())
                {
                    ModificationTracker.newElement.Add(item);
                    //frm_ModificationWatcher._main.SetNbrNewElement();
                }
                foreach (ElementId item in e.GetModifiedElementIds())
                {
                    IEnumerable<ElementId> elementIds = from id in ModificationTracker.modifiedElement
                                                        select id;
                   
                   
                    if (!elementIds.Contains(item))
                    {
                        ModificationTracker.modifiedElement.Add(item);
                       // frm_ModificationWatcher._main.SetNbrModifiedElement();

                        ElementWatcher elementWatcher = new ElementWatcher
                        {
                            Id = item,
                            Category= e.GetDocument().GetElement(item).Category.Name,
                            Comment = ""
                        };
                        frm_ModificationWatcher.colModifiedElement.Add(elementWatcher);
                    }
                    
                }
                
              
            }
        }
      
    }
}
