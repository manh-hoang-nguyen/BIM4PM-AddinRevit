namespace ProjectManagement.Utils.RevitUtils
{
    using ProjectManagement.Commun;
    using ProjectManagement.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;

    public class Compare
    {
        /// <summary>
        /// The Execute
        /// </summary>
        public static void Execute(bool withPara)
        {
            
            CompareResult.ModifiedElements = new List<string>();
            CompareResult.SameElements = new List<string>();
            CompareResult.DeletedElements = new List<string>();
            CompareResult.NewElements = new List<string>(); 
            
            CompareResult.DeletedElements = RevitElementList.InCloud.Keys.Except(RevitElementList.InModel.Keys);
            CompareResult.NewElements = RevitElementList.InModel.Keys.Except(RevitElementList.InCloud.Keys);

            var EleToCompareGuid = RevitElementList.InModel.Keys.Except(CompareResult.NewElements);
 
            foreach ( string guid in EleToCompareGuid)
            {
                RevitElement current = RevitElementList.InModel[guid];
                RevitElement previous = RevitElementList.InCloud[guid];
                bool geometryIsSame = CompareGeometry(current, previous);
                bool revitParameterIsSame = CompareRevitParameter(current, previous);
                bool sharedParameterIsSame = CompareSharedParameter(current, previous);

                if (withPara == false)
                {
                    if (geometryIsSame) CompareResult.SameElements.Add(guid);  
                    else CompareResult.ModifiedElements.Add(guid); 
                }
                else
                {
                    if (geometryIsSame && revitParameterIsSame && sharedParameterIsSame) CompareResult.SameElements.Add(guid);
                    else CompareResult.ModifiedElements.Add(guid);
                }
            }
            int i = CompareResult.ModifiedElements.Count;


        }

        private static bool CompareGeometry(RevitElement current, RevitElement previous)
        {
            bool result = false;

            if (current.boundingBox == previous.boundingBox
                && current.centroid == previous.centroid
                && current.location == previous.location
                && current.volume == previous.volume) result = true; 

            return result;
        }
        private static bool CompareRevitParameter(RevitElement current, RevitElement previous)
        {
            bool result = false;

            if (current.parameters.ToLower() == previous.parameters.ToLower()) result = true;

            return result;
        }
        private static bool CompareSharedParameter(RevitElement current, RevitElement previous)
        {
            bool result = false;

            if (current.sharedParameters == previous.sharedParameters) result = true;

            return result;
        }
    }
}
