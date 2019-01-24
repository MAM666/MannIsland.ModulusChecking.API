using MannIsland.ModulusChecking.API.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MannIsland.ModulusChecking.API.FileAccess
{
    /// <summary>
    /// Read in a resource
    /// </summary>
    public class ReadResource
    {
        /// <summary>
        ///     Find the matched range Sortcode and return an array of lines
        /// </summary>
        /// <param name="sortcode"></param>
        /// <returns>a list of ModulusWeight modals</returns>
        public static List<ModulusWeight> FindMatched_Sortcodes(string sortcode)
        {
            // list of rows that match the sortcode
            List<ModulusWeight> mwList = new List<ModulusWeight>();

            // point to the assembly resource file
            var assembly = Assembly.GetExecutingAssembly();
            string resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith(".valacdos.txt"));
            ModulusWeightTranslator translator = new ModulusWeightTranslator();

            // start the stream reader.
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                // loop the stream until completed
                string result;
                while ((result = reader.ReadLine()) != null)
                {
                    // look in the translator to see if there is a match and return the list 1 elemenet larger.
                    translator.Translate(ref mwList, sortcode, System.Text.RegularExpressions.Regex.Split(result, @"\s{1,}"));
                }
            }

            // return the list of matches
            return mwList;
        }
    }

}