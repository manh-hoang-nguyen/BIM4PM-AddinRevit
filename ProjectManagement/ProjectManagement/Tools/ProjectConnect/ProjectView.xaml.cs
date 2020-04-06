namespace BIM4PM.UI.Tools.Project
{
    using System.Windows;
    using System.Windows.Controls;

    public partial class ProjectView : UserControl
    {
        public ProjectView()
        {
            InitializeComponent();
        }

        private void TextBlock_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MessageBox.Show("ckeii");
        }
    }
}
