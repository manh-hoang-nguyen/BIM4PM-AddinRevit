using System.ComponentModel;
using System.Windows;

namespace ProjectManagement.FormInterface
{
    /// <summary>
    /// Logique d'interaction pour frm_ProgressBar.xaml
    /// </summary>
    public partial class frm_ProgressBar : Window, INotifyPropertyChanged
    {
        private BackgroundWorker _bgWorker = new BackgroundWorker();

        private int _workerState;
        private double _pourcentage;

        public int WorkerState
        {
            get => _workerState;
            set
            {
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

        public frm_ProgressBar(int nbWork, string comment)
        {
            InitializeComponent();

            prgBar.Maximum = nbWork;
            DataContext = this;
            _bgWorker.DoWork += (s, ee) =>
            {
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