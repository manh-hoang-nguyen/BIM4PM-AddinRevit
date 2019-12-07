using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using Autodesk.Revit.DB;
using ProjectManagement.Commun;
using Newtonsoft.Json;
using ProjectManagement.Controllers;
using ProjectManagement.Models;

namespace ProjectManagement.FormInterface
{
    /// <summary>
    /// Logique d'interaction pour frm_ProgressBar.xaml
    /// </summary>
    public partial class frm_ProgressBar : Window,INotifyPropertyChanged
    {
        private BackgroundWorker _bgWorker = new BackgroundWorker();
        
        private  int _workerState;
        private double _pourcentage;
        public int WorkerState
        {
            get => _workerState;
            set {
                _workerState = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WorkerState"));
            }
        }

        public double Pourcentage
        {
            get => _pourcentage;
            set
            {
                _pourcentage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Pourcentage"));
            }
        }

        public frm_ProgressBar( int nbWork,string comment )
        {
            InitializeComponent();
            
            prgBar.Maximum = nbWork;
            DataContext = this;
            _bgWorker.DoWork += (s, ee) =>
            {
                foreach (string id in GuidList.guid_newElement)
                {
                    Element e = frm_Login._doc.GetElement(id);
                    JsonToPostComparison comparison = ComparisonController.CreateJsonPost(e, comment, UserData.user.email);
                    string jsonNew = JsonConvert.SerializeObject(comparison, Formatting.Indented);

                    string jsNewx = "{\"projectId\":\"" + UserData.idProjectActive
                           + "\",\"version\":\"" + ProjectProvider.Instance.CurrentVersion.version
                           + "\",\"data\":[" + jsonNew + "]}";
                    ComparisonController.PostComparison_NewElement(jsNewx);


                    WorkerState++;
                    Pourcentage = WorkerState / nbWork*100;

                }
                foreach (string id in GuidList.guid_modifiedElement)
                {
                    Element e = frm_Login._doc.GetElement(id);
                    JsonToPostComparison comparison = ComparisonController.CreateJsonPost(e, comment, UserData.user.email);
                    string jsonModif = JsonConvert.SerializeObject(comparison, Formatting.Indented);
                    WorkerState++;
                    Pourcentage = WorkerState / nbWork * 100;

                }
                foreach(string id in GuidList.guid_deletedElement)
                {
                    string jsDel = "{\"projectId\":\"" + UserData.idProjectActive + "\",\"data\":[{\"guid\":\"" + id + "\"}]}";


                    WorkerState++;
                    Pourcentage = WorkerState / nbWork * 100;
                }
                MessageBox.Show("Work is done..."+ WorkerState.ToString());
            };

            //for (int i = 0; i <= nbWork; i++)
            //{

            //    WorkerState += 100/nbWork;
            //}
            //MessageBox.Show("Work is done...");
            _bgWorker.RunWorkerAsync();

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
