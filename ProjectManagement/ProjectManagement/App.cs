namespace ProjectManagement
{
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

        private PanelProprety _panelProprety = null;

        public static UIControlledApplication _uicapp = null;

        private Document _doc;

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
            string nomOnglet = "Manh Hoang";
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


            PushButtonData login = new PushButtonData("Connecter", "Login", thisAssemblyPath, "ProjectManagement.CmdRevit.CmdLogin");
            login.AvailabilityClassName = "ProjectManagement.AvailabilityButtonLogin";
            _button = rbAuth.AddItem(login);

            rbAuth.AddSeparator();

            PushButtonData logout = new PushButtonData("Logout", "Logout", thisAssemblyPath, "ProjectManagement.CmdRevit.CmdLogout");
            logout.AvailabilityClassName = "ProjectManagement.AvailabilityButtonLogout";
            rbAuth.AddItem(logout);

            PushButtonData Test = new PushButtonData("Test", "Test", thisAssemblyPath, "ProjectManagement.CmdRevit.CmdGetData");
           
            rbAuth.AddItem(Test);


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

        private void OnDocumentSynchronized(object sender, DocumentSynchronizedWithCentralEventArgs args)
        {
        }

        private void onViewActivated(object sender, ViewActivatedEventArgs e)
        {
            _doc = e.Document;

            //_uiDoc = new UIDocument(doc);
            PanelProprety._uiDoc = new UIDocument(_doc);
        }

        private void OnDocumentSave(object sender, DocumentSavedEventArgs args)
        {
        }

        private static void OnDocumentCreated(object sender, DocumentCreatedEventArgs args)
        {

            ModelProvider.Instance.Models.Add(args.Document);
        }

        private static void OnDocumentOpened(object source, DocumentOpenedEventArgs args)
        {

            ModelProvider.Instance.Models.Add(args.Document);
        }

        private static void OnDocumentClosing(object source, DocumentClosingEventArgs args)
        {
            //if (args.Document.Title == ModelProvider.Instance.CurrentModel.Title) AuthProvider.Instance.Disconnect();
            if (ModelProvider.Instance.CurrentModel != null && args.Document.Title == ModelProvider.Instance.CurrentModel.Title)
            {

                AuthProvider.Instance.Logout();
            }
            var docToRemove = ModelProvider.Instance.Models.Where(x => x.Title == args.Document.Title);
            if (docToRemove != null) ModelProvider.Instance.Models.Remove(docToRemove.FirstOrDefault());
        }

        private void OnDocumentClosed(object sender, DocumentClosedEventArgs args)
        {
        }

        private void DockablePanelActivated()
        {

            PanelProprety panelPropreties = new PanelProprety();
            _panelProprety = panelPropreties;
            DockablePaneId paneId = new DockablePaneId(new Guid("{D7C963CE-B7CA-426A-8D51-6E8254D21157}"));
            _uicapp.RegisterDockablePane(paneId, "Historiques", (IDockablePaneProvider)panelPropreties);
        }

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

        public void TextChangedButton()
        {
            string s = _button.ItemText;
            _button.ItemText = s.Equals("Login") ? ("Vous êtes connecté au projet" + Environment.NewLine + _projectName) : "Login";
        }
    }

    public class AvailabilityButtonLogin : IExternalCommandAvailability
    {
        public bool IsCommandAvailable(UIApplication a, CategorySet b)
        {
            if (AuthProvider.Instance.IsAuthenticated == false)
            {
                return true;
            }
            else return false;
        }
    }

    public class AvailabilityButtonLogout : IExternalCommandAvailability
    {
        public bool IsCommandAvailable(UIApplication a, CategorySet b)
        {
            if (AuthProvider.Instance.IsAuthenticated == false)
            {
                return false;
            }
            else return true;
        }
    }
}
