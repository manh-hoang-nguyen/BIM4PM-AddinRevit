namespace BIM4PM.UI
{
    using Autodesk.Revit.DB;
    using Autodesk.Revit.DB.Events;
    using Autodesk.Revit.UI;
    using Autodesk.Revit.UI.Events;
    using BIM4PM.UI.Commun;
    
    using BIM4PM.UI.Models;
    using BIM4PM.UI.Tools;
    using BIM4PM.UI.Tools.Discussion;
    using BIM4PM.UI.Tools.History;
    using BIM4PM.UI.Tools.Project;
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
         
        private RibbonItem _button;
         
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


            PushButtonData login = new PushButtonData("Connecter", "Login", thisAssemblyPath, "BIM4PM.UI.CmdRevit.CmdLogin");
            login.AvailabilityClassName = "BIM4PM.UI.AvailabilityButtonLogin";
            _button = rbAuth.AddItem(login);

            rbAuth.AddSeparator();

            PushButtonData logout = new PushButtonData("Logout", "Logout", thisAssemblyPath, "BIM4PM.UI.CmdRevit.CmdLogout");
            logout.AvailabilityClassName = "BIM4PM.UI.AvailabilityButtonLogout";
            rbAuth.AddItem(logout);

            PushButtonData Test = new PushButtonData("Test", "Test", thisAssemblyPath, "BIM4PM.UI.CmdRevit.CmdGetData");
           
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
            if (ProjectModelConnect.SelectedRevitModel == null
                || ModelProvider.DicRevitElements == null)
                return;

            if (doc.Title == ProjectModelConnect.SelectedRevitModel.Title)
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

                        if (ModelProvider.DicRevitElements.ContainsKey(e.UniqueId))
                        {
                            ModelProvider.DicRevitElements.Remove(e.UniqueId);
                            ModelProvider.DicRevitElements.Add(e.UniqueId, revitElement);
                        }

                        else
                            ModelProvider.DicRevitElements.Add(e.UniqueId, revitElement);
                    }
                }
                foreach (ElementId id in args.GetDeletedElementIds())
                {
                    foreach (RevitElement item in ModelProvider.DicRevitElements.Values)
                    {
                        if (item.elementId == id.ToString())
                        {
                            ModelProvider.DicRevitElements.Remove(item.guid);
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

                        if (ModelProvider.DicRevitElements.ContainsKey(e.UniqueId))
                        {
                            ModelProvider.DicRevitElements.Remove(e.UniqueId);
                            ModelProvider.DicRevitElements.Add(e.UniqueId, revitElement);
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

            
        }

        private void OnDocumentSave(object sender, DocumentSavedEventArgs args)
        {
        }

        private static void OnDocumentCreated(object sender, DocumentCreatedEventArgs args)
        {

            ModelProvider.Models.Add(args.Document);
        }

        private static void OnDocumentOpened(object source, DocumentOpenedEventArgs args)
        {

            ModelProvider.Models.Add(args.Document);
        }

        private static void OnDocumentClosing(object source, DocumentClosingEventArgs args)
        {
            //if (args.Document.Title == ModelProvider.Instance.CurrentModel.Title) AuthProvider.Instance.Disconnect();
            if (ProjectModelConnect.SelectedRevitModel != null && args.Document.Title == ProjectModelConnect.SelectedRevitModel.Title)
            {

                AuthProvider.Instance.Logout();
            }
            var docToRemove = ModelProvider.Models.Where(x => x.Title == args.Document.Title);
            if (docToRemove != null) ModelProvider.Models.Remove(docToRemove.FirstOrDefault());
        }

        private void OnDocumentClosed(object sender, DocumentClosedEventArgs args)
        {
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

       
    }

    public class AvailabilityButtonLogin : IExternalCommandAvailability
    {
        public bool IsCommandAvailable(UIApplication a, CategorySet b)
        {
            if (Auth.IsAuthenticated == false)
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
            if (Auth.IsAuthenticated == false)
            {
                return false;
            }
            else return true;
        }
    }
}
