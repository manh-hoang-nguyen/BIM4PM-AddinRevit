using Autodesk.Revit.DB.Events;
using ProjectManagement.Tools.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Commun
{
   public static class RevitAppEvents
    {
        public static void OnDocumentChanged(object sender,DocumentChangedEventArgs args)
        {

        }
        public static void OnDocumentCreated(object sender, DocumentCreatedEventArgs args)
        {


            ProjectViewModel.Documents.Add(args.Document);
        }
        public static void OnDocumentOpened(object source, DocumentOpenedEventArgs args)
        {

            ProjectViewModel.Documents.Add(args.Document);
        }
        public static void OnDocumentClosing(object source, DocumentClosingEventArgs args)
        {
            var docToRemove = ProjectViewModel.Documents.Where(x => x.Title == args.Document.Title);
            if (docToRemove != null) ProjectViewModel.Documents.Remove(docToRemove.FirstOrDefault());
        }
    }
}
