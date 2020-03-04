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

        public  List<History> history { get; set; }
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
    public class RevitElement: EntityBase
    { 
        [JsonProperty("project")]
        public string Project { get; set; }
        [JsonProperty("version")]
        public string version { get; set; }
        [JsonProperty("guid")]
        public string guid { get; set; }
        [JsonProperty("history")]
        public List<History> history { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("elementId")]
        public string elementId { get; set; }
        [JsonProperty("category")]
        public string category { get; set; }
        [JsonProperty("level")]
        public string level { get; set; }
        [JsonProperty("parameters")]
        public string parameters { get; set; }
        [JsonProperty("geometryParameters")]
        public string geometryParameters { get; set; }
        [JsonProperty("sharedParameters")]
        public string sharedParameters { get; set; }
        [JsonProperty("worksetId")]
        public string worksetId { get; set; }
        [JsonProperty("location")]
        public string location { get; set; }
        [JsonProperty("boundingBox")]
        public string boundingBox { get; set; }
        [JsonProperty("centroid")]
        public string centroid { get; set; }
        [JsonProperty("typeId")]
        public string typeId { get; set; }
        [JsonProperty("volume")]
        public string volume { get; set; }

        

       

        public RevitElement()
        {
           
        }

        public RevitElement(Element element)
        {
            //    //if (ModelProvider.Instance.CurrentModel !=null && ProjectProvider.Instance.CurrentProject != null)
            //    //{
            //    //    project = ProjectProvider.Instance.CurrentProject._id;
            //    //    version = ProjectProvider.Instance.CurrentVersion._id;
            //        guid = element.UniqueId;
            //        name = element.Name;
            //       elementId = element.Id.IntegerValue.ToString();
            //       category = element.Category.Name;
            //      //  level = ElementUtils.GetElementLevel(ModelProvider.Instance.Levels, element);
            //    //   parameters = ParameterUtils.SerializeRevitParameters(element);
            //    //    geometryParameters = ParameterUtils.SerializeGeoParameters(element); 
            //    //    sharedParameters = ParameterUtils.SerializeSharedParameters(element, ModelProvider.Instance.CurrentModel);
            //        worksetId = element.WorksetId.ToString();
            //    //    location = ElementUtils.SerializeLocation(element);
            //    //    boundingBox = ElementUtils.SerializeBoundingBox(element.get_BoundingBox(null));
            //    //    centroid = ElementUtils.SerializePoint(ElementUtils.GetCentroid(element));
            //    //    volume = ElementUtils.GetAllSolidVolume(element).ToString();
            //    //    typeId = element.GetTypeId().ToString();
            //    //}

            }

            public RevitElement(RevitElement project, RevitElement model, List<History> history)
        {
            Id = project.Id;
            this.Project = project.Project; 
            version = project.version; 
            guid = project.guid; 
            this.history = history; 
            name = model.name; 
            elementId = model.elementId; 
            category = model.category; 
            level = model.level; 
            parameters = model.parameters; 
            geometryParameters = model.geometryParameters; 
            sharedParameters = model.sharedParameters; 
            worksetId = model.worksetId; 
            location = model.location;
            boundingBox = model.boundingBox; 
            centroid = model.centroid;
            typeId = model.typeId;
            volume = model.volume;
        }

        /// <summary>
        /// The Equals
        /// </summary>
        /// <param name="other">The other<see cref="RevitElement"/></param>
        /// <returns>The <see cref="bool"/></returns>
        protected bool Equals(RevitElement other)
        {
            return string.Equals(name, other.name)
                && string.Equals(category, other.category)
                && string.Equals(level, other.level)
                && string.Equals(parameters, other.parameters)
                && string.Equals(sharedParameters, other.parameters) //we dont compare worksetId
                && string.Equals(location, other.location)
                && string.Equals(boundingBox, other.boundingBox)
                && string.Equals(centroid, other.centroid)
                && string.Equals(volume, other.volume)
                && string.Equals(typeId, other.typeId);
        }

        /// <summary>
        /// The Equals
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((RevitElement)obj);
        }

        /// <summary>
        /// The GetHashCode
        /// </summary>
        /// <returns>The <see cref="int"/></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (name != null ? name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (category != null ? category.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ level.GetHashCode();
                hashCode = (hashCode * 397) ^ parameters.GetHashCode();
                hashCode = (hashCode * 397) ^ sharedParameters.GetHashCode();
                hashCode = (hashCode * 397) ^ location.GetHashCode();
                hashCode = (hashCode * 397) ^ boundingBox.GetHashCode();
                hashCode = (hashCode * 397) ^ centroid.GetHashCode();
                hashCode = (hashCode * 397) ^ volume.GetHashCode();
                hashCode = (hashCode * 397) ^ typeId.GetHashCode();
                return hashCode;
            }
        }

        public override bool Validate()
        {
            var isValid = true;
            if (string.IsNullOrWhiteSpace(Project)) isValid = false;
            if (string.IsNullOrWhiteSpace(version)) isValid = false;
            if (string.IsNullOrWhiteSpace(guid)) isValid = false;
            if (string.IsNullOrWhiteSpace(name)) isValid = false;
            if (string.IsNullOrWhiteSpace(elementId)) isValid = false;
            return isValid;
        }
    }
}
