using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Routes
{
    public class CommentRouter
    {
         /// <summary>
         /// Json format: projectId, guid, auteur, comment
         /// </summary>
        public static string PostComment = "https://manh-hoang.herokuapp.com/api/comment";
        /// <summary>
        /// json: projectId, guid
        /// </summary>
        public static string GetComment= "https://manh-hoang.herokuapp.com/api/comment";
        /// <summary>
        /// Json: projectId
        /// </summary>
        public static string GetAllComments = "https://manh-hoang.herokuapp.com/api/comment/all";
        /// <summary>
        /// projectId,guid, commentId
        /// </summary>
        public static string DeleteComment = "https://manh-hoang.herokuapp.com/api/comment";
    }
}
