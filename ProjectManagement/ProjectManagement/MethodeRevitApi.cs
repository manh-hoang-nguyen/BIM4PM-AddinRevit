namespace BIM4PM.UI
{
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using BIM4PM.UI.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class MethodeRevitApi
    {
        /// <summary>
        /// The GetDocumentByTitle
        /// </summary>
        /// <param name="ui_app">The ui_app<see cref="UIApplication"/></param>
        /// <param name="TitreDocument">The TitreDocument<see cref="string"/></param>
        /// <returns>The <see cref="Document"/></returns>
        public static Document GetDocumentByTitle(UIApplication ui_app, string TitreDocument)
        {
            foreach (Document doc in ui_app.Application.Documents)
            {
                if (doc.Title == TitreDocument)
                {
                    return doc;
                }
            }

            return null;
        }

        /// <summary>
        /// The GetElementOfCategories
        /// </summary>
        /// <param name="doc">The doc<see cref="Document"/></param>
        /// <returns>The <see cref="IList{Element}"/></returns>
        public static IList<Element> GetElementOfCategories(Document doc)
        {
            //Créer une liste de catégories
            IList<BuiltInCategory> catList = new List<BuiltInCategory>();
            //ajout catégorie dans liste
            catList.Add(BuiltInCategory.OST_Doors);
            catList.Add(BuiltInCategory.OST_Windows);
            catList.Add(BuiltInCategory.OST_Walls);
            catList.Add(BuiltInCategory.OST_StructuralColumns);
            catList.Add(BuiltInCategory.OST_StructuralFraming);
            catList.Add(BuiltInCategory.OST_GenericModel);
            catList.Add(BuiltInCategory.OST_Columns);
            catList.Add(BuiltInCategory.OST_StructuralFoundation);
            catList.Add(BuiltInCategory.OST_Ceilings);
            catList.Add(BuiltInCategory.OST_Floors);
            catList.Add(BuiltInCategory.OST_Stairs);
            catList.Add(BuiltInCategory.OST_SpecialityEquipment);
            catList.Add(BuiltInCategory.OST_Roofs);
            //****************catégories SYSTEMES******************
            catList.Add(BuiltInCategory.OST_DuctCurves);//Gaine
            catList.Add(BuiltInCategory.OST_PlaceHolderDucts);// espace réservés aux gaines---VERIFIE
            catList.Add(BuiltInCategory.OST_DuctFitting);//raccords de gaine---VERIFIE
            catList.Add(BuiltInCategory.OST_DuctAccessory);//accessoir de gaineOST_FlexDuctCurves---VERIFIE
            catList.Add(BuiltInCategory.OST_FlexDuctCurves);//gaine flexible---VERIFIE
            catList.Add(BuiltInCategory.OST_DuctTerminal);//Bouche d'aération ---VERIFIE
            catList.Add(BuiltInCategory.OST_PipeCurves); //Canalisation---VERIFIE
            catList.Add(BuiltInCategory.OST_PipeFitting);//Raccord de canalisation---VERIFIE
            catList.Add(BuiltInCategory.OST_PlumbingFixtures);//Appareil sanitairesOST_Sprinklers---VERIFIE
            catList.Add(BuiltInCategory.OST_PlumbingFixtures);//OST_Sprinklers---VERIFIE
            catList.Add(BuiltInCategory.OST_CableTray);// chemin de câble---VERIFIE
            catList.Add(BuiltInCategory.OST_Conduit);//Conduits ***Note fait jusqu'à raccordement chemin à câble---VERIFIE
            catList.Add(BuiltInCategory.OST_CableTrayFitting);//Raccord chemin de câble
            catList.Add(BuiltInCategory.OST_LightingFixtures);//Luminaires
            //PLB
            catList.Add(BuiltInCategory.OST_MechanicalEquipment); //Equipement génie climatique
            catList.Add(BuiltInCategory.OST_PipeAccessory);// asscessoir de canalisation
                                                           // Créer filtre de plusieurs catégorie
                                                           //ELE
            catList.Add(BuiltInCategory.OST_ElectricalEquipment);
            catList.Add(BuiltInCategory.OST_ElectricalFixtures);
            catList.Add(BuiltInCategory.OST_LightingDevices);
            catList.Add(BuiltInCategory.OST_DataDevices);
            catList.Add(BuiltInCategory.OST_CommunicationDevices);
            catList.Add(BuiltInCategory.OST_FireAlarmDevices);
            catList.Add(BuiltInCategory.OST_SecurityDevices);

            ElementMulticategoryFilter multiCatFilter = new ElementMulticategoryFilter(catList);
            return new FilteredElementCollector(doc).WhereElementIsNotElementType().WherePasses(multiCatFilter).ToElements();
        }

        /// <summary>
        /// The GetLevelElement
        /// </summary>
        /// <param name="levles">The levles<see cref="IList{Level}"/></param>
        /// <param name="e">The e<see cref="Element"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string GetLevelElement(IList<Level> levles, Element e)
        {
            string level = "";

            //Get level element
            if (e.LevelId.ToString() == "-1")
            {
                level = levles[0].Name;
                for (int i = 0; i < levles.Count; i++)
                {
                    try
                    {
                        double x = e.get_BoundingBox(null).Min.Z;
                        if (x >= levles[i].ProjectElevation)
                            level = levles[i].Name;
                    }
                    catch
                    {
                        level = "No Level";
                    }
                }
            }
            else
            {
                foreach (Level lv in levles)
                {
                    if (lv.Id == e.LevelId)
                        level = lv.Name;
                }
            }

            return level;
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


            levles.OrderBy(a => a.Elevation).Select(a => a.Name).ToList();

            /*
            //Sap xep level theo thu tu tang dan
            Level tmp;//bien trung gian
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
            */
            return levles;
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
        /// The GetCentroid
        /// </summary>
        /// <param name="e">The e<see cref="Element"/></param>
        /// <returns>XYZ</returns>
        public static CentroidElement GetCentroid(Element e)
        {
            CentroidElement centroidElement = new CentroidElement() { status = "0" };
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
                if (solids.Count == 1)
                {
                    centroidElement = new CentroidElement()
                    {
                        status = "1",
                        centroid = new PointXYZ
                        {
                            X = solids[0].ComputeCentroid().X,
                            Y = solids[0].ComputeCentroid().Y,
                            Z = solids[0].ComputeCentroid().Z
                        }
                    };
                }
            }
            catch
            {
                return centroidElement;
            }

            return centroidElement;
        }

        /// <summary>
        /// The GetBoundingBox
        /// </summary>
        /// <param name="e">The e<see cref="Element"/></param>
        /// <returns>The <see cref="BoundingBoxElement"/></returns>
        public static BoundingBoxElement GetBoundingBox(Element e)
        {
            BoundingBoxElement boundingbox = new BoundingBoxElement() { status = "0" };
            try
            {
                XYZ min = e.get_BoundingBox(null).Min;
                XYZ max = e.get_BoundingBox(null).Max;
                boundingbox = new BoundingBoxElement
                {
                    status = "1",
                    BoundingBox_point01 = new PointXYZ
                    {
                        X = min.X,
                        Y = min.Y,
                        Z = min.Z
                    },
                    BoundingBox_point02 = new PointXYZ
                    {
                        X = max.X,
                        Y = max.Y,
                        Z = max.Z
                    }
                };
            }
            catch { }

            return boundingbox;
        }

        /// <summary>
        /// Get location of an element, return an array XYZ
        /// </summary>
        /// <param name="e">The e<see cref="Element"/></param>
        /// <returns></returns>
        public static LocationElement GetLocationElement(Element e)
        {
            LocationElement elementLocation = new LocationElement() { LocationType = "null" };

            Location position = e.Location;

            if (null == position)
            {
                elementLocation.LocationType = "null";
                return elementLocation;
            }
            else
            {
                // If the location is a point location, give the user information
                LocationPoint positionPoint = position as LocationPoint;

                LocationCurve positionCurve = position as LocationCurve;

                if (null != positionPoint)
                {
                    elementLocation = new LocationElement
                    {
                        LocationType = "Point",
                        Location_point01 = new PointXYZ
                        {
                            X = positionPoint.Point.X,
                            Y = positionPoint.Point.Y,
                            Z = positionPoint.Point.Z
                        }
                    };
                }
                else
                {
                    if (null != positionCurve)
                    {
                        // il faut analyser curve line and curve courbe
                        Curve curve = positionCurve.Curve;
                        elementLocation = new LocationElement
                        {
                            LocationType = "Curve",
                            Location_point01 = new PointXYZ
                            {
                                X = curve.GetEndPoint(0).X,
                                Y = curve.GetEndPoint(0).Y,
                                Z = curve.GetEndPoint(0).Z
                            },
                            Location_point02 = new PointXYZ
                            {
                                X = curve.GetEndPoint(1).X,
                                Y = curve.GetEndPoint(1).Y,
                                Z = curve.GetEndPoint(1).Z
                            }
                        };
                    }
                }
            }

            return elementLocation;
        }
    }

    internal class UtilParameter
    {
        /// <summary>
        /// The GetParameterValue
        /// </summary>
        /// <param name="Paramètre">The Paramètre<see cref="Parameter"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string GetParameterValue(Parameter Paramètre)
        {
            switch (Paramètre.StorageType)
            {
                case StorageType.Double:
                    //// Obtenir la valeur du paramètre sans unité.
                    //return paramètre.AsDouble(); // Changer la valeur de retour.

                    // Obtenir la valeur du paramètre avec unité.
                    return Paramètre.AsValueString();

                case StorageType.ElementId:
                    return Paramètre.AsElementId().IntegerValue.ToString();

                case StorageType.Integer:
                    return Paramètre.AsValueString();

                case StorageType.None:
                    return Paramètre.AsValueString();

                case StorageType.String:
                    return Paramètre.AsString();

                default:
                    return "";
            }
        }
    }
}
