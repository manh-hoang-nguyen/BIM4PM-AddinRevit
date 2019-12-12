namespace ProjectManagement.Tools.History
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Collections.ObjectModel;
    using System.Dynamic;
    using System.Windows.Data;

    public class HistoryViewModel : ViewModelBase
    {
        public RelayCommand<HistoryView> WindowLoaded { get; set; }

        public RelayCommand<HistoryView> Refresh { get; set; }

        public HistoryModel Model { get; set; }

         

        public HistoryViewModel()
        {
            Model = new HistoryModel();
            WindowLoaded = new RelayCommand<HistoryView>(OnWindowLoaded);
            Refresh = new RelayCommand<HistoryView>(OnRefresh);
        }

        private void OnRefresh(HistoryView view)
        {
            Model.GetHistory();
            
        }

        /// <summary>
        /// The OnWindowLoaded
        /// </summary>
        /// <param name="view">The view<see cref="HistoryView"/></param>
        private void OnWindowLoaded(HistoryView view)
        {
            view.History.ItemsSource = HistoryModel.HistoriesByTypeChange;
            CollectionView collectionView = (CollectionView)CollectionViewSource.GetDefaultView(view.History.ItemsSource);
            PropertyGroupDescription group = new PropertyGroupDescription("type");
            collectionView.GroupDescriptions.Clear();
            collectionView.GroupDescriptions.Add(group);

            collectionView.SortDescriptions.Clear();
            collectionView.SortDescriptions.Add(new System.ComponentModel.SortDescription("type", System.ComponentModel.ListSortDirection.Ascending));
            collectionView.SortDescriptions.Add(new System.ComponentModel.SortDescription("date", System.ComponentModel.ListSortDirection.Descending));
          


        }
    }
}
