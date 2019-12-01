using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjectManagement
{
   public class DataContainer
    {
        public string versionChange { get; set; }

        public string auteur { get; set; }
        public string comment { get; set; }
        public List<Data> dataBody { get; set; }
    }

    public class Data
    {
         

        public string _id { get; set; }
        public string guid { get; set; }
        public string elementId { get; set; }
        public string status { get; set; }
        public Version[] version { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public int __v { get; set; }

    }
    }


    public class Version
    { 
    public LocationElement location { get; set; }
    public BoundingBoxElement boundingBox { get; set; }
    public CentroidElement centroidElement { get; set; }
    public string _id { get; set; }
    public int v { get; set; }
    public string identifiant { get; set; }
    public string level { get; set; }
    public string category { get; set; }
    public string name { get; set; }
    public string volume { get; set; }
    public string surface { get; set; }
    public string typeId { get; set; }
    public double solidVolume { get; set; } 
}


    public class LocationElement
    {

        public string LocationType { get; set; }

        public PointXYZ Location_point01 { get; set; }
        
        public PointXYZ Location_point02 { get; set; }
    }
    
    public class BoundingBoxElement
    {
            
        public string status { get; set; } 
        public PointXYZ BoundingBox_point01 { get; set; } 
        public PointXYZ BoundingBox_point02 { get; set; }
    }
    
    public class CentroidElement
    {
        
        public string status { get; set; } 
        public PointXYZ centroid { get; set; }
    }
    public class PointXYZ
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }


