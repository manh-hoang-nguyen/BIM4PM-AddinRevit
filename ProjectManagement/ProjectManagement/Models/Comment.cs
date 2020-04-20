using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.UI.Models
{
    public class CommentRes
    {
        public string success { get; set; }
        public CommentChildRes data { get; set; }
    }
    public class CommentResPost
    {
        public string success { get; set; }

        public Comment data { get; set; }
    }
    public class CommentChildRes
    {
        public string _id { get; set; }
        public List<Comment> comments { get; set; }
    }
    public class Comment
    {
        public string _id { get; set; }
        public TypeComment type { get; set; }
       
        public string text { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }

    }
    public class ChildComment
    {
        public string _id { get; set; }
        public TypeComment type { get; set; }
         
        
        public string text { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }

    public enum TypeComment
    {
        comment,
        demand,
        question
    }
}
