namespace ProjectManagement.Tools.Auth
{
    using Autodesk.Revit.UI;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using ProjectManagement.Tools.Project;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using Visibility = System.Windows.Visibility;

    public class LoginViewModel : ViewModelBase
    {
        private UIApplication _uiapp;

        public LoginModel Model { get; set; }

        public RelayCommand<Window> Cancel { get; set; }

        public RelayCommand<Window> Authenticated { get; set; }

        public RelayCommand<LoginView> Login { get; set; }

        public RelayCommand<LoginView> WindowLoaded { get; set; }

        private Visibility _visibilityProgressBar { get; set; }

        private Visibility _visibilityAuthenticated { get; set; }

        private Visibility _visibilityNotAuthenticated { get; set; }

        private Visibility _visiblityLoginWindow { get; set; }

        private string _userName { get; set; }

        private string _password { get; set; }

        public Visibility VisibilityProgressBar
        {
            get => _visibilityProgressBar; set { _visibilityProgressBar = value; RaisePropertyChanged(); }
        }

        public Visibility VisibilityAuthenticated
        {
            get => _visibilityAuthenticated; set { _visibilityAuthenticated = value; RaisePropertyChanged(); }
        }

        public Visibility VisibilityNotAuthenticated
        {
            get => _visibilityNotAuthenticated; set { _visibilityNotAuthenticated = value; RaisePropertyChanged(); }
        }

        public Visibility VisiblityLoginWindow
        {
            get => _visiblityLoginWindow; set { _visiblityLoginWindow = value; RaisePropertyChanged(); }
        }

        public string UserName
        {
            get => _userName; set { _userName = value; RaisePropertyChanged(); }
        }

        public string Password
        {
            get => _password; set { _password = value; RaisePropertyChanged(); }
        }

        public LoginViewModel(UIApplication uiapp)
        {
            _uiapp = uiapp;
            Model = new LoginModel();
            WindowLoaded = new RelayCommand<LoginView>(OnWindowLoaded);
            Cancel = new RelayCommand<Window>(OnCancel);
            Login = new RelayCommand<LoginView>(OnLogin);
            Authenticated = new RelayCommand<Window>(OnAuthenticated);
        }

        /// <summary>
        /// The OnAuthenticated
        /// </summary>
        /// <param name="win">The win<see cref="Window"/></param>
        private void OnAuthenticated(Window win)
        {
            win.Close();
            ProjectView project = new ProjectView
            {
                DataContext = new ProjectViewModel(_uiapp)
            };
            project.Show();
        }

        /// <summary>
        /// The OnWindowLoaded
        /// </summary>
        /// <param name="Window">The Window<see cref="Window"/></param>
        private void OnWindowLoaded(LoginView win)
        {
            //ProgressBar.Visibility = Visibility.Hidden;
            VisibilityProgressBar = Visibility.Hidden;
            VisibilityAuthenticated = Visibility.Hidden;
            VisibilityNotAuthenticated = Visibility.Hidden;
            VisiblityLoginWindow = Visibility.Visible;
            string isSave = Properties.Settings.Default["IsSave"] as string;
            if (isSave == "1")
            {
                string userEmail = Properties.Settings.Default["UserEmail"] as string;
                string userPassword = Properties.Settings.Default["UserPassword"] as string;
                win.tbEmail.Text = userEmail;
                win.PasswordBox.Password = userPassword;
                win.cBSave.IsChecked = true;
            }
        }

        /// <summary>
        /// The OnLogin
        /// </summary>
        /// <param name="passwordBox">The passwordBox<see cref="PasswordBox"/></param>
        private void OnLogin(LoginView win)
        {
            if (win.cBSave.IsChecked==true)
            {
                Properties.Settings.Default["UserEmail"] = win.tbEmail.Text;
                Properties.Settings.Default["UserPassword"] = win.PasswordBox.Password;
                Properties.Settings.Default["IsSave"] = "1";
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default["UserEmail"] ="";
                Properties.Settings.Default["UserPassword"] ="";
                Properties.Settings.Default["IsSave"] = "0";
                Properties.Settings.Default.Save();
            }
            VisibilityProgressBar = Visibility.Visible;

            Thread thread = new Thread(() =>
            {
                bool isAuthenticated = Model.LoginAsync(UserName, win.PasswordBox.Password);
                if (isAuthenticated)
                {
                    VisiblityLoginWindow = Visibility.Hidden;
                    VisibilityProgressBar = Visibility.Hidden;
                    VisibilityNotAuthenticated = Visibility.Hidden;
                    VisibilityAuthenticated = Visibility.Visible;
                }
                else
                {
                    VisiblityLoginWindow = Visibility.Hidden;
                    VisibilityProgressBar = Visibility.Hidden;
                    VisibilityNotAuthenticated = Visibility.Visible;
                    VisibilityAuthenticated = Visibility.Hidden;
                }
            });

            thread.Start();
        }

        /// <summary>
        /// The OnCancel
        /// </summary>
        /// <param name="win">The win<see cref="Window"/></param>
        private void OnCancel(Window win)
        {
            win.Close();
        }
    }
}
