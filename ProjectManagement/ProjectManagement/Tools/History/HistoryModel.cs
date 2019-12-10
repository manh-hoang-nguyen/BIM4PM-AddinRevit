using ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Tools.History
{
   public class HistoryModel
    {
        public static List<Models.History> Histories { get; set; }
        public static ObservableCollection<HistoryByTypeChange> HistoriesByTypeChange { get; set; } = new ObservableCollection<HistoryByTypeChange>();
        public void GetGeometryChange()
        {
         
        }

        public void GetHistory()
        {
            App.HistoryHandler.Request.Make(HistoryRequestId.Refresh);
            App.HistoryEvent.Raise();
        }
        
    }

  
}
