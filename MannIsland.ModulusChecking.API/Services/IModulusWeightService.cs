
namespace MannIsland.ModulusChecking.API.Services
{
    /// <summary>
    /// The required contracts for the validation and calculation process.
    /// </summary>
    public interface IModulusWeightService
    {
        string Validate(string sortcode, string accountNumber);

    }
}