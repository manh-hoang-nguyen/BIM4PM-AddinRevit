namespace BIM4PM.UI.Tools.Synchronize
{
    using Newtonsoft.Json;
    using BIM4PM.UI.Commun;
    using RestSharp;
    using System;
    using System.Windows.Forms;

    public class SyncModel
    {
        public SyncModel()
        {
        }

        /// <summary>
        /// The FirstCommit
        /// </summary>
        public void FirstCommit()
        {
            //https://stackoverflow.com/questions/48968193/restsharp-the-operation-has-timed-out/49677943
            //https://stackoverflow.com/questions/46584175/restsharp-timeout-not-working
            CompareProvider.Instance.FirstCommit();
        }

        /// <summary>
        /// The Synchronize
        /// </summary>
        public void Synchronize()
        {
            CompareProvider.Instance.Synchronize();
        }
    }
}
