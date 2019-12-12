namespace ProjectManagement
{
    using Autodesk.Revit.Attributes;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.DB.Events;
    using Autodesk.Revit.UI;
    using Autodesk.Revit.UI.Events;
    using ProjectManagement.Commun;
    using ProjectManagement.FormInterface;
    using ProjectManagement.Models;
    using ProjectManagement.Tools;
    using ProjectManagement.Tools.Discussion;
    using ProjectManagement.Tools.History;
    using ProjectManagement.Tools.Project;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Windows;

    internal class App : IExternalApplication
    {
        public static App Instance { get; private set; }

        public static ModelRequestHandler ModelHandler { get; set; }

        public static ExternalEvent ModelEvent { get; set; }

        public static HistoryRequestHandler HistoryHandler { get; set; }

        public static ExternalEvent HistoryEvent { get; set; }

        public static DiscussionRequestHandler DiscussionHandler { get; set; }

        public static ExternalEvent DiscussionEvent { get; set; }

        public static PaletteMainView PaletteWindow { get; set; }

        public static DocumentSet DocumentSet { get; set; }

        public static string _projectName;

        internal static App _app = null;

        private RibbonItem _button;

        /// <summary>
        /// Provide access to singleton class instance.
        /// </summary>
        private PanelProprety _panelProprety = null;

        public static UIControlledApplication _uicapp = null;

        private Document _doc;

        /// <summary>
        /// The OnStartup
        /// </summary>
        /// <param name="uicapp">The uicapp<see cref="UIControlledApplication"/></param>
        /// <returns>The <see cref="Result"/></returns>
        public Result OnStartup(UIControlledApplication uicapp)
        {
            Instance = this;
            ModelHandler = new ModelRequestHandler();
            ModelEvent = ExternalEvent.Create(ModelHandler);
            HistoryHandler = new HistoryRequestHandler();
            HistoryEvent = ExternalEvent.Create(HistoryHandler);
            DiscussionHandler = new DiscussionRequestHandler();
            DiscussionEvent = ExternalEvent.Create(DiscussionHandler);

            PaletteUtilities.RegisterPalette(uicapp);

            

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


            button.Ajouter(rbAuth, "Afficher", logo, "ProjectManagement.ShowDockableWindow", thisAssemblyPath, "Afficher pallette de propriétés");
            Separateur.Ajouter(rib_panelProprety);
            button.Ajouter(rib_panelProprety, "Masquer", logo, "ProjectManagement.HideDockableWindow", thisAssemblyPath, "Masquer pallette de propriétés");

            rib_panelProprety.AddSeparator();
            button.Ajouter(rib_panelProprety, "Modification Watcher", logo, "ProjectManagement.CmdRevit.CmdModificationWatcher", thisAssemblyPath, "Masquer pallette de propriétés");


            DockablePanelActivated(); //Method for dockable panel



            uicapp.ViewActivated += new EventHandler<ViewActivatedEventArgs>(onViewActivated); //for panel proprety 
            uicapp.ControlledApplication.DocumentOpened += OnDocumentOpened;
            uicapp.ControlledApplication.DocumentCreated += OnDocumentCreated;
            uicapp.ControlledApplication.DocumentClosing += OnDocumentClosing;
            uicapp.ControlledApplication.DocumentClosed += OnDocumentClosed;
            uicapp.ControlledApplication.DocumentSaved += OnDocumentSave;
            uicapp.ControlledApplication.DocumentSynchronizedWithCentral += OnDocumentSynchronized;
            uicapp.ControlledApplication.DocumentChanged += OnDocumentChanged;

            return Result.Succeeded;
        }

       

        /// <summary>
        /// The OnDocumentChanged: Update list revit element in model
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="args">The args<see cref="DocumentChangedEventArgs"/></param>
        private void OnDocumentChanged(object sender, DocumentChangedEventArgs args)
        {
            Document doc = args.GetDocument();
            if (ModelProvider.Instance.CurrentModel == null
                || ModelProvider.Instance.DicRevitElements == null)
                return;

            if (doc.Title == ModelProvider.Instance.CurrentModel.Title)
            {
                List<ElementId> elementIds = new List<ElementId>();

                foreach (ElementId id in args.GetAddedElementIds())
                {
                    Element e = doc.GetElement(id);
                    if (null != e.Category
                            && 0 < e.Parameters.Size
                            && (e.Category.HasMaterialQuantities))
                    {
                        RevitElement revitElement = new RevitElement(e);

                        if (ModelProvider.Instance.DicRevitElements.ContainsKey(e.UniqueId))
                        {
                            ModelProvider.Instance.DicRevitElements.Remove(e.UniqueId);
                            ModelProvider.Instance.DicRevitElements.Add(e.UniqueId, revitElement);
                        }

                        else
                            ModelProvider.Instance.DicRevitElements.Add(e.UniqueId, revitElement);
                    } 
                }
                foreach (ElementId id in args.GetDeletedElementIds())
                {
                    foreach (RevitElement item in ModelProvider.Instance.DicRevitElements.Values)
                    {
                        if (item.elementId == id.ToString())
                        {
                            ModelProvider.Instance.DicRevitElements.Remove(item.guid);
                            break;
                        }
                    }


                }
                foreach (ElementId id in args.GetModifiedElementIds())
                {

                    Element e = doc.GetElement(id);
                    if (null != e.Category
                          && 0 < e.Parameters.Size
                          && (e.Category.HasMaterialQuantities))
                    {
                        RevitElement revitElement = new RevitElement(e);

                        if (ModelProvider.Instance.DicRevitElements.ContainsKey(e.UniqueId))
                        {
                            ModelProvider.Instance.DicRevitElements.Remove(e.UniqueId);
                            ModelProvider.Instance.DicRevitElements.Add(e.UniqueId, revitElement);
                        }
                    }

                }
            }
        }

        /// <summary>
        /// The OnDocumentSynchronized
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="args">The args<see cref="DocumentSynchronizedWithCentralEventArgs"/></param>
        private void OnDocumentSynchronized(object sender, DocumentSynchronizedWithCentralEventArgs args)
        {
        }

        /// <summary>
        /// The onViewActivated
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="ViewActivatedEventArgs"/></param>
        private void onViewActivated(object sender, ViewActivatedEventArgs e)
        {
            _doc = e.Document;

            //_uiDoc = new UIDocument(doc);
            PanelProprety._uiDoc = new UIDocument(_doc);
        }

        /// <summary>
        /// The OnDocumentSave
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="args">The args<see cref="DocumentSavedEventArgs"/></param>
        private void OnDocumentSave(object sender, DocumentSavedEventArgs args)
        {
        }

        /// <summary>
        /// The OnDocumentCreated
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="args">The args<see cref="DocumentCreatedEventArgs"/></param>
        private static void OnDocumentCreated(object sender, DocumentCreatedEventArgs args)
        {

            ModelProvider.Instance.Models.Add(args.Document);
        }

        /// <summary>
        /// When DocumentOpened, update list model
        /// </summary>
        /// <param name="source">The source<see cref="object"/></param>
        /// <param name="args">The args<see cref="DocumentOpenedEventArgs"/></param>
        private static void OnDocumentOpened(object source, DocumentOpenedEventArgs args)
        {

            ModelProvider.Instance.Models.Add(args.Document);
        }

        /// <summary>
        /// When DocumentClosing, update list models and disconnect to projects if models is current models
        /// </summary>
        /// <param name="source">The source<see cref="object"/></param>
        /// <param name="args">The args<see cref="DocumentClosingEventArgs"/></param>
        private static void OnDocumentClosing(object source, DocumentClosingEventArgs args)
        {  
            //if (args.Document.Title == ModelProvider.Instance.CurrentModel.Title) AuthProvider.Instance.Disconnect();
            if (ModelProvider.Instance.CurrentModel != null && args.Document.Title == ModelProvider.Instance.CurrentModel.Title)
            {
                MessageBox.Show("Please DISCONNECT to project before closing the model.");
                args.Cancel();
                return;
            }
            var docToRemove = ModelProvider.Instance.Models.Where(x => x.Title == args.Document.Title);
            if (docToRemove != null) ModelProvider.Instance.Models.Remove(docToRemove.FirstOrDefault());
        }
        private void OnDocumentClosed(object sender, DocumentClosedEventArgs args)
        {
             
           
        }
        /// <summary>
        /// The DockablePanelActivated
        /// </summary>
        private void DockablePanelActivated()
        {

            PanelProprety panelPropreties = new PanelProprety();
            _panelProprety = panelPropreties;
            DockablePaneId paneId = new DockablePaneId(new Guid("{D7C963CE-B7CA-426A-8D51-6E8254D21157}"));
            _uicapp.RegisterDockablePane(paneId, "Historiques", (IDockablePaneProvider)panelPropreties);
        }

        /// <summary>
        /// The OnShutdown
        /// </summary>
        /// <param name="a">The a<see cref="UIControlledApplication"/></param>
        /// <returns>The <see cref="Result"/></returns>
        public Result OnShutdown(UIControlledApplication a)
        {
            a.ControlledApplication.DocumentOpened -= OnDocumentOpened;
            a.ControlledApplication.DocumentCreated -= OnDocumentCreated;
            a.ControlledApplication.DocumentClosing -= OnDocumentClosing;
            a.ControlledApplication.DocumentClosed -= OnDocumentClosed; 
            a.ControlledApplication.DocumentSaved -= OnDocumentSave;
            a.ControlledApplication.DocumentSynchronizedWithCentral -= OnDocumentSynchronized;
            a.ControlledApplication.DocumentChanged -= OnDocumentChanged;
            return Result.Succeeded;
        }

        /// <summary>
        /// The TextChangedButton
        /// </summary>
        public void TextChangedButton()
        {
            string s = _button.ItemText;
            _button.ItemText = s.Equals("Login") ? ("Vous êtes connecté au projet" + Environment.NewLine + _projectName) : "Login";
        }
    }

    [Transaction(TransactionMode.ReadOnly)]
    public class ShowDockableWindow : IExternalCommand
    {
        /// <summary>
        /// The Execute
        /// </summary>
        /// <param name="commandData">The commandData<see cref="ExternalCommandData"/></param>
        /// <param name="message">The message<see cref="string"/></param>
        /// <param name="elements">The elements<see cref="ElementSet"/></param>
        /// <returns>The <see cref="Result"/></returns>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            DockablePaneId dpid = new DockablePaneId(new Guid(Properties.Resources.PaletteGuid));

            DockablePane dp = commandData.Application
              .GetDockablePane(dpid);

            dp.Show();

            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.ReadOnly)]
    public class HideDockableWindow : IExternalCommand
    {
        /// <summary>
        /// The Execute
        /// </summary>
        /// <param name="commandData">The commandData<see cref="ExternalCommandData"/></param>
        /// <param name="message">The message<see cref="string"/></param>
        /// <param name="elements">The elements<see cref="ElementSet"/></param>
        /// <returns>The <see cref="Result"/></returns>
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
}
