using BIM4PM.UI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.UI.Commun
{
   public class DiscussionProvider
    {
        private DiscussionProvider()
        {
            AuthProvider.Instance.ConnectionChanged += (s, e) => Reset(); 

            Comments = new ObservableCollection<Comment>();
        }

        public static DiscussionProvider Instance { get; } = new DiscussionProvider();


        public RevitElement RevitElement { get; set; }

        public ObservableCollection<Comment> Comments { get; set; } 

        public void Reset()
        {
            if (AuthProvider.Instance.IsConnected == false)
            {
                RevitElement = null;
                Comments = new ObservableCollection<Comment>();
            } 
        }
    }
  
}
