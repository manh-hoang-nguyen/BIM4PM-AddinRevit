namespace BIM4PM.UI.Tools.Auth
{
    using Autodesk.Revit.UI;
    using BIM4PM.UI.Commun;
    using BIM4PM.UI.Events;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using Prism.Events;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Windows;

    public class LoginViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        #region Properties
        private IEventAggregator _eventAggregator;

        private UIApplication _uiapp;

        private LoginView _win;

        public LoginModel Model { get; set; }

        public RelayCommand<Window> Cancel { get; set; }

        public RelayCommand<Window> Authenticated { get; set; }

        public RelayCommand<LoginView> LoginCommand { get; set; }

        public RelayCommand<LoginView> WindowLoaded { get; set; }
        public RelayCommand WindowClosed{ get; set; }
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

        #endregion
        public LoginViewModel(UIApplication uiapp, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _uiapp = uiapp;
            Model = new LoginModel();
            WindowLoaded = new RelayCommand<LoginView>(OnWindowLoaded);
            Cancel = new RelayCommand<Window>(OnCancel);
            LoginCommand = new RelayCommand<LoginView>(OnLogin, CanLogin);
            Authenticated = new RelayCommand<Window>(OnAuthenticated);
            WindowClosed = new RelayCommand(OnWindowClosed);
        }

        private void OnWindowClosed()
        {
            Cleanup();
        }

        private bool CanLogin(LoginView arg)
        {
            return !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password) && !HasErrors;
        }

        private void OnAuthenticated(Window win)
        {
            win.Close();
        }

        private void OnWindowLoaded(LoginView win)
        {
            _win = win; 

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

            // Register email and password for next login
            if (win.cBSave.IsChecked == true)
            {
                Properties.Settings.Default["UserEmail"] = Email.Trim();
                Properties.Settings.Default["UserPassword"] = Password.Trim();
                Properties.Settings.Default["IsSave"] = "1";
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default["UserEmail"] = "";
                Properties.Settings.Default["UserPassword"] = "";
                Properties.Settings.Default["IsSave"] = "0";
                Properties.Settings.Default.Save();
            }
           
            var subjet = new Auth();
            //LaunchPanel launchPanel = new LaunchPanel();
            //subjet.Attach(launchPanel);
            var isAuth = subjet.Login(Email.Trim(), Password.Trim());
            if (isAuth)
            {
                _eventAggregator.GetEvent<AuthEvent>().Publish(true);
                win.Close();
            }
            else _eventAggregator.GetEvent<AuthEvent>().Publish(false);
        }

        private void OnCancel(Window win)
        {

            win.Close();
        }

        #region Errors
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
        Task<List<string>> GetErrorPassword(string value)
        {
            return Task.Factory.StartNew(() =>
            {

                if (string.IsNullOrEmpty(value))
                    return new List<string> { "Invalid Password" };
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

        public bool HasErrors
        {
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
        #endregion
    }
}
