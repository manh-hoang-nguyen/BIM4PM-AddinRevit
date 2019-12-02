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

    public class ModelRequestHandler : IExternalEventHandler
    {
        public ModelRequest Request { get; set; } = new ModelRequest();

        public Document _doc { get; set; }

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
            RevitElementList.InModel = new Dictionary<string, RevitElement>();
            
           

            IList<Element> elements = FilterUtils.GetElementInProject(_doc);
            IList<Level> levels = ElementUtils.GetLevels(_doc);
            foreach (Element element in elements)
            {
                string level = ElementUtils.GetElementLevel(levels, element);
                string parameters = ParameterUtils.SerializeRevitParameters(element);
                string sharedParameters = ParameterUtils.SerializeSharedParameters(element);
                string location = ElementUtils.SerializeLocation(element);
                string boundingBox = ElementUtils.SerializeBoundingBox(element.get_BoundingBox(null));
                string centroid = ElementUtils.SerializePoint(ElementUtils.GetCentroid(element));
                string volume = ElementUtils.GetAllSolidVolume(element).ToString();


                RevitElement revitElement = new RevitElement()
                {
                    project = ProjectProvider.Ins.CurrentProject._id,
                    version = VersionCommun.CurrentVersion._id,
                    guid = element.UniqueId,
                    name = element.Name,
                    elementId = element.Id.IntegerValue,
                    category = element.Category.Name,
                    level = level,
                    parameters = parameters,
                    sharedParameters = sharedParameters,
                    worksetId = element.WorksetId.ToString(),
                    location = location,
                    boundingBox = boundingBox,
                    centroid = centroid,
                    volume = volume,
                    typeId = element.GetTypeId().ToString()

                };

                RevitElementList.InModel.Add(revitElement.guid,revitElement);


            }
            Compare.Execute();
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
