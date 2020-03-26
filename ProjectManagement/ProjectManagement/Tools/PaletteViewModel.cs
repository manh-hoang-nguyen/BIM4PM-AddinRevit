﻿namespace ProjectManagement.Tools
{
    using GalaSoft.MvvmLight;
    using ProjectManagement.Commun;
    using ProjectManagement.Tools.Discussion;
    using ProjectManagement.Tools.History;
    using ProjectManagement.Tools.Project;
    using System.Collections.ObjectModel;
    using System.Threading;
    using System.Windows.Controls;

    public class PaletteViewModel : ViewModelBase
    {
       public static ObservableCollection<TabItem> TabItems { get; set; } = new ObservableCollection<TabItem>();

        public PaletteViewModel()
        { 
            if (TabItems.Count == 0)
            {
                TabItems.Add(new TabItem
                {
                    Content = new ProjectView() { DataContext = new ProjectViewModel() },
                    Header = "Connect"
                });
            }
            //AuthProvider.Instance.AuthenticationChanged += (s, e) => OnAuthentication();
            AuthProvider.Instance.ConnectionChanged += (s, e) => AddTabsOnConnected();
           
        }

        private void OnAuthentication()
        {
            if (AuthProvider.Instance.IsAuthenticated = !true)
            {
                TabItems.RemoveAt(0);
            }
        }
        private void AddTabsOnConnected()
        {
            

            if (AuthProvider.Instance.IsConnected == true)
            {
                if (TabItems.Count < 3)
                {
                    TabItems.Add(new TabItem
                    {
                        Content = new HistoryView() { DataContext = new HistoryViewModel() },
                        Header = "History"
                    });
                    TabItems.Add(new TabItem
                    {
                        Content = new DiscussionView() { DataContext = new DiscussionViewModel() },
                        Header = "Discussion"
                    });
                }

            }
            else
            {
                int count = TabItems.Count;
                for (int i = 1; i < count; i++)
                {
                    TabItems.RemoveAt(1);

                }
            }
        }
    }
}
