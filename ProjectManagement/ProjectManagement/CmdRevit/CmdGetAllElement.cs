using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using System;

namespace BIM4PM.UI.CmdRevit
{
    [Transaction(TransactionMode.Manual)]
    public class Lab2_2_ModelElements : IExternalCommand
    {
        const string _CategoryNameLegendComponents = "Legend Components";

        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIDocument UIdoc = commandData.Application.ActiveUIDocument;
            Application app = commandData.Application.Application;
            Document doc = UIdoc.Document;
            CategorySet categories = CreateCategoryList(doc, app);
            //string xx = "";
            //foreach (Category item in categories)
            //{

            //    xx += item.Name +" "+ item.Parent + Environment.NewLine;
            //}
            //TaskDialog.Show("revit", xx);
            Reference reference = UIdoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element);
            Element element = UIdoc.Document.GetElement(reference);
            BIM4PM.UI.Utils.RevitUtils.ParameterUtils.GetAllParametersElement(element);
            return Result.Succeeded;
        }
        private CategorySet CreateCategoryList(Document doc, Application app)
        {
            CategorySet myCategorySet = app.Create.NewCategorySet();
            Categories categories = doc.Settings.Categories;
            Category materialCat = categories.get_Item(BuiltInCategory.OST_Materials);

            foreach (Category c in categories)
            {
                if (c.AllowsBoundParameters && c.CategoryType == CategoryType.Model && c != materialCat)
                {
                    myCategorySet.Insert(c);
                }
            }

            return myCategorySet;
        }
    }
  

}
