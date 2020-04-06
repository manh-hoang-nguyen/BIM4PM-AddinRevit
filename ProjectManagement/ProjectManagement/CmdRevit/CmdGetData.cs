using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using GraphQL;
using GraphQL.Client.Http;
using System.Threading.Tasks;
using System.Linq;
using System.Text.Json; 
using GraphQL.Client.Serializer.Newtonsoft;
using System.Collections.Generic;

namespace BIM4PM.UI.CmdRevit
{
    [Transaction(TransactionMode.ReadOnly)]
    public class CmdGetData : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Task task = getComment();

            return Result.Succeeded;
        }



        private async Task<GraphQLResponse<PersonAndFilmsResponse>> getComment()
        {

            var graphQLClient = new GraphQLHttpClient("https://swapi.apis.guru/");

            var personAndFilmsRequest = new GraphQLRequest
            {
                Query = @"
			     {
			        revitElements { 
                                location
                                level
                                volume
                                centroid
                                geometryParameters 
                                }
			    }"
            };

            var graphQLResponse = await graphQLClient.SendQueryAsync<PersonAndFilmsResponse>(personAndFilmsRequest);

            var x = JsonSerializer.Serialize(graphQLResponse, new JsonSerializerOptions { WriteIndented = true });

            return graphQLResponse;
        }
    }
    public class PersonAndFilmsResponse
    {
        public PersonContent Person { get; set; }

        public class PersonContent
        {
            public string Name { get; set; }
            public FilmConnectionContent FilmConnection { get; set; }

            public class FilmConnectionContent
            {
                public List<FilmContent> Films { get; set; }

                public class FilmContent
                {
                    public string Title { get; set; }
                }
            }
        }
    }
}