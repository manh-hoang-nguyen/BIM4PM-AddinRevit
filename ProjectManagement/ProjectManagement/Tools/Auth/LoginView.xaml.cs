 
using System.Windows;
using System.Windows.Controls;
using BIM4PM.Common;
using BIM4PM.Common.Wpf;

namespace BIM4PM.UI.Tools.Auth
{
    /// <summary>
    /// Logique d'interaction pour LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private LoginViewModel _loginViewModel;
        public LoginView(LoginViewModel loginViewModel)
        {
             
            InitializeComponent(); 
            _loginViewModel = loginViewModel;
            DataContext = _loginViewModel;
        }

       
    }
}