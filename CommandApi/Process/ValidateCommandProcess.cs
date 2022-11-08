using CommandApi.Controllers;
using CommandApi.DAL;
using CommandApi.Entity;
using CommandApi.Entity.Interface;
using CommandApi.Service;
using System.ComponentModel.Design;

namespace CommandApi.Process
{
    public interface IValidateComemandProcess
    {
        ValidateCommandProcessResult ValidateCommande(int commandId);

    }

    public class ValidateComemandProcess : IValidateComemandProcess
    {
        private Command command;

        private readonly ICommandPriceCalculator priceCalculator;
        private readonly ICommandRepository commandRepository;
        private readonly ICommandStockManager stockManager;
        private readonly IClientCrm clientCrm;

        public ValidateComemandProcess(ICommandPriceCalculator commandPriceCalculator, IClientCrm clientCrm, ICommandRepository commandRepository, ICommandStockManager stockManager)
        {
            priceCalculator = commandPriceCalculator;
            this.commandRepository = commandRepository;
            this.stockManager = stockManager;
            this.clientCrm = clientCrm;
        }


        public ValidateCommandProcessResult ValidateCommande(int commandId)
        {
            try
            {
                InitialiseCommand(commandId);

                CheckStockAvailability();
                
                double prixCommande = priceCalculator.GetPriceTtc(command);

                bool isCommandinProgress = clientCrm.HasCommandInProgress(command.GetClientId());

                return CheckClientAvailability(prixCommande, isCommandinProgress);
            }
            catch (Exception e)
            {
                return new ValidateCommandProcessResult() { IsSucces = false, Message = e.Message };
            }
        }

        private void CheckStockAvailability()
        {
            if (!stockManager.IsStockAvailabelForCommand(command))
            {
                throw new Exception("Not enoug product in stock");
            }
        }

        private static ValidateCommandProcessResult CheckClientAvailability(double prixCommande, bool isCommandinProgress)
        {
            if (isCommandinProgress && prixCommande > 10000)
            {
                return new ValidateCommandProcessResult() { IsSucces = false, Message = "A command is already in progress" };
            }
            else
            {
                return new ValidateCommandProcessResult()
                {
                    IsSucces = true,
                };
            }
        }


        private void InitialiseCommand(int commandId)
        {
            command = commandRepository.GetCommand(commandId);
        }

        
    }

    public class ValidateCommandProcessResult
    {
        public bool IsSucces { get; internal set; }
        public string Message { get; internal set; }
    }
}

