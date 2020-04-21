using GalaSoft.MvvmLight;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BIM4PM.UI.Tools.Auth
{
   public class LoginInput : ViewModelBase, INotifyDataErrorInfo
    {
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
               
            }
        }
        Task<List<string>> GetErrorPassword(string value)
        {
            return Task.Factory.StartNew(() =>
            {

                if (string.IsNullOrEmpty(value) || value.Length < 8)
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
