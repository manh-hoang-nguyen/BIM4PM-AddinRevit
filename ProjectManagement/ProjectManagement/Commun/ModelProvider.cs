namespace BIM4PM.UI.Commun
{
    using Autodesk.Revit.DB;
    using GalaSoft.MvvmLight;
    using BIM4PM.UI.Models;
    using BIM4PM.UI.Utils.RevitUtils;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class ModelProvider : ObservableObject, IConnectObserver 
    {
        public ModelProvider()
        {
            DicRevitElements = new Dictionary<string, RevitElement>();
            AuthProvider.Instance.ConnectionChanged += (s, e) =>  Reset();
           
        }

        //public static ModelProvider Instance { get; } = new ModelProvider();


        public static Dictionary<string, RevitElement> DicRevitElements { get; set; }

        private Document _currentModel;

        public Document CurrentModel
        {
            get => _currentModel;
            set
            {
                if(Set(()=> CurrentModel, ref _currentModel, value))
                {
                   if(CurrentModel !=null)
                        Levels = Utils.RevitUtils.ElementUtils.GetLevels(CurrentModel);
                }
            }
        }
        public static IList<Level> Levels { get; set; }

        public static ObservableCollection<Document> Models { get; set; } = new ObservableCollection<Document>();//Dont reset Models, exception null when disconnect


        public void Update()
        {

            
            IList<Element> elements = FilterUtils.GetElementInProject(CurrentModel);

            foreach (Element element in elements)
            {
                RevitElement revitElement = new RevitElement(element);

                DicRevitElements.Add(revitElement.guid, revitElement);

            }
        }

        public void Update(IConnect connect)
        {
            
            if (ProjectModelConnect.IsConnected == true)
            {
                Document doc = ProjectModelConnect.SelectedRevitModel;

                IList<Element> elements = FilterUtils.GetElementInProject(doc);

                foreach (Element element in elements)
                {
                    RevitElement revitElement = new RevitElement(element);

                    DicRevitElements.Add(revitElement.guid, revitElement);

                }

                Levels = Utils.RevitUtils.ElementUtils.GetLevels(doc);
            }
            else
            {
                Reset();
            }
        }

        private void Reset()
        {
            if((AuthProvider.Instance.IsConnected == false))
            {
                DicRevitElements = null;
                CurrentModel = null;
                Levels = null;
            }
          
        }
    }
}
