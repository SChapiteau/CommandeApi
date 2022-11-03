using CommandApi.DAL;
using CommandApi.Entity;
using CommandApi.Process;
using CommandApi.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CommandApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommandController : ControllerBase
    {

        [HttpGet]
        public ApiResult ValidateCommand(int commandId)
        {
            var validateCommandProcess = new ValidateCommandProcess();
            var processResult = validateCommandProcess.ValidateCommande(commandId);

            return new ApiResult()
            {
                IsSucces = processResult.IsSucces,
                Message = processResult.Message,
            };
        }

        
    }
}