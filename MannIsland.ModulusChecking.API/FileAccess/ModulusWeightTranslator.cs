using MannIsland.ModulusChecking.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MannIsland.ModulusChecking.API.FileAccess
{
    /// <summary>
    /// translate from a resource text file to the ModulusWeight structure model.
    /// </summary>
    public class ModulusWeightTranslator : IModulusWeightTranslator
    {
        /// <summary>
        /// Translate the matched string sortcodes to a ModulusWeight structure
        /// </summary>
        /// <param name="mwList">the list of ModulusWeight</param>
        /// <param name="sortcode">sortcode to match</param>
        /// <param name="fields">weight fields from the file</param>
        public void Translate(ref List<ModulusWeight> modulusweightList, string sortcode, string[] fields)
        {
            //need at least 2 elements
            if (fields.GetLength(0) < 2) return;

            // check the range of the sortcode
            if (!(fields[0].Equals(sortcode) | fields[1].Equals(sortcode)))
            {
                // is it outside the range, cant use this line so lets go back.
                if (Convert.ToInt32(sortcode) < Convert.ToInt32(fields[0]) | Convert.ToInt32(sortcode) > Convert.ToInt32(fields[1]))
                    return;
            }

            // line is valid so lets add the vlaues to the model.
            var modulusweight = new ModulusWeight
            {
                SortcodeStart = fields[0],
                SortcodeEnd = fields[1],
                AIg = fields[2],
                Weight = new List<int>(),
                ExceptionRule = 0
            };

            //populate the weight
            int max = fields.Count();
            // is there an exception code
            if (max > 17)
            {
                modulusweight.ExceptionRule = Convert.ToInt16(fields[17]);
                max = 17;

            }
            for (int i = 3; i < max; i++)
            {
                modulusweight.Weight.Add(Convert.ToInt16(fields[i]));
            }

            // add it to the list.
            modulusweightList.Add( modulusweight );
        }
    }
}