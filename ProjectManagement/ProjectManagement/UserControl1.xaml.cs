namespace ProjectManagement
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using Xceed.Wpf.Toolkit.PropertyGrid;
    using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

    /// <summary>
    /// Logique d'interaction pour UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl, ITypeEditor
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        //public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
        //"Value", typeof(IList<string>), typeof(UserControl1), new PropertyMetadata(default(IList<string>)));
       
        public FrameworkElement ResolveEditor(PropertyItem propertyItem)
        {
            throw new NotImplementedException();
        }
    }
}
