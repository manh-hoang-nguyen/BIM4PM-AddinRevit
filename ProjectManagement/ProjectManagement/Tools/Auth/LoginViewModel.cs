namespace BIM4PM.UI.Tools.Auth
{
    using Autodesk.Revit.UI;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    using BIM4PM.UI.Commun;
    using System.Threading;
    using System.Windows;

    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;
    using System.Net.Mail;
    using BIM4PM.UI.Resources.Utils;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Collections;

    public class LoginViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        

        private UIApplication _uiapp;
         
        public LoginModel Model { get; set; }

        public RelayCommand<Window> Cancel { get; set; }

        public RelayCommand<Window> Authenticated { get; set; }

        public RelayCommand<LoginView> LoginCommand { get; set; }

        public RelayCommand<LoginView> WindowLoaded { get; set; } 

        private string _email { get; set; }

        private string _password { get; set; } 
        
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                RaisePropertyChanged("Email");
                GetErrorsForEmail(Email).ContinueWith((errorsTask) =>
                {
                    lock (_PropertyErrors)
                    {
                        _PropertyErrors["Email"] = errorsTask.Result;
                        ErrorsChanged(this, new DataErrorsChangedEventArgs("Email"));
                    }
                });
                LoginCommand.RaiseCanExecuteChanged();
            }
        }
        
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged("Password");
                GetErrorPassword(Password).ContinueWith((errorsTask) =>
                {
                    lock (_PropertyErrors)
                    {
                        _PropertyErrors["Password"] = errorsTask.Result;
                        ErrorsChanged(this, new DataErrorsChangedEventArgs("Password"));
                    }
                });
                LoginCommand.RaiseCanExecuteChanged();
            }
        }
        Task<List<string>> GetErrorPassword(string value)
        {
            return Task.Factory.StartNew(() =>
            { 
                
                if (string.IsNullOrEmpty(value) || value.Length <8)
                    return new List<string> { "Invalid Email" };
                return null;
            });
        }
        Task<List<string>> GetErrorsForEmail(string email)
        {
            return Task.Factory.StartNew(() =>
            {
                 
                Regex regex = new Regex(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");
                if (regex.Match(email) == Match.Empty)
                    return new List<string> { "Invalid Email" };
                return null;
            });

        }
        

        public bool HasErrors {
            get { return PropertyErrorsPresent(); }
        }
        Dictionary<string, List<string>> _PropertyErrors = new Dictionary<string, List<string>>();
        private bool PropertyErrorsPresent()
        {
            bool errors = false;
            foreach (var key in _PropertyErrors.Keys)
            {
                if (_PropertyErrors[key] != null)
                {
                    errors = true;
                    break;
                }
            }

            return errors;
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
            return !string.IsNullOrWhiteSpace( Email) && !string.IsNullOrWhiteSpace (Password) && !HasErrors;
        }

        private void OnAuthenticated(Window win)
        {
            win.Close();
        }

        private void OnWindowLoaded(LoginView win)
        { 
         
            string isSave = Properties.Settings.Default["IsSave"] as string;
            if (isSave == "1")
            {
                string userEmail = Properties.Settings.Default["UserEmail"] as string;
                string userPassword = Properties.Settings.Default["UserPassword"] as string;
                Email = userEmail;
                Password = userPassword;
                win.cBSave.IsChecked = true;
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
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged = delegate { };
        public IEnumerable GetErrors(string propertyName)
        {
            lock (_PropertyErrors)
            {
                if (_PropertyErrors.ContainsKey(propertyName))
                {
                    return _PropertyErrors[propertyName];
                }
            }
            return null;
        }
    }
}