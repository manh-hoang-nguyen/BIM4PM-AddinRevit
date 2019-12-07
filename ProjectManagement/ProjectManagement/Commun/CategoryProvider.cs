namespace ProjectManagement.Commun
{
    using Autodesk.Revit.DB;
    using System.Collections.Generic;

    public class CategoryProvider
    {
        private static CategoryProvider _ins;

        public static CategoryProvider Instance
        {
            get
            {
                if (_ins == null)
                    _ins = new CategoryProvider();
                return _ins;
            }
            set
            {
                _ins = value;
            }
        }

        public IList<BuiltInCategory> Categories()
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
                BuiltInCategory.OST_PlaceHolderDucts,// espace réservés aux gaines 
                BuiltInCategory.OST_DuctFitting,//raccords de gaine 
                BuiltInCategory.OST_DuctAccessory,//accessoir de gaineOST_FlexDuctCurves 
                BuiltInCategory.OST_FlexDuctCurves,//gaine flexible 
                BuiltInCategory.OST_DuctTerminal,//Bouche d'aération 
                BuiltInCategory.OST_PipeCurves, //Canalisation 
                BuiltInCategory.OST_PipeFitting,//Raccord de canalisation 
                BuiltInCategory.OST_PlumbingFixtures,//Appareil sanitaires 
                BuiltInCategory.OST_PlumbingFixtures,//OST_Sprinklers 
                BuiltInCategory.OST_CableTray,// chemin de câble 
                BuiltInCategory.OST_Conduit,//Conduits ***Note fait jusqu'à raccordement chemin à câble 
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
            return catList;
        }
    }
}
