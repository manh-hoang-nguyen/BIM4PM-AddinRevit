namespace ProjectManagement.Utils.RevitUtils
{
    using Autodesk.Revit.DB;
    using Autodesk.Revit.DB.Electrical;
    using Autodesk.Revit.DB.Mechanical;
    using Autodesk.Revit.DB.Plumbing;
    using System.Collections.Generic;
    using System.Linq;

    public class FilterUtils
    {
        /// <summary>
        /// The Get all element which have parameters and material
        /// </summary>
        /// <param name="doc">The doc<see cref="Document"/></param>
        /// <param name="isActiveView">The isActiveView<see cref="bool"/></param>
        /// <returns>The <see cref="IList{Element}"/></returns>
        public static IList<Element> GetElementInProject(Document doc, bool isActiveView = false)
        {
            IList<Element> elements = new List<Element>();
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            if (isActiveView)
                collector = new FilteredElementCollector(doc, doc.ActiveView.Id);
            collector
                    .WhereElementIsNotElementType()
                    .WhereElementIsViewIndependent()
                    .ToElements();
            foreach (Element element in collector)
            {
                if (null != element.Category
                  && 0 < element.Parameters.Size
                  && (element.Category.HasMaterialQuantities))
                {
                    elements.Add(element);
                }
            }
            return elements;
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
            BuiltInCategory[] bics = new BuiltInCategory[] {
                BuiltInCategory.OST_Doors,
                BuiltInCategory.OST_Walls,
                BuiltInCategory.OST_StructuralColumns,
                BuiltInCategory.OST_StructuralFraming,
                BuiltInCategory.OST_GenericModel,
                BuiltInCategory.OST_Columns,
                BuiltInCategory.OST_StructuralFoundation,
                BuiltInCategory.OST_Ceilings,
                BuiltInCategory.OST_Floors,
                BuiltInCategory.OST_Stairs,
                BuiltInCategory.OST_StairsRailing,
                BuiltInCategory.OST_SpecialityEquipment,
                BuiltInCategory.OST_Roofs,
                BuiltInCategory.OST_Windows,
                BuiltInCategory.OST_DuctCurves,//Gaine
                BuiltInCategory.OST_PlaceHolderDucts,// espace réservés aux gaines---VERIFIE
                BuiltInCategory.OST_DuctFitting,//raccords de gaine---VERIFIE
                BuiltInCategory.OST_DuctAccessory,//accessoir de gaineOST_FlexDuctCurves---VERIFIE
                BuiltInCategory.OST_FlexDuctCurves,//gaine flexible---VERIFIE
                BuiltInCategory.OST_DuctTerminal,//Bouche d'aération ---VERIFIE
                BuiltInCategory.OST_PipeCurves, //Canalisation---VERIFIE
                BuiltInCategory.OST_PipeFitting,//Raccord de canalisation---VERIFIE
                BuiltInCategory.OST_PlumbingFixtures,//Appareil sanitairesOST_Sprinklers---VERIFIE
                BuiltInCategory.OST_PlumbingFixtures,//OST_Sprinklers---VERIFIE
                BuiltInCategory.OST_CableTray,// chemin de câble---VERIFIE
                BuiltInCategory.OST_Conduit,//Conduits ***Note fait jusqu'à raccordement chemin à câble---VERIFIE
                BuiltInCategory.OST_CableTrayFitting,//Raccord chemin de câble
                BuiltInCategory.OST_LightingFixtures,//Luminaires
                //PLB
                BuiltInCategory.OST_MechanicalEquipment, //Equipement génie climatique
                BuiltInCategory.OST_PipeAccessory,// asscessoir de canalisation
                BuiltInCategory.OST_ConduitFitting,

                //ELE
                BuiltInCategory.OST_ElectricalEquipment,
                BuiltInCategory.OST_ElectricalFixtures,
                BuiltInCategory.OST_LightingDevices,
                BuiltInCategory.OST_DataDevices,
                BuiltInCategory.OST_CommunicationDevices,
                BuiltInCategory.OST_FireAlarmDevices,
                BuiltInCategory.OST_SecurityDevices,

                BuiltInCategory.OST_Sprinklers,
                BuiltInCategory.OST_Wire,
              };

            foreach (BuiltInCategory item in bics)
            {
                catList.Add(item);
            }

            ElementMulticategoryFilter multiCatFilter = new ElementMulticategoryFilter(catList);
            return new FilteredElementCollector(doc).WhereElementIsNotElementType().WherePasses(multiCatFilter).ToElements();
        }

        /// <summary>
        /// The GetConnectorElements
        /// </summary>
        /// <param name="doc">The doc<see cref="Document"/></param>
        /// <param name="include_wires">The include_wires<see cref="bool"/></param>
        /// <returns>The <see cref="FilteredElementCollector"/></returns>
        internal static FilteredElementCollector GetConnectorElements(
              Document doc,
              bool include_wires)
        {
            // what categories of family instances
            // are we interested in?

            BuiltInCategory[] bics = new BuiltInCategory[] {
                //BuiltInCategory.OST_CableTray,
                BuiltInCategory.OST_CableTrayFitting,
                //BuiltInCategory.OST_Conduit,
                BuiltInCategory.OST_ConduitFitting,
                //BuiltInCategory.OST_DuctCurves,
                BuiltInCategory.OST_DuctFitting,
                BuiltInCategory.OST_DuctTerminal,
                BuiltInCategory.OST_ElectricalEquipment,
                BuiltInCategory.OST_ElectricalFixtures,
                BuiltInCategory.OST_LightingDevices,
                BuiltInCategory.OST_LightingFixtures,
                BuiltInCategory.OST_MechanicalEquipment,
                //BuiltInCategory.OST_PipeCurves,
                BuiltInCategory.OST_PipeFitting,
                BuiltInCategory.OST_PlumbingFixtures,
                BuiltInCategory.OST_SpecialityEquipment,
                BuiltInCategory.OST_Sprinklers,
                //BuiltInCategory.OST_Wire,
              };

            IList<ElementFilter> a
              = new List<ElementFilter>(bics.Count());

            foreach (BuiltInCategory bic in bics)
            {
                a.Add(new ElementCategoryFilter(bic));
            }

            LogicalOrFilter categoryFilter
              = new LogicalOrFilter(a);

            LogicalAndFilter familyInstanceFilter
              = new LogicalAndFilter(categoryFilter,
                new ElementClassFilter(
                  typeof(FamilyInstance)));

            IList<ElementFilter> b
              = new List<ElementFilter>(6);

            b.Add(new ElementClassFilter(typeof(CableTray)));
            b.Add(new ElementClassFilter(typeof(Conduit)));
            b.Add(new ElementClassFilter(typeof(Duct)));
            b.Add(new ElementClassFilter(typeof(Pipe)));

            if (include_wires)
            {
                b.Add(new ElementClassFilter(typeof(Wire)));
            }

            b.Add(familyInstanceFilter);

            LogicalOrFilter classFilter
              = new LogicalOrFilter(b);

            FilteredElementCollector collector
              = new FilteredElementCollector(doc);

            collector.WherePasses(classFilter);

            return collector;
        }

        /// <summary>
        /// The GetElementList
        /// </summary>
        /// <param name="doc">The doc<see cref="Document"/></param>
        /// <param name="categories">The categories<see cref="CategorySet"/></param>
        /// <returns>The <see cref="IList{Element}"/></returns>
        public static IList<Element> GetElementList(Document doc, CategorySet categories)
        {
            //Retrive all model elements
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            IList<ElementFilter> categoryFilters = new List<ElementFilter>();

            foreach (Category category in categories)
            {
                categoryFilters.Add(new ElementCategoryFilter(category.Id));
            }

            ElementFilter filter = new LogicalOrFilter(categoryFilters);

            return collector.WherePasses(filter).WhereElementIsNotElementType().ToElements();
        }
    }
}
