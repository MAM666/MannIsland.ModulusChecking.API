using MannIsland.ModulusChecking.API.Services;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MannIsland.ModulusChecking.API.Controllers
{
    [Route("api/ModulusController")]
    public class ModulusController : ApiController
    {

        private readonly IModulusWeightService _modulusWeightService;

        public ModulusController(IModulusWeightService modulusWeightService)
        {
            _modulusWeightService = modulusWeightService;
        }

        // GET: ModulusWeightController/12345,12345678
        [HttpGet]
        /// <summary>
        /// Check that the sortcode_accountNumber are valid
        /// </summary>
        /// <param name="sortcode">the sortcode to match and validate</param>
        /// <param name="accountNumber">the account number to validate</param>
        /// <returns>Y = true : N = fails</returns>
        public HttpResponseMessage Get(string sortcode, string accountNumber)
        {
            // start a process to validate
            //Services.ModulusWeightService service = new Services.ModulusWeightService();
            if (_modulusWeightService == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(_modulusWeightService.Validate(sortcode, accountNumber));
        }
    }

}
