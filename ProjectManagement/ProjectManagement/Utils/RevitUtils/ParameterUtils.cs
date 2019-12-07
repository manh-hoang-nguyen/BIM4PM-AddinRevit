namespace ProjectManagement.Utils.RevitUtils
{
    using Autodesk.Revit.DB;
    using System.Collections.Generic;

    public class ParameterUtils
    {
        /// <summary>
        /// The SerializeRevitParameters
        /// </summary>
        /// <param name="e">The e<see cref="Element"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string SerializeRevitParameters(Element e)
        {
            string parameters = "";
            IList<Parameter> paraList = e.GetOrderedParameters();

            foreach (Parameter para in e.GetOrderedParameters())
            {
                if (para.IsShared == false)
                {
                    string val = null;
                    //exclude geometryParameters
                    if (para.Definition.ParameterGroup == BuiltInParameterGroup.PG_CONSTRAINTS
                        || para.Definition.ParameterGroup == BuiltInParameterGroup.PG_GEOMETRY)
                    {
                        if (para.StorageType != StorageType.Double)
                        {
                            switch (para.StorageType)
                            {
                                case StorageType.String:
                                    val = para.AsString();
                                    break;

                                default:
                                    val = para.AsValueString();
                                    break;
                            }
                        }
                    }
                    else
                    {
                        switch (para.StorageType)
                        {
                            case StorageType.String:
                                val = para.AsString();
                                break;

                            default:
                                val = para.AsValueString();
                                break;
                        }
                    } 

                    if (val == null) val = "(n/a)";
                    parameters += string.Format("{0}:{1}={2}", para.Id, para.Definition.Name, val) + ";";
                }
            }

            return parameters;
        }

        /// <summary>
        /// get all parameter in group parameter constraints and geometry
        /// </summary>
        /// <param name="e">The e<see cref="Element"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string SerializeGeoParameters(Element e)
        {
            string parameters = "";
            IList<Parameter> paraList = e.GetOrderedParameters();

            foreach (Parameter para in e.GetOrderedParameters())
            {
                if (para.IsShared == false
                    && para.StorageType == StorageType.Double
                    && (para.Definition.ParameterGroup == BuiltInParameterGroup.PG_CONSTRAINTS 
                    || para.Definition.ParameterGroup == BuiltInParameterGroup.PG_GEOMETRY))
                {
                    string val = para.AsDouble().ToString();

                    parameters += string.Format("{0}:{1}={2}", para.Id, para.Definition.Name, val) + ";";
                }
            }

            return parameters;
        }

        /// <summary>
        /// The SerializeSharedParameters
        /// </summary>
        /// <param name="e">The e<see cref="Element"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string SerializeSharedParameters(Element e)
        {
            string parameters = "";
            IList<Parameter> paraList = e.GetOrderedParameters();

            foreach (Parameter para in e.GetOrderedParameters())
            {

                if (para.IsShared == true)
                {

                    parameters += string.Format("{0}:{1}={2}", para.Id, para.Definition.Name, ParameterToString(para)) + ";";
                }
            }

            return parameters;
        }

        /// <summary>
        /// Helper function: return a string form of a given parameter.
        /// </summary>
        /// <param name="param">The param<see cref="Parameter"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string ParameterToString(Parameter param)
        {
            string val = "none";

            if (param == null)
            {
                return val;
            }

            // To get to the parameter value, we need to parse it depending on its storage type

            switch (param.StorageType)
            {
                case StorageType.Double:
                    double dVal = param.AsDouble();
                    val = dVal.ToString();
                    break;
                case StorageType.Integer:
                    int iVal = param.AsInteger();
                    val = iVal.ToString();
                    break;
                case StorageType.String:
                    string sVal = param.AsString();
                    val = sVal;
                    break;
                case StorageType.ElementId:
                    ElementId idVal = param.AsElementId();
                    val = idVal.IntegerValue.ToString();
                    break;

                case StorageType.None:
                    break;
            }
            return val;
        }

        /// <summary>
        /// The GetAllParametersElement
        /// </summary>
        /// <param name="e">The e<see cref="Element"/></param>
        /// <returns>The <see cref="Dictionary{string, string}"/></returns>
        public static Dictionary<string, string> GetAllParametersElement(Element e)
        {
            //Key is parameter name, value is paramter value
            Dictionary<string, string> dic = new Dictionary<string, string>();

            IList<Parameter> parameters = e.GetOrderedParameters();
            List<string> param_values = new List<string>(parameters.Count);
            foreach (Parameter p in parameters)
            {
                // AsValueString displays the value as the 
                // user sees it. In some cases, the underlying
                // database value returned by AsInteger, AsDouble,
                // etc., may be more relevant.

                param_values.Add(string.Format("{0}={1}",
                  p.Definition.Name, p.AsValueString()));
            }
            foreach (Parameter p in parameters)
            {
                var xxx = p.AsValueString();
                var xx = p.Definition.ParameterType;

                dic.Add(p.Id.ToString(), ParameterToString(p));
            }

            return dic;
        }
    }
}
