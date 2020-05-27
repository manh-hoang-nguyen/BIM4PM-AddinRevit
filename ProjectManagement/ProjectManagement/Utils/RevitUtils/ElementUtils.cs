namespace BIM4PM.UI.Utils.RevitUtils
{
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class ElementUtils
    {
        /// <summary>
        /// The GetBoundingBox
        /// </summary>
        /// <param name="e">The e<see cref="Element"/></param>
        public static void GetBoundingBox(Element e)
        {
            BoundingBoxXYZ box = e.get_BoundingBox(null);
        }

        /// <summary>
        /// The SerializeLocation 
        /// </summary>
        /// <param name="e">The e<see cref="Element"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string SerializeLocation(Element e)
        {
            string location = string.Empty;

            Location loc = e.Location;

            if (loc == null)
            {

                return location;
            }

            else
            {
                LocationPoint pt = loc as LocationPoint;
                if (pt != null)
                {
                    //XYZ pt1 = pt.Point;
                    // special cases.
                    //if ((e.Category.Id.IntegerValue == (int)BuiltInCategory.OST_Columns) ||
                    //    (e.Category.Id.IntegerValue == (int)BuiltInCategory.OST_StructuralColumns))
                    //{
                    //    // in this case, get the Z value from the 
                    //    var offset = e.get_Parameter(BuiltInParameter.FAMILY_BASE_LEVEL_OFFSET_PARAM);

                    //    if ((e.LevelId != ElementId.InvalidElementId) && (offset != null))
                    //    {
                    //        Level levPt1 = lookupLevel(e, pt1);
                    //        double newZ = levPt1.Elevation + offset.AsDouble();
                    //        pt1 = new XYZ(pt1.X, pt1.Y, newZ);
                    //    }
                    //}
                     
                    location = SerializePoint(pt.Point);
                     
                }
                else
                {

                    LocationCurve crv = loc as LocationCurve;
                    if (crv != null)
                    {
                        if (crv.Curve.IsBound)
                        {
                            XYZ p1 = crv.Curve.GetEndPoint(0);
                            XYZ p2 = crv.Curve.GetEndPoint(1);
                            location = SerializePoint(p1) + ";" + SerializePoint(p2);
                        }
                    }

                }
                 
                return location;
            }
        }
        /// <summary>
        /// The GetAllSolidVolume
        /// </summary>
        /// <param name="e">The e<see cref="Element"/></param>
        /// <returns>The <see cref="double"/></returns>
        public static double GetAllSolidVolume(Element e)
        {
            double volume = 0;
            volume = GetVolumeSolidCase1(e);
            if (volume == 0) volume = GetVolumeSolidCase2(e);
            return volume;
        }

        /// <summary>
        /// The GetVolumeSolidCase1
        /// </summary>
        /// <param name="e">The e<see cref="Element"/></param>
        /// <returns>The <see cref="double"/></returns>
        private static double GetVolumeSolidCase1(Element e)
        {
            Options opt = new Options();
            opt.ComputeReferences = true;
            List<Solid> solids = new List<Solid>();
            double volume = 0;

            // Get geometry element of the selected element
            GeometryElement geoElement = e.get_Geometry(opt);
            if (geoElement == null) return volume;
            // Get geometry object
            foreach (GeometryObject geoObject in geoElement)
            {
                GeometryInstance instance = geoObject as GeometryInstance;
                if (null != instance)
                {
                    foreach (GeometryObject instObj in instance.SymbolGeometry)
                    {
                        Solid solid = instObj as Solid;
                        solids.Add(solid);
                    }
                }
            }
            //Remove solide n'a pas de centroid
            for (int i = 0; i < solids.Count; i++)
            {
                if (solids[i] == null)
                {
                    solids.RemoveAt(i);
                }
            }
            //Calcul volume
            foreach (Solid s in solids)
            {
                try
                {
                    volume += s.Volume;
                }
                catch
                {
                    volume = 0;
                }
            }
            return volume;
        }

        /// <summary>
        /// The GetVolumeSolidCase2
        /// </summary>
        /// <param name="e">The e<see cref="Element"/></param>
        /// <returns>The <see cref="double"/></returns>
        private static double GetVolumeSolidCase2(Element e)
        {
            Options opt = new Options();
            opt.ComputeReferences = true;
            List<Solid> solids = new List<Solid>();
            double volume = 0;

            // Get geometry element of the selected element
            GeometryElement geoElement = e.get_Geometry(opt);
            if (geoElement == null) return volume;
            // Get geometry object
            foreach (GeometryObject geoObject in geoElement)
            {
                Solid solid = geoObject as Solid;
                solids.Add(solid);
            }
            //Remove solide n'a pas de centroid
            for (int i = 0; i < solids.Count; i++)
            {
                if (solids[i] == null)
                {
                    solids.RemoveAt(i);
                }
            }
            //Calcul volume
            foreach (Solid s in solids)
            {
                try
                {
                    volume += s.Volume;
                }
                catch
                {
                    volume = 0;
                }
            }
            return volume;
        }

        /// <summary>
        /// The SelectionElememnt
        /// </summary>
        /// <param name="uidoc">The uidoc<see cref="UIDocument"/></param>
        public static void SelectionElememnt(UIDocument uidoc)
        {

            Reference reference = uidoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element);
            Element element = uidoc.Document.GetElement(reference);
        }

        /// <summary>
        /// The SerializePoint
        /// </summary>
        /// <param name="pt">The pt<see cref="XYZ"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string SerializePoint(XYZ pt)
        {
            if (pt == null) return String.Empty;
            return pt.X.ToString(CultureInfo.InvariantCulture) + ";" + pt.Y.ToString(CultureInfo.InvariantCulture) + ";" + pt.Z.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// The SerializeBoundingBox
        /// </summary>
        /// <param name="box">The box<see cref="BoundingBoxXYZ"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string SerializeBoundingBox(BoundingBoxXYZ box)
        {
            if (box == null) return String.Empty;

            return box.Min.X.ToString(CultureInfo.InvariantCulture) + ";" + box.Min.Y.ToString(CultureInfo.InvariantCulture) + ";" + box.Min.Z.ToString(CultureInfo.InvariantCulture) + ";" +
                box.Max.X.ToString(CultureInfo.InvariantCulture) + ";" + box.Max.Y.ToString(CultureInfo.InvariantCulture) + ";" + box.Max.Z.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// The DeserializeBoundingBox
        /// </summary>
        /// <param name="input">The input<see cref="string"/></param>
        /// <returns>The <see cref="BoundingBoxXYZ"/></returns>
        public static BoundingBoxXYZ DeserializeBoundingBox(string input)
        {
            if (String.IsNullOrEmpty(input)) return null;

            string[] values = input.Split(';');

            if (values.Length == 6)
            {
                BoundingBoxXYZ box = new BoundingBoxXYZ()
                {
                    Min = new XYZ(Double.Parse(values[0], CultureInfo.InvariantCulture), Double.Parse(values[1], CultureInfo.InvariantCulture), Double.Parse(values[2], CultureInfo.InvariantCulture)),
                    Max = new XYZ(Double.Parse(values[3], CultureInfo.InvariantCulture), Double.Parse(values[4], CultureInfo.InvariantCulture), Double.Parse(values[5], CultureInfo.InvariantCulture))
                };

                return box;
            }

            return null;
        }

        /// <summary>
        /// The GetLevelElement
        /// </summary>
        /// <param name="levles">The levles<see cref="IList{Level}"/></param>
        /// <param name="e">The e<see cref="Element"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string GetElementLevel(IList<Level> levels, Element e)
        {
            string level = "";

            if (e.get_BoundingBox(null) == null) return level = "No Level";
            double x = e.get_BoundingBox(null).Min.Z;
            //Get level element
            if (e.LevelId.ToString() == "-1")
            {
                level = levels[0].Name;
                for (int i = 0; i < levels.Count; i++)
                {
                     
                        
                        if (x >= levels[i].ProjectElevation)
                            level = levels[i].Name;
                        else
                        {
                            break;
                        }
                  
                }
            }
            else
            {
                
                level = (from lv in levels
                         where e.LevelId == lv.Id
                         select lv.Name).FirstOrDefault();
            }

            return level;
        }

        /// <summary>
        /// The GetCentroid
        /// </summary>
        /// <param name="e">The e<see cref="Element"/></param>
        /// <returns>XYZ</returns>
        public static XYZ GetCentroid(Element e)
        {
            XYZ centroid = new XYZ();
            try
            {
                List<Solid> solids = new List<Solid>();

                Options opt = new Options();
                opt.ComputeReferences = false;
                GeometryElement geo1 = e.get_Geometry(opt);

                if (null != geo1)
                {
                    foreach (GeometryObject geoObject in geo1)
                    {
                        Solid solid = geoObject as Solid;
                        if (null != solid)
                            if (false == solid.Faces.IsEmpty)
                                solids.Add(solid);
                    }
                }
                // we dont compare centroid with object have more 1 solid
                if (solids.Count == 1)
                {
                    centroid = solids[0].ComputeCentroid();
                }
            }
            catch
            {
                return centroid;
            }

            return centroid;
        }

        /// <summary>
        /// The GetLevels
        /// </summary>
        /// <param name="doc">The doc<see cref="Document"/></param>
        /// <returns>The <see cref="IList{Level}"/></returns>
        public static IList<Level> GetLevels(Document doc)
        {

            IList<Level> levles = new List<Level>();
            foreach (Level lv in new FilteredElementCollector(doc).OfClass(typeof(Level)))
            {
                levles.Add(lv);
            } 

            //Order by elevation
            Level tmp; 
            for (int i = 0; i < levles.Count; i++)
            {
                for (int j = i + 1; j < levles.Count; j++)
                {
                    double lv1 = levles[j].ProjectElevation;
                    double lv2 = levles[i].ProjectElevation;
                    if (lv1 < lv2)
                    {
                        tmp = levles[i];
                        levles[i] = levles[j];
                        levles[j] = tmp;
                    }
                }
            }
            
            return levles;
        }
    }
}
