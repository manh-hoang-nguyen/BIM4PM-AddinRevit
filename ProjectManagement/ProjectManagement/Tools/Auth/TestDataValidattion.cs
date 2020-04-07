using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.UI.Tools.Auth
{
   public class TestDataValidattion : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private string _email;
        [EmailAddress]
        [Required]
        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Email"));
                }
            }
        }
        private string _password;

        public event PropertyChangedEventHandler PropertyChanged;

        [Required]
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("CellPhone"));
                    ValidatePassword(Password);
                }
            }
        }

        public bool HasErrors { get { return PropertyErrorsPresent(); } }

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
        Dictionary<string, List<string>> _PropertyErrors = new Dictionary<string, List<string>>();
        /// <summary>
        List<string>  ValidatePassword(string value)
        {
            if(value.Length < 8)
            {
                return new List<string> { "Invalid password" };
            }
            return null;
        }

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
