namespace BIM4PM.Model
{
    using Autodesk.Revit.DB;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    public class RevitElementRes
    {
        public bool success { get; set; }

        public List<RevitElement> data { get; set; }
    }

    public class HistoryResParent
    {
        public bool success { get; set; }

        public HistoryResChild data { get; set; }
    }

    public class HistoryResChild
    {
        public string _id { get; set; }

        public List<History> history { get; set; }
    }

    public class HistoryByTypeChange
    {
        public DateTime date { get; set; }

        public string userName { get; set; }

        public TypeChange type { get; set; }
    }

    public enum TypeChange
    {
        CreatedOn,
        Geometry,
        Parameters,
        SharedParameters
    }

    public class RevitElement : EntityBase
    {
        [JsonProperty("project")]
        public string Project { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("guid")]
        public string Guid { get; set; }

        [JsonProperty("history")]
        public List<History> History { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("elementId")]
        public string ElementId { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("level")]
        public string Level { get; set; }

        [JsonProperty("parameters")]
        public string Parameters { get; set; }

        [JsonProperty("geometryParameters")]
        public string GeometryParameters { get; set; }

        [JsonProperty("sharedParameters")]
        public string SharedParameters { get; set; }

        [JsonProperty("worksetId")]
        public string WorksetId { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("boundingBox")]
        public string BoundingBox { get; set; }

        [JsonProperty("centroid")]
        public string Centroid { get; set; }

        [JsonProperty("typeId")]
        public string TypeId { get; set; }

        [JsonProperty("volume")]
        public string Volume { get; set; }

        public RevitElement()
        {
            History = new List<History>();
        }

        public RevitElement(Element element)
        {
        }

        public RevitElement(RevitElement project, RevitElement model, List<History> history)
        {
            Id = project.Id;
            this.Project = project.Project;
            Version = project.Version;
            Guid = project.Guid;
            this.History = history;
            Name = model.Name;
            ElementId = model.ElementId;
            Category = model.Category;
            Level = model.Level;
            Parameters = model.Parameters;
            GeometryParameters = model.GeometryParameters;
            SharedParameters = model.SharedParameters;
            WorksetId = model.WorksetId;
            Location = model.Location;
            BoundingBox = model.BoundingBox;
            Centroid = model.Centroid;
            TypeId = model.TypeId;
            Volume = model.Volume;
        }

        protected bool Equals(RevitElement other)
        {
            return string.Equals(Name, other.Name)
                && string.Equals(Category, other.Category)
                && string.Equals(Level, other.Level)
                && string.Equals(Parameters, other.Parameters)
                && string.Equals(SharedParameters, other.Parameters) //we dont compare worksetId
                && string.Equals(Location, other.Location)
                && string.Equals(BoundingBox, other.BoundingBox)
                && string.Equals(Centroid, other.Centroid)
                && string.Equals(Volume, other.Volume)
                && string.Equals(TypeId, other.TypeId);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((RevitElement)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Category != null ? Category.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Level.GetHashCode();
                hashCode = (hashCode * 397) ^ Parameters.GetHashCode();
                hashCode = (hashCode * 397) ^ SharedParameters.GetHashCode();
                hashCode = (hashCode * 397) ^ Location.GetHashCode();
                hashCode = (hashCode * 397) ^ BoundingBox.GetHashCode();
                hashCode = (hashCode * 397) ^ Centroid.GetHashCode();
                hashCode = (hashCode * 397) ^ Volume.GetHashCode();
                hashCode = (hashCode * 397) ^ TypeId.GetHashCode();
                return hashCode;
            }
        }

        public override bool Validate()
        {
            var isValid = true;
            if (string.IsNullOrWhiteSpace(Project)) isValid = false;
            if (string.IsNullOrWhiteSpace(Version)) isValid = false;
            if (string.IsNullOrWhiteSpace(Guid)) isValid = false;
            if (string.IsNullOrWhiteSpace(Name)) isValid = false;
            if (string.IsNullOrWhiteSpace(ElementId)) isValid = false;
            return isValid;
        }
    }
}
