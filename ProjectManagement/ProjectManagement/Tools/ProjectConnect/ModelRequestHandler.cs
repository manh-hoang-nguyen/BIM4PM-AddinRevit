namespace BIM4PM.UI.Tools.Project
{
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using BIM4PM.UI.Commun;
    using System;
    using System.Threading;
    using System.Windows;

    public class ModelRequestHandler : IExternalEventHandler
    {
        public ModelRequest Request { get; set; } = new ModelRequest();

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

        public string GetName()
        {
            return "Model External Event";
        }

        private DocumentSet GetDocuments(UIApplication app)
        {

            return app.Application.Documents;
        }

        private void GetRevitElementData(UIApplication app)
        {
            //if (ProjectModelConnect.SelectedRevitModel == null)
            //{
            //    MessageBox.Show("Select Revit model please!");
            //    return;
            //}
            //ModelProvider.Instance.Update();
        }

        private void Message(UIApplication app)
        {
            TaskDialog.Show("MH", " this is a test");
        }
    }

    public class ModelRequest
    {
        private int _request = (int)RequestId.None;

        public RequestId Take()
        {
            return (RequestId)Interlocked.Exchange(ref _request, (int)RequestId.None);
        }

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
