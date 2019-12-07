namespace ProjectManagement.Commun
{
    using Autodesk.Revit.DB;
    using ProjectManagement.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CompareProvider
    {
        private static CompareProvider _ins;

        public static CompareProvider Instance
        {
            get
            {
                if (_ins == null)
                    _ins = new CompareProvider();
                return _ins;
            }
            set
            {
                _ins = value;
            }
        }
        public IList<ElementId> ElementToExamine { get; set; } = new List<ElementId>();

        public Dictionary<string, RevitElement> ElementToSynchonize { get; set; } = new Dictionary<string, RevitElement>();

        public IEnumerable<string> Deleted { get; set; } = new List<string>();

        public IEnumerable<string> New { get; set; } = new List<string>();

        public ICollection<string> Modified { get; set; } = new List<string>();

        public ICollection<string> Same { get; set; } = new List<string>();

        public bool IsUpToDate()
        {
            if (Modified.Count == 0 && Deleted.ToList().Count == 0 && New.ToList().Count == 0 && Same.Count != 0) return true;
            else return false;
        }

        public void Reset()
        {
            ElementToCompare = new List<ElementId>();
            Deleted = new List<string>();
            Modified = new List<string>();
            New = new List<string>();
            Same = new List<string>();
            ElementToSynchonize = new Dictionary<string, RevitElement>();
        }

        public void Execute()
        {

            Modified = new List<string>();
            Same = new List<string>();
            Deleted = new List<string>();
            New = new List<string>();

            Deleted = ProjectProvider.Instance.DicRevitElements.Keys.Except(ModelProvider.Instance.DicRevitElements.Keys);
            New = ModelProvider.Instance.DicRevitElements.Keys.Except(ProjectProvider.Instance.DicRevitElements.Keys);

            var EleToCompareGuid = ModelProvider.Instance.DicRevitElements.Keys.Except(New);
            Parallel.ForEach(EleToCompareGuid, guid =>
            {
                History history = new History();
                RevitElement current = ModelProvider.Instance.DicRevitElements[guid];
                RevitElement previous = ProjectProvider.Instance.DicRevitElements[guid];
                bool geometryIsSame = GeometryCompare(current, previous);
                bool revitParameterIsSame = ParameterCompare(current, previous);
                bool sharedParameterIsSame = SharedParameterCompare(current, previous);

                if (geometryIsSame && revitParameterIsSame && sharedParameterIsSame)
                {
                    Same.Add(guid);
                }
                else
                {
                    Modified.Add(guid);
                    if (!geometryIsSame) history.geometryChange = true;
                    if (!revitParameterIsSame) history.parameterChange = true;
                    if (!sharedParameterIsSame) history.sharedParameterChange = true;
                    RevitElement revitElement = new RevitElement(previous, current, new List<History> { history });

                    ElementToSynchonize.Add(guid, revitElement);
                }
            });
        }

        private bool GeometryCompare(RevitElement current, RevitElement previous)
        {
            bool result = false;

            if (current.boundingBox == previous.boundingBox
                && current.centroid == previous.centroid
                && current.location == previous.location
                && current.geometryParameters == previous.geometryParameters
                && current.volume == previous.volume) result = true;

            return result;
        }

        private bool ParameterCompare(RevitElement current, RevitElement previous)
        {
            bool result = false;

            if (current.parameters == previous.parameters) result = true;

            return result;
        }

        private bool SharedParameterCompare(RevitElement current, RevitElement previous)
        {
            bool result = false;

            if (current.sharedParameters == previous.sharedParameters) result = true;

            return result;
        }
    }
}
