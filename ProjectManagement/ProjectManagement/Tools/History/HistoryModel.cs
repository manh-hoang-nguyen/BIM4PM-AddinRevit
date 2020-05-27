using BIM4PM.UI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.UI.Tools.History
{
   public class HistoryModel
    {
        private HistoryModel()
        {
            HistoriesByTypeChange = new ObservableCollection<HistoryByTypeChange>();
        }

        public static HistoryModel Instance { get; } = new HistoryModel();

        //public HistoryModel()
        //{
        //    _ins = this;
        //    HistoriesByTypeChange = new ObservableCollection<HistoryByTypeChange>();
        //}
        public static List<Models.History> Histories { get; set; }
        // public static ObservableCollection<HistoryByTypeChange> HistoriesByTypeChange { get; set; } = new ObservableCollection<HistoryByTypeChange>();
        public  ObservableCollection<HistoryByTypeChange> HistoriesByTypeChange { get; set; } 
    

        public void GetHistory()
        {
            App.HistoryHandler.Request.Make(HistoryRequestId.Refresh);
            App.HistoryEvent.Raise();
        }
        
    }

  
}
