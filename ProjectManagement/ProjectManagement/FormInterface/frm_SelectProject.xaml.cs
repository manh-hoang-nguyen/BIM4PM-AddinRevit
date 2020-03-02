namespace ProjectManagement.FormInterface
{
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using ProjectManagement.Commun;
    using ProjectManagement.Models;
    using System.Windows;

    /// <summary>
    /// Logique d'interaction pour frm_SelectProject.xaml
    /// </summary>
    public partial class frm_SelectProject : Window
    {
        internal UIApplication _uiapp;

        public static Document _doc;

        public frm_SelectProject(UIApplication uiapp, Document doc)
        {
            InitializeComponent();
            _uiapp = uiapp;
            _doc = doc;
            cbSelectProject.ItemsSource = UserData.userProject;
            cbSelectProject.DisplayMemberPath = "projectName";
            foreach (Document document in uiapp.Application.Documents)
            {
                cbSelectProjectRevit.Items.Add(document.Title);
            }
        }

        /// <summary>
        /// The Button_Click
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
         

            Close();
        }

        /// <summary>
        /// The CbSelectProject_SelectionChanged
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="System.Windows.Controls.SelectionChangedEventArgs"/></param>
        private void CbSelectProject_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            UserProject userProject = cbSelectProject.SelectedItem as UserProject;
            UserData.idProjectActive = userProject.projectId;
            App._projectName = userProject.projectName;
            if (cbSelectProjectRevit.SelectedItem != null) btnOK.IsEnabled = true;
        }

        /// <summary>
        /// The CbSelectProjectRevit_SelectionChanged
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="System.Windows.Controls.SelectionChangedEventArgs"/></param>
        private void CbSelectProjectRevit_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cbSelectProject.SelectedItem != null) btnOK.IsEnabled = true;
        }
    }
}
