using Newtonsoft.Json;
using System.Collections.Generic;

namespace MannIsland.ModulusChecking.API.ExtensionMethods
{

    /// <summary>
    /// JSON structure
    /// </summary>
    public class RootObject
    {
        public string Valid { get; set; }
        public List<string> ExceptionRulesNotApplied { get; set; }
    }

    public static class JSONBuilder
    {
        /// <summary>
        /// Create the JSON string to return too the API
        /// </summary>
        /// <param name="valid">was the code valid</param>
        /// <param name="exceptionRulesNotApplied">the exception rules that were not applied</param>
        /// <returns>JSON string.</returns>
        public static string ToJSON(string valid, List<string> exceptionRulesNotApplied)
        {
            RootObject obj = new RootObject();
            obj.Valid = valid;

            if (exceptionRulesNotApplied.Count > 0)
                obj.ExceptionRulesNotApplied = new List<string>();
            foreach (string s in exceptionRulesNotApplied)
            {
                obj.ExceptionRulesNotApplied.Add(s);
            }

            return JsonConvert.SerializeObject(obj);
        }

    }

}