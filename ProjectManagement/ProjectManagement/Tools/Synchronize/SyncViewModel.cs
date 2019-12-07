namespace ProjectManagement.Tools.Synchronize
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using ProjectManagement.Commun;
    using ProjectManagement.Models;
    using System;
    using System.Collections.ObjectModel;
    using System.Windows;

    public class SyncViewModel : ViewModelBase
    {
        public RelayCommand<SyncView> WindowLoaded { get; set; }
        public RelayCommand<SyncView> FirstCommit { get; set; }
        public RelayCommand<SyncView> Synchronize { get; set; }
        public RelayCommand<SyncView> MemberDoubleClick { get; set; }
        public RelayCommand<SyncView> MemberToMailDoubleClick { get; set; }
        public RelayCommand<SyncView> MailNotification { get; set; }
        public SyncModel Model { get; set; }

        public ObservableCollection<Member> AllMembers { get; set; } = new ObservableCollection<Member>();
        public ObservableCollection<Member> MembersToMail { get; set; } = new ObservableCollection<Member>();

        public SyncViewModel()
        {
            Model = new SyncModel();
            WindowLoaded = new RelayCommand<SyncView>(OnWindowLoaded);
            FirstCommit = new RelayCommand<SyncView>(OnFirstCommit);
            Synchronize = new RelayCommand<SyncView>(OnSynchronize);
            MemberDoubleClick = new RelayCommand<SyncView>(OnMemberDoubleClick);
            MemberToMailDoubleClick = new RelayCommand<SyncView>(OnMemberToMailDoubleClick);
            MailNotification = new RelayCommand<SyncView>(OnMailNotification);
        }

        private void OnMailNotification(SyncView view)
        {
            if (view.ckbMail.IsChecked== true)
            {
                view.MailPanel.Visibility = Visibility.Visible;
            }
            else
            {
                view.MailPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void OnMemberToMailDoubleClick(SyncView view)
        {
          
            Member member = view.MembersToMail.SelectedItem as Member;
            if (member == null) return;
            AllMembers.Add(member);
            MembersToMail.Remove(member);
        }

        private void OnMemberDoubleClick(SyncView view)
        {
            
            Member member= view.Members.SelectedItem as Member;
            if (member == null) return;
            AllMembers.Remove(member);
            MembersToMail.Add(member);
        }

        private void OnSynchronize(SyncView view)
        {
            Model.Synchronize();
        }

        private void OnFirstCommit(SyncView view)
        {
            Model.FirstCommit();
        }

        /// <summary>
        /// The OnWindowLoaded
        /// </summary>
        /// <param name="view">The view<see cref="SyncView"/></param>
        private void OnWindowLoaded(SyncView view)
        {
            if (ProjectProvider.Instance.DicRevitElements == null || ModelProvider.Instance.DicRevitElements == null)
            {
                MessageBox.Show("You have to connect to project first!");
                view.Win.Close();
            }
            else
            {
                if (ModelProvider.Instance.DicRevitElements.Count == 0)
                {
                    MessageBox.Show("No element in models");
                    view.Win.Close();
                }
                else
                {
                    if (ProjectProvider.Instance.DicRevitElements.Count == 0)
                        view.Synchonize.IsEnabled = false;
                    else
                        view.FirstCommit.IsEnabled = false;

                }


            }
            view.Members.ItemsSource = AllMembers = new ObservableCollection<Member>(ProjectProvider.Instance.ProjectMembers);
            view.MembersToMail.ItemsSource = MembersToMail;

        }
    }
}
