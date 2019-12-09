namespace ProjectManagement.CmdRevit
{
    using Autodesk.Revit.Attributes;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using ProjectManagement.Tools.Auth;

    [Transaction(TransactionMode.ReadOnly)]
    public class CmdLogin : IExternalCommand
    {
       
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application; 

            LoginView loginView = new LoginView()
            {
                DataContext = new LoginViewModel(uiapp)
            };
            loginView.ShowDialog();
            return Result.Succeeded;
        }
    }
}
