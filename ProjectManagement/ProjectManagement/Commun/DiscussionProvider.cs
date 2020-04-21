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
            

            Comments = new ObservableCollection<Comment>();
        }

        public static DiscussionProvider Instance { get; } = new DiscussionProvider();


        public RevitElement RevitElement { get; set; }

        public ObservableCollection<Comment> Comments { get; set; } 

        public void Reset()
        {
            
             
                RevitElement = null;
                Comments = new ObservableCollection<Comment>();
            
        }
    }
  
}
