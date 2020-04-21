namespace BIM4PM.DataAccess
{
    public class RevitElementRoute
    {
        public RevitElementRoute(string versionId)
        {
            GetElementsUrl = $"{RouteBase.ApiPrefix}/versions/{versionId}/revitelements";
            GetEleInPeriodUrl = $"{GetElementsUrl}/period";
        }

        public string GetElementsUrl { get; set; }

        public string GetEleInPeriodUrl { get; set; }
    }
}
