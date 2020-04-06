namespace BIM4PM.UI.Tools.Discussion
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using BIM4PM.UI.Commun;
    using System;
    using System.Windows.Media;

    public class DiscussionViewModel : ViewModelBase
    {
        public RelayCommand<DiscussionView> MouseEnterBtn { get; set; }
       
        public RelayCommand<DiscussionView> MouseLeaveBtn { get; set; }

        public RelayCommand<DiscussionView> MouseEnterBtnComments { get; set; }

        public RelayCommand<DiscussionView> MouseLeaveBtnComments { get; set; }

        public RelayCommand<DiscussionView> WindowLoaded { get; set; }

        public RelayCommand<DiscussionView> Refresh { get; set; }

        public RelayCommand<DiscussionView> SendComment { get; set; }

        public RelayCommand<DiscussionView> GetComment { get; set; }

        public RelayCommand<DiscussionView> GetTopics { get; set; }

        public RelayCommand<DiscussionView> TxbCommentChange { get; set; }

        public DiscussionModel Model { get; set; }

        private bool _btnSendIsEnabled { get; set; }

        public bool BtnSendIsEnabled
        {
            get => _btnSendIsEnabled; set { _btnSendIsEnabled = value; RaisePropertyChanged(); }
        }

        public DiscussionViewModel()
        {
            Model = new DiscussionModel();
          
            WindowLoaded = new RelayCommand<DiscussionView>(OnWindowLoaded);
            SendComment = new RelayCommand<DiscussionView>(OnComment);
            GetComment = new RelayCommand<DiscussionView>(OnGetComment);
            GetTopics = new RelayCommand<DiscussionView>(OnGetTopics);
            MouseEnterBtn = new RelayCommand<DiscussionView>(OnMouseEnterBtn);
            MouseLeaveBtn = new RelayCommand<DiscussionView>(OnMouseLeaveBtn);
            MouseEnterBtnComments = new RelayCommand<DiscussionView>(OnMouseEnterBtnComments);
            MouseLeaveBtnComments = new RelayCommand<DiscussionView>(OnMouseLeaveBtnComments);
            TxbCommentChange = new RelayCommand<DiscussionView>(OnTxbCommentChange);
        }

        private void OnTxbCommentChange(DiscussionView view)
        { 
             if (string.IsNullOrEmpty(view.txbComment.Text) || string.IsNullOrWhiteSpace(view.txbComment.Text))
            {
                BtnSendIsEnabled = false;
            }
            else
            {
                BtnSendIsEnabled = true;
            }
        }

        private void OnGetComment(DiscussionView view)
        {
            Model.Refresh();
        }

        private void OnWindowLoaded(DiscussionView view)
        { 
            view.comments.ItemsSource = DiscussionProvider.Instance.Comments;
          
        }

        private void OnGetTopics(DiscussionView view)
        {
           
        }

         

        private void OnComment(DiscussionView view)
        {
            string[] stringArray = view.txbComment.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            string comment = "";
            int count = stringArray.Length;
            for (int i = 0; i < count - 1; i++)
            {
                comment += stringArray[i] + "\\n";
            }
            comment += stringArray[count - 1];

            Model.SendComment(comment);
            view.txbComment.Text = null;
        }
        private void OnMouseLeaveBtnComments(DiscussionView view)
        {
            view.BorderBtnComments.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void OnMouseEnterBtnComments(DiscussionView view)
        {
            view.BorderBtnComments.Background = new SolidColorBrush(Colors.LightBlue);
        }

        private void OnMouseLeaveBtn(DiscussionView view)
        {
            view.BorderBtn.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void OnMouseEnterBtn(DiscussionView view)
        {
            view.BorderBtn.Background = new SolidColorBrush(Colors.LightBlue);

        }
    }
}
