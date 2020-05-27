namespace BIM4PM.UI.CmdRevit
{
    using Autodesk.Revit.Attributes;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using BIM4PM.UI.Commun;
    
    using BIM4PM.UI.Tools.Auth;
    using System;
    
    using BIM4PM.UI.Events;

    [Transaction(TransactionMode.ReadOnly)]
    public class CmdLogin : IExternalCommand
    {
      
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;

            
            LoginView loginView = new LoginView(new LoginViewModel(uiapp, EventInstance.Instance.EventAggregator));

            loginView.ShowDialog();

            // Todo: Check if token != null
            DockablePaneId dpid = new DockablePaneId(new Guid(Properties.Resources.PaletteGuid));

            DockablePane dp = commandData.Application.GetDockablePane(dpid);

            dp.Show();

            return Result.Succeeded;
        }

        
    }
}
