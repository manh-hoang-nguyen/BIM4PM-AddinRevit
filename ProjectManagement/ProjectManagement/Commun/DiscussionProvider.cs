using ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Commun
{
   public class DiscussionProvider
    {
        private readonly static DiscussionProvider _instance = new DiscussionProvider();

        private DiscussionProvider()
        {
            Comments = new ObservableCollection<Comment>();
        }

        public static DiscussionProvider Instance => _instance;
        
 
        public RevitElement RevitElement { get; set; }

        public ObservableCollection<Comment> Comments { get; set; } 

        public void Reset()
        {
            RevitElement = null;
            Comments  = new ObservableCollection<Comment>();
          
        }
    }
  
}
