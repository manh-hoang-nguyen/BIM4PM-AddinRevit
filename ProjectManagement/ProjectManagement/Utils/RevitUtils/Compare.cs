namespace ProjectManagement.Utils.RevitUtils
{
    using ProjectManagement.Commun;
    using ProjectManagement.Models;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Compare
    {
        /// <summary>
        /// The Execute
        /// </summary>
        public static void Execute()
        {
            CompareResult.ModifiedElements = new List<string>();
           
            var same = RevitElementList.InCloud.Count == RevitElementList.InModel.Count && RevitElementList.InCloud.Keys.SequenceEqual(RevitElementList.InModel.Keys);
            var keysDictionary1HasThat2DoesNot = RevitElementList.InCloud.Keys.Except(RevitElementList.InModel.Keys);
            var keysDictionary2HasThat1DoesNot = RevitElementList.InModel.Keys.Except(RevitElementList.InCloud.Keys);
            var EleToCompareGuid = RevitElementList.InModel.Keys.Except(keysDictionary2HasThat1DoesNot);

            foreach ( string guid in EleToCompareGuid)
            {
                RevitElement current = RevitElementList.InModel[guid];
                RevitElement previous = RevitElementList.InCloud[guid];
                bool geometryIsSame = CompareGeometry(current, previous);
                bool revitParameterIsSame = CompareRevitParameter(current, previous);
                bool sharedParameterIsSame = CompareSharedParameter(current, previous);
               
            }
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

            if (current.parameters == previous.parameters) result = true;

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
