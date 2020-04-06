using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.UI.Utils.RevitUtils
{
  public  class DocumentUtils
    {
        public static Document GetDocumentByTitle(UIApplication ui_app, string TitreDocument)
        {
            foreach (Document doc in ui_app.Application.Documents)
            {
                if (doc.Title == TitreDocument)
                {
                    return doc;
                }
            }

            return null;
        }
        /// <summary>
        /// The CreateCategoryList
        /// </summary>
        /// <param name="doc">The doc<see cref="Document"/></param>
        /// <param name="app">The app<see cref="Application"/></param>
        /// <returns>The <see cref="CategorySet"/></returns>
        public static CategorySet CategoryList(Document doc, Application app)
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
