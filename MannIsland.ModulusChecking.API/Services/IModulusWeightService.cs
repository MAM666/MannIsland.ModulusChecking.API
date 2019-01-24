
using System.Collections.Generic;

namespace MannIsland.ModulusChecking.API.Services
{
    /// <summary>
    /// The required contracts for the validation and calculation process.
    /// </summary>
    public interface IModulusWeightService
    {
        List<string> ExceptionRulesNotApplied { get; }

        string Validate(string sortcode, string accountNumber);

    }
}