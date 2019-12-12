namespace ProjectManagement.CmdRevit
{
    using Autodesk.Revit.Attributes;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using ProjectManagement.Commun;
    using ProjectManagement.Tools.Auth;
    using System;

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

            if (AuthProvider.Instance.IsAuthenticated == true)
            {
                DockablePaneId dpid = new DockablePaneId(new Guid(Properties.Resources.PaletteGuid));

                DockablePane dp = commandData.Application.GetDockablePane(dpid);

                dp.Show();

            }

            return Result.Succeeded;
        }
    }
}
