using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagement.Commun;
using ProjectManagement.Models;

namespace ProjectManagement.CmdRevit.Utils
{
   public class Compare
    {
        public static void GetListGuidElement()
        {
             
            IEnumerable<string> guid_version_1 = from e in ComparisonList.ComparisonInDatabase
                                                 select e.guid;
            IEnumerable<string> guid_version_2 = from e in ComparisonList.ComparisonInModel
                                                 select e.guid;
           GuidList.guid_deletedElement = guid_version_1.Where(x => !guid_version_2.Contains(x));

            GuidList.guid_newElement = guid_version_2.Where(x => !guid_version_1.Contains(x));

              GuidList.guid_ElementToExamine = guid_version_2.Where(x => guid_version_1.Contains(x));

          

            /*
            DataList.DeletedElement = (from e in DataList.DataInDatabase
                                       where guid_deletedElement.Contains(e.guid)
                                       select e);
            DataList.NewElement = (from e in DataList.DataModel
                                   where guid_newElement.Contains(e.guid)
                                   select e);
            DataList.ElementToExamine = (from e in DataList.DataModel
                                         where guid_ElementToExamine.Contains(e.guid)
                                         select e);
            DataList.guid_deletedElement = guid_deletedElement;
            */
        }
        public static bool CompareElements(Comparison e1, Comparison e2)
        {
            bool compare=false;
            int i1 = e1.data.Length;
            int i2 = e2.data.Length;

            bool typeId = (e1.data[i1 - 1].typeId == e2.data[i2 - 1].typeId) ? true : false;

            bool solideVolume = (e1.data[i1 - 1].solidVolume == e2.data[i2 - 1].solidVolume) ? true : false;

            bool location;

            bool boundingBox;

            bool centroid;

            //compare location
            switch (e2.data[i2 - 1].location.LocationType)
            {
                case "Point":
                    location = (
                      CompareTwoPoint(  e1.data[i1 - 1].location.Location_point01,e2.data[i2-1].location.Location_point01)
                        ) ? true : false;
                    break;

                case "Curve":
                    location = (
                     CompareTwoPoint(e1.data[i1 - 1].location.Location_point01, e2.data[i2 - 1].location.Location_point01)
                     && CompareTwoPoint(e1.data[i1 - 1].location.Location_point02, e2.data[i2 - 1].location.Location_point02)
                       ) ? true : false;
                    break;
                default:
                    location = true;
                    break;
            }
            //compare boundingBox
            switch (e2.data[i2 - 1].boundingBox.status)
            {
                case "1":
                         boundingBox = (
                             CompareTwoPoint(e1.data[i1 - 1].boundingBox.BoundingBox_point01, e2.data[i2 - 1].boundingBox.BoundingBox_point01)
                             && CompareTwoPoint(e1.data[i1 - 1].boundingBox.BoundingBox_point02, e2.data[i2 - 1].boundingBox.BoundingBox_point02)
                               ) ? true : false;
                    break;

                default: boundingBox = true;
                    break;
            }
            //compaire centroid

            switch (e2.data[i2 - 1].centroidElement.status)
            {
                case "1":
                    centroid = (
                        CompareTwoPoint(e1.data[i1 - 1].centroidElement.centroid, e2.data[i2 - 1].centroidElement.centroid) 
                                ) ? true : false;
                    break;

                default:
                    centroid = true;
                    break;
            }

            compare = (typeId&& solideVolume && location && boundingBox && centroid) ? true : false;

            return compare;
        }
        public static bool CompareTwoPoint (PointXYZ p1,PointXYZ p2)
        {
           return   (  p1.X == p2.X && p1.Y == p2.Y && p1.Z == p2.Z  ) ? true : false;
             
        }
    }
}
