namespace ProjectManagement.Commun
{
    using Autodesk.Revit.DB;
    using ProjectManagement.Models;
    using ProjectManagement.Utils.RevitUtils;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class ModelProvider : INotifyPropertyChanged
    {
        private static ModelProvider _ins;

        public static ModelProvider Instance
        {
            get
            {
                if (_ins == null)
                    _ins = new ModelProvider();
                return _ins;
            }
            set
            {
                _ins = value;
            }
        }

        public Dictionary<string, RevitElement> DicRevitElements { get; set; }

        private Document _currentModel;

        public Document CurrentModel
        {
            get => _currentModel; set { _currentModel = value; OnPropertyChanged(); }
        }
        public IList<Level> Levels { get; set; }

        public ObservableCollection<Document> Models { get; set; } = new ObservableCollection<Document>();//Dont reset Models, exception null when disconnect

        public void Reset()
        {
            DicRevitElements = null;
            CurrentModel = null;
            Levels = null;
        }

      

        public void Update()
        {

            DicRevitElements = new Dictionary<string, RevitElement>();
            IList<Element> elements = FilterUtils.GetElementInProject(CurrentModel);

            foreach (Element element in elements)
            {
                RevitElement revitElement = new RevitElement(element);

                DicRevitElements.Add(revitElement.guid, revitElement);

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (CurrentModel != null)
                Levels = Utils.RevitUtils.ElementUtils.GetLevels(CurrentModel);
        }
    }
}
