#region Namespaces

using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using ProjectManagement.FormInterface;
using System;
using System.Reflection;
using ProjectManagement.Tools.Project;
#endregion Namespaces

namespace ProjectManagement
{
    internal class App : IExternalApplication
    {
        public static App Instance { get; private set; }
        private DockablePaneId _dpid;
        public static string _projectName;
        internal static App _app = null;
        private RibbonItem _button;

        /// <summary>
        /// Provide access to singleton class instance.
        /// </summary>
        

        private PanelProprety _panelProprety = null;
        public static UIControlledApplication _uicapp = null;
        private static bool _subscribed = false;
        private UIDocument _uiDoc;
        private Document _doc;

        public static ModelRequestHandler ModelHandler { get; set; }
        public static ExternalEvent ModelEvent { get; set; }

        public Result OnStartup(UIControlledApplication uicapp)
        {
            Instance = this;
            ModelHandler = new ModelRequestHandler();
            ModelEvent = ExternalEvent.Create(ModelHandler);
            _uicapp = uicapp;
           
            // Obtenir le chemin du dll assembly
            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;

            /************************************Création de l'onglet ***********************************************/
            string nomOnglet = "BIMNG-Project Management";
            Onglet onglet = new Onglet();
            onglet.Ajouter(uicapp, nomOnglet);
            Bouton button = new Bouton();

            //********************Create panel*******************

            Ruban panneau = new Ruban();
            RibbonPanel rbAuth = panneau.Ajouter(uicapp, nomOnglet, "Authentification");
            RibbonPanel rbVerification = panneau.Ajouter(uicapp, nomOnglet, "Verification");
            RibbonPanel rbDatabase = panneau.Ajouter(uicapp, nomOnglet, "Database");
            RibbonPanel ribbonPanel = panneau.Ajouter(uicapp, nomOnglet, "Update Data");
            RibbonPanel rib_panelProprety = panneau.Ajouter(uicapp, nomOnglet, "Palette d'historiques");
            rbDatabase.Visible = false;
            ribbonPanel.Visible = false;
            rib_panelProprety.Visible = false;
            //*********************************

            PushButtonData data = new PushButtonData("Connecter", "Login", thisAssemblyPath, "ProjectManagement.CmdRevit.CmdLogin");

            data.AvailabilityClassName = "ProjectManagement.CmdRevit.Utils.AvailabilityButton";

            _button = rbAuth.AddItem(data);
            /************************************Création de bouton Extraction de données***********************************************/

            string logo = null;
            string commentaireBouton = "Get Data";

            #region Buttons

            //************* Panel Authen***********
 
            rbAuth.AddSeparator();
            button.Ajouter(rbAuth, "Logout", logo, "ProjectManagement.CmdRevit.CmdLogout", thisAssemblyPath, "Log out of server");
            button.Ajouter(rbAuth, "progessbar", logo, "ProjectManagement.CmdRevit.CmdTestProgressBar", thisAssemblyPath, "");
            //*********** Panel Verification*****************
            button.Ajouter(rbVerification, "Verif Model", logo, "ProjectManagement.CmdRevit.CmdCheckModel", thisAssemblyPath, "Vérifier si model est à jour sur cloud");
            rbVerification.AddSeparator();
            button.Ajouter(rbVerification, "Update", logo, "ProjectManagement.CmdRevit.CmdUpdate", thisAssemblyPath, "Mettre à jour data in cloud");


            //**************Panel Database******************
            //button.Ajouter(rbDatabase, "Get Data", logo, "ProjectManagement.CmdRevit.CmdGetData", thisAssemblyPath, commentaireBouton);
            //rbDatabase.AddSeparator();
            button.Ajouter(rbDatabase, "Synchronization", logo, "ProjectManagement.CmdRevit.CmdSynchronization", thisAssemblyPath, commentaireBouton);
            rbDatabase.AddSeparator();
            button.Ajouter(rbDatabase, "Vider Database", logo, "ProjectManagement.CmdRevit.CmdDeleteData", thisAssemblyPath, commentaireBouton);
            rbDatabase.AddSeparator();
            button.Ajouter(rbDatabase, "Create Version", logo, "ProjectManagement.CmdRevit.CmdCreateVersion", thisAssemblyPath, commentaireBouton);

            //************* Panel Project Management*********** 
            button.Ajouter(ribbonPanel, "Send Data", logo, "ProjectManagement.CmdRevit.CmdSendData", thisAssemblyPath, commentaireBouton);
            ribbonPanel.AddSeparator();
            button.Ajouter(ribbonPanel, "DeletedElement", logo, "ProjectManagement.CmdRevit.CmdSendDeletedElement", thisAssemblyPath, commentaireBouton);
           

            button.Ajouter(rib_panelProprety, "Afficher", logo, "ProjectManagement.ShowDockableWindow", thisAssemblyPath, "Afficher pallette de propriétés");
            Separateur.Ajouter(rib_panelProprety);
            button.Ajouter(rib_panelProprety, "Masquer", logo, "ProjectManagement.HideDockableWindow", thisAssemblyPath, "Masquer pallette de propriétés");
           
            rib_panelProprety.AddSeparator();
            button.Ajouter(rib_panelProprety, "Modification Watcher", logo, "ProjectManagement.CmdRevit.CmdModificationWatcher", thisAssemblyPath, "Masquer pallette de propriétés");
            #endregion Buttons

            #region Methods

            DockablePanelActivated(); //Method for dockable panel

            #endregion Methods

            #region Events

            //foreach (Autodesk.Windows.RibbonTab tab in Autodesk.Windows.ComponentManager.Ribbon.Tabs)
            //{
            //    if (tab.Id == "Modify")
            //    {
            //        if (_subscribed)
            //        {
            //            tab.PropertyChanged -=PanelProprety.PanelEvent;
            //            _subscribed = false;
            //        }
            //        else
            //        {
            //            tab.PropertyChanged += PanelProprety.PanelEvent;
            //            _subscribed = true;
            //        }
            //        break;
            //    }
            //}

            uicapp.ViewActivated += new EventHandler<ViewActivatedEventArgs>(onViewActivated); //for panel proprety
          

            #endregion Events

            return Result.Succeeded;
        }

        //void Application_ViewActivated( object sender, ViewActivatedEventArgs e)

        #region Events

        private void onViewActivated(object sender, ViewActivatedEventArgs e)
        {
            _doc = e.Document;

            //_uiDoc = new UIDocument(doc);
            PanelProprety._uiDoc = new UIDocument(_doc);
        }

        /* void PanelEvent(object sender, System.ComponentModel.PropertyChangedEventArgs e)
         {
             if (e.PropertyName == "Title")
             {
                 Selection selection = _uiDoc.Selection;
                 ICollection<ElementId> ids = _uiDoc.Selection.GetElementIds();

                 int n = ids.Count;
                 if (n != 0)
                 {
                 }
             }
         }*/
       
        private void DockablePanelActivated()
        {
             
            PanelProprety panelPropreties = new PanelProprety();
            _panelProprety = panelPropreties;

            
            DockablePaneId paneId = new DockablePaneId(new Guid("{D7C963CE-B7CA-426A-8D51-6E8254D21157}"));
            _uicapp.RegisterDockablePane(paneId, "Historiques", (IDockablePaneProvider)panelPropreties);
            
        }
        /* cach 2 tao voi event
        private void OnApplicationInitialized(object sender, Autodesk.Revit.DB.Events.ApplicationInitializedEventArgs e)
        {
            PanelProprety MainDockableWindow = new PanelProprety();

            _panelProprety = MainDockableWindow;

            _dpid = new DockablePaneId(new Guid("{D7C963CE-B7CA-426A-8D51-6E8254D21157}"));

            _uicapp.RegisterDockablePane(_dpid, "Historiquesfff", MainDockableWindow as IDockablePaneProvider);
        }
        */

        #endregion Events

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }

        #region Méthodes

        public void TextChangedButton()
        {
            string s = _button.ItemText; 
            _button.ItemText = s.Equals("Login") ? ("Vous êtes connecté au projet" + Environment.NewLine + _projectName) : "Login";
        }

        #endregion Méthodes
    }

    #region Command

    [Transaction(TransactionMode.ReadOnly)]
    public class ShowDockableWindow : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            DockablePaneId dpid = new DockablePaneId(
              new Guid("{D7C963CE-B7CA-426A-8D51-6E8254D21157}"));

            DockablePane dp = commandData.Application
              .GetDockablePane(dpid);

            dp.Show();

            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.ReadOnly)]
    public class HideDockableWindow : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            DockablePaneId dpid = new DockablePaneId(
              new Guid("{D7C963CE-B7CA-426A-8D51-6E8254D21157}"));

            DockablePane dp = commandData.Application
              .GetDockablePane(dpid);

            dp.Hide();
            return Result.Succeeded;
        }
    }

    #endregion Command
}