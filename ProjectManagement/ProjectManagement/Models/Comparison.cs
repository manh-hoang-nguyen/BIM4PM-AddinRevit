using System;

namespace BIM4PM.UI.Models
{
    public class Comparison
    {
        public string _id { get; set; }
        public string projectId { get; set; }
        public string guid { get; set; }
       
        public Data[] data { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public int __v { get; set; }
    }
    public class JsonToPostComparison
    {
       
        public string guid { get; set; }
        public string author { get; set; }
        public string comment { get; set; }
        public string typeId { get; set; }
        public double solidVolume { get; set; }
        public LocationElement location { get; set; }
        public BoundingBoxElement boundingBox { get; set; }
        public CentroidElement centroidElement { get; set; }
    }
    public class Data
    {
        public string _id { get; set; }
        public int v { get; set; }
        public string typeId { get; set; }
        public double solidVolume { get; set; }
        public LocationElement location { get; set; }
        public BoundingBoxElement boundingBox { get; set; }
        public CentroidElement centroidElement { get; set; }
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
}