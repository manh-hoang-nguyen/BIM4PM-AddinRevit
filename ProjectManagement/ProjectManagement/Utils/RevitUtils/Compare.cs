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
            var keysDictionary1HasThat2DoesNot = RevitElementList.InCloud.Keys.Except(RevitElementList.InModel.Keys);
            var keysDictionary2HasThat1DoesNot = RevitElementList.InModel.Keys.Except(RevitElementList.InCloud.Keys);
            var xx = RevitElementList.InModel.Keys.Except(keysDictionary2HasThat1DoesNot);
        }
    }
}
