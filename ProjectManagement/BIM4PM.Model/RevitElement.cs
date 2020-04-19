namespace BIM4PM.Model
{
    using Autodesk.Revit.DB;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    

    public class RevitElement : EntityBase
    {
        [JsonProperty("guid")]
        public string Guid { get; set; }

        [JsonProperty("versionId")]
        public string VersionId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("elementId")]
        public string ElementId { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("level")]
        public string Level { get; set; }

        [JsonProperty("modifications")]
        public Modifications Modifications { get; set; }

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

        public RevitElement(Element element)
        {
        }

        public RevitElement(RevitElement project, RevitElement model, List<History> history)
        {
            Id = project.Id;
            VersionId = project.VersionId;
            Guid = project.Guid;
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

        public RevitElement()
        {
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
    }
}
