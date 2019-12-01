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
            
            var same = RevitElementList.InCloud.Count == RevitElementList.InModel.Count && RevitElementList.InCloud.Keys.SequenceEqual(RevitElementList.InModel.Keys);
            var DeletedElementGuid = RevitElementList.InCloud.Keys.Except(RevitElementList.InModel.Keys);
            var NewElementGuid = RevitElementList.InModel.Keys.Except(RevitElementList.InCloud.Keys);
            var ElementToCheck = RevitElementList.InModel.Keys.Except(NewElementGuid);
        }
    }
}
