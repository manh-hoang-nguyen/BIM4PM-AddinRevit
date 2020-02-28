using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using ProjectManagement.Commun;
using ProjectManagement.Controllers;
using ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace ProjectManagement.FormInterface
{
    /// <summary>
    /// Logique d'interaction pour PanelProprety.xaml
    /// </summary>
    public partial class PanelProprety : Page, IDockablePaneProvider 
    {
        public static UIDocument _uiDoc;
         

        private static string _guid;

        public static string Guid
        {
            get => _guid;
            set
            {
              
                if (_guid != value)
                {
                    _guid = value;
                    
                }
            }
        }

       

        private static ObservableCollection<Modification> modifications = new ObservableCollection<Modification>();
        private static ObservableCollection<Modification> tmp_modifications = new ObservableCollection<Modification>();
        private static ObservableCollection<ChildComment> comments = new ObservableCollection<ChildComment>();
        private static ObservableCollection<ChildComment> tmp_comments = new ObservableCollection<ChildComment>();

        public PanelProprety()
        {
            InitializeComponent();
           
            history_element.ItemsSource = modifications;
           
            comment_element.ItemsSource = comments;
            
           
        }

        public void SetupDockablePane(DockablePaneProviderData data)
        {
            data.FrameworkElement = this;

            data.InitialState.DockPosition = DockPosition.Tabbed;
            data.InitialState.TabBehind = DockablePanes.BuiltInDockablePanes.PropertiesPalette;
            data.VisibleByDefault = false;
            //data.FrameworkElement = panelPropreties as System.Windows.FrameworkElement;
            //data.InitialState = new DockablePaneState();

            //data.InitialState.DockPosition = DockPosition.Tabbed;
        }

        private void Button_Click_comment(object sender, RoutedEventArgs e)
        {
            string auteur = "Manh Hoang";
            ChildComment comment = new ChildComment
            {
                //authorName = UserData.user.userName,
                text = txbComment.Text,
                createdAt = DateTime.Now
            };
            string json = "{\"guid\":\"" + _guid + "\",\"auteur\":\"" + auteur + "\",\"comment\":\"" + txbComment.Text + "\"}";
            ChildComment com = CommentController.PostComment(json,UserData.authentication.token);
            comments.Insert(0,com);
            ////Comment cc = (from c in HistoryList.CommentInDatabase
            //              where c.guid == Guid
            //              select c).FirstOrDefault();
           

            
            foreach (Comment item in HistoryList.CommentInDatabase)
            {
                //if (item.guid == Guid)
                //{
                     
                //    //item.comments[item.comments.Length] = com;
                //    item.comments.Add(com);
                //    HistoryList.CommentInDatabase.Insert(i, item);
                //    HistoryList.CommentInDatabase.RemoveAt(i+1);
                //    break;
                //}
                //i++;
            }
            comment_element.Height = PanelHistory.Height/2.5;
            txbComment.Text = "";
             
        }

        public static void PanelEvent(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Title")
            {
                comments.Clear();
                modifications.Clear();
                 
                Selection selection = _uiDoc.Selection;
                ICollection<ElementId> ids = _uiDoc.Selection.GetElementIds();
                Document doc = _uiDoc.Document;

                int n = ids.Count;
                switch (n)
                {
                    case 0: break;
                    case 1:
                        try
                        {
                            Guid = doc.GetElement(ids.ElementAt(0)).UniqueId;

                            //Comment xx = (from e1 in HistoryList.CommentInDatabase
                            //              where e1.guid == Guid
                            //              select e1).FirstOrDefault();

                            Historyx x = (from e1 in HistoryList.HistoryInDatabase
                                          where e1.guid == Guid
                                          select e1).FirstOrDefault();
                            for (int i = 1; i <= x.modifications.Count(); i++)
                            {
                                modifications.Add(x.modifications[x.modifications.Count() - i]);
                            }
                            //for (int i = 1; i <= xx.comments.Count(); i++)
                            //{
                            //    comments.Add(xx.comments[xx.comments.Count() - i]);
                            //}
                            //comments = new ObservableCollection<ChildComment>(tmp_comments.Reverse());
                        }
                        catch
                        {

                        }

                        break;

                    default: break;
                }
            }
        }

        private void PanelHistory_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //comment_element.Height = PanelHistory.Height - Row1.Height.Value - Row3.Height.Value - tbComment.Height;
        }
    }

    
}