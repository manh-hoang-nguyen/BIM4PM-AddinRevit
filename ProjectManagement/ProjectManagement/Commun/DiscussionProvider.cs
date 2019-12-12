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
        private static DiscussionProvider _ins;

        public static DiscussionProvider Instance
        {
            get
            {
                if (_ins == null)
                    _ins = new DiscussionProvider();
                return _ins;
            }
            set
            {
                _ins = value;
            }
        }
        public RevitElement RevitElement { get; set; }

        public ObservableCollection<Comment> Comments { get; set; } = new ObservableCollection<Comment>();

        public void Reset()
        {
            RevitElement = null;
            Comments  = new ObservableCollection<Comment>();
          
        }
    }
  
}
