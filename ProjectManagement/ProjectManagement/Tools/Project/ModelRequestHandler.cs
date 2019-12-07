namespace ProjectManagement.Tools.Project
{
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using ProjectManagement.Commun;
    using ProjectManagement.Models;
    using ProjectManagement.Utils.RevitUtils;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Windows;

    public class ModelRequestHandler : IExternalEventHandler
    {
        public ModelRequest Request { get; set; } = new ModelRequest();

        /// <summary>
        /// The Execute
        /// </summary>
        /// <param name="app">The app<see cref="UIApplication"/></param>
        public void Execute(UIApplication app)
        {
            try
            {
                switch (Request.Take())
                {
                    case RequestId.Element:
                        GetRevitElementData(app);
                        break;
                    case RequestId.Model:
                        GetDocuments(app);
                        break;
                    case RequestId.None:
                        Message(app);

                        break;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// The GetName
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        public string GetName()
        {
            return "Model External Event";
        }

        /// <summary>
        /// Return a set of documents within Revit
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        private DocumentSet GetDocuments(UIApplication app)
        {
            return app.Application.Documents;
        }

        /// <summary>
        /// The GetRevitElementData
        /// </summary>
        /// <param name="app">The app<see cref="UIApplication"/></param>
        private void GetRevitElementData(UIApplication app)
        {
            if (ModelProvider.Instance.CurrentModel == null)
            {
                MessageBox.Show("Select Revit model please!");
                return;
            }
            ModelProvider.Instance.Update();
            
            if(ProjectProvider.Instance.DicRevitElements != null && ProjectProvider.Instance.DicRevitElements.Count == 0)
            {
                MessageBox.Show("You do not have data in cloud yet. Do your first commit.");
                Synchronize.SyncView syncView = new Synchronize.SyncView
                {
                    DataContext = new Synchronize.SyncViewModel()
                };
                syncView.ShowDialog();
            }
            
        }

        /// <summary>
        /// The Message
        /// </summary>
        /// <param name="app">The app<see cref="UIApplication"/></param>
        private void Message(UIApplication app)
        {
            TaskDialog.Show("MH", " this is a test");
        }
    }

    public class ModelRequest
    {
        private int _request = (int)RequestId.None;

        /// <summary>
        /// The Take
        /// </summary>
        /// <returns>The <see cref="RequestId"/></returns>
        public RequestId Take()
        {
            return (RequestId)Interlocked.Exchange(ref _request, (int)RequestId.None);
        }

        /// <summary>
        /// The Make
        /// </summary>
        /// <param name="request">The request<see cref="RequestId"/></param>
        public void Make(RequestId request)
        {
            Interlocked.Exchange(ref _request, (int)request);
        }
    }

    public enum RequestId
    {
        None,
        Model,
        Element
    }
}
