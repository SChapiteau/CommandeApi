using CommandApi.Process;
using Microsoft.AspNetCore.Mvc;

namespace CommandApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommandeController : ControllerBase
    {

        [HttpGet]
        public ApiResult ValidateCommande(int commandeId)
        {
            var validateCommandeProcess = DependencyResolver.Resolve<IValidateCommandeProcess>();
            var processResult = validateCommandeProcess.ValidateCommande(commandeId);

            return new ApiResult
            {
                IsSucces = processResult.IsSucces,
                Message = processResult.Message,
            };
        }

        
    }
}