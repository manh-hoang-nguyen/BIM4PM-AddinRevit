namespace BIM4PM.UI.Tools.Auth
{
    using Autodesk.Revit.UI;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    using BIM4PM.UI.Commun;


    using System.Threading;
    using System.Windows;
    using Visibility = System.Windows.Visibility;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class LoginViewModel : ViewModelBase
    {
        private TestDataValidattion _input;
        public TestDataValidattion Input { get => _input; set { _input = value; RaisePropertyChanged("Email"); }  }

        private UIApplication _uiapp;

        public LoginModel Model { get; set; }

        public RelayCommand<Window> Cancel { get; set; }

        public RelayCommand<Window> Authenticated { get; set; }

        public RelayCommand<LoginView> LoginCommand { get; set; }

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
            get => _password;
            set {
                //if (value.Length <8  )
                //    throw new ArgumentException("The age must be between 10 and 100");
                 
                _password = value;
                RaisePropertyChanged("Password"); }
        }

        public LoginViewModel(UIApplication uiapp)
        {
            _uiapp = uiapp;
            Model = new LoginModel();
            WindowLoaded = new RelayCommand<LoginView>(OnWindowLoaded);
            Cancel = new RelayCommand<Window>(OnCancel);
            LoginCommand = new RelayCommand<LoginView>(OnLogin, CanLogin);
            Authenticated = new RelayCommand<Window>(OnAuthenticated);
        }

        private bool CanLogin(LoginView arg)
        {
            return true;
        }

        private void OnAuthenticated(Window win)
        {
            win.Close();
        }

        private void OnWindowLoaded(LoginView win)
        {
            

            VisibilityProgressBar = Visibility.Hidden;
            VisibilityAuthenticated = Visibility.Hidden;
            VisibilityNotAuthenticated = Visibility.Hidden;
            VisiblityLoginWindow = Visibility.Visible;
            string isSave = Properties.Settings.Default["IsSave"] as string;
            if (isSave == "1")
            {
                string userEmail = Properties.Settings.Default["UserEmail"] as string;
                string userPassword = Properties.Settings.Default["UserPassword"] as string;
                //win.emailTextBox.Text = userEmail;
                ////win.PasswordBox.Password = userPassword;
                //win.cBSave.IsChecked = true;
            }
        }

        private void OnLogin(LoginView win)
        { 
             
            //// Register email and password for next login
            //if (win.cBSave.IsChecked == true)
            //{
            //    //Properties.Settings.Default["UserEmail"] = Email;
            //   // Properties.Settings.Default["UserPassword"] = win.PasswordBox.Password;
            //    Properties.Settings.Default["IsSave"] = "1";
            //    Properties.Settings.Default.Save();
            //}
            //else
            //{
            //    Properties.Settings.Default["UserEmail"] = "";
            //    Properties.Settings.Default["UserPassword"] = "";
            //    Properties.Settings.Default["IsSave"] = "0";
            //    Properties.Settings.Default.Save();
            //}
            //VisibilityProgressBar = Visibility.Visible;

            //Thread thread = new Thread(() =>
            //{
            //    var subjet = new Auth();
            //    LaunchPanel launchPanel = new LaunchPanel();
            //    subjet.Attach(launchPanel);
            //    //subjet.Login(Email, win.PasswordBox.Password);

            //    if (Auth.IsAuthenticated)
            //    {
            //        VisiblityLoginWindow = Visibility.Hidden;
            //        VisibilityProgressBar = Visibility.Hidden;
            //        VisibilityNotAuthenticated = Visibility.Hidden;
            //        VisibilityAuthenticated = Visibility.Visible;
                    
            //    }
            //    else
            //    {
            //        VisiblityLoginWindow = Visibility.Hidden;
            //        VisibilityProgressBar = Visibility.Hidden;
            //        VisibilityNotAuthenticated = Visibility.Visible;
            //        VisibilityAuthenticated = Visibility.Hidden;
                    
            //    }
            //});

            //thread.Start();
        }

        private void OnCancel(Window win)
        {
            win.Close();
        }
    }
}
