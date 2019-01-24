using MannIsland.ModulusChecking.API.Models;
using System.Collections.Generic;

namespace MannIsland.ModulusChecking.API.FileAccess
{
    /// <summary>
    /// The required contracts for the transalation
    /// </summary>
    public interface IModulusWeightTranslator
    {
        void Translate(ref List<ModulusWeight> moduleweightList, string sortcode, string[] fields);
    }
}