using ProjectManagement.Models;
using System.Collections.Generic;

namespace ProjectManagement.Commun

{   
   //public static class TokenUser
   // {
   //     public static Token token ;
   // }
    public static class JsonPost
    {
        public static List<JsonToPostComparison> PostComparison_NewElement=new List<JsonToPostComparison>();
        public static List<JsonToPostComparison> PostComparison_ModifiedElement = new List<JsonToPostComparison>();

    }
    public static class GuidList
    {
        public static IEnumerable<string> guid_deletedElement = new List<string>();
        public static IEnumerable<string> guid_newElement = new List<string>();
        public static IEnumerable<string> guid_ElementToExamine = new List<string>();
        public static List<string> guid_modifiedElement = new List<string>();
        public static List<string> guid_sameElement = new List<string>();
        public static IEnumerable<string> guidInModel;

    }
    public static class DataList
    {
        public static IEnumerable<Data> DataInDatabase;
        public static List<Data> DataModel;
        public static IEnumerable<Data> DeletedElement;
       
        public static IEnumerable<Data> NewElement;
        public static IEnumerable<Data> ModifiedElement;
        public static IEnumerable<Data> SameElement;
        public static IEnumerable<Data> ElementToExamine;
    }
    public static class ComparisonList
    {
        public static IEnumerable<Comparison> ComparisonInDatabase;
        public static List<Comparison> ComparisonInModel;

    }
    public static class HistoryList
    {
        public static List<Historyx> HistoryInDatabase;
        public static List<Comment> CommentInDatabase = new List<Comment>();

    }

    public static class CommentList
    {
        //public static List<Comment> CommentInDatabase;
    }
    
}