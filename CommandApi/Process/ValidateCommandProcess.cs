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
        private readonly IClientCrm clientCrm;

        public ValidateComemandProcess(ICommandPriceCalculator commandPriceCalculator, IClientCrm clientCr, ICommandRepository commandRepository)
        {
            priceCalculator = commandPriceCalculator;
            this.commandRepository = commandRepository;
            this.clientCrm = clientCrm;
        }


        public ValidateCommandProcessResult ValidateCommande(int commandId)
        {
            try
            {
                InitialiseCommand(commandId);

                var stockManager = new StockManager();
                if (stockManager.IsStockAvailabelForCommand(command))
                {
                    //Calcul du prix de la commande
                    double prixCommande = priceCalculator.GetPriceTtc(command);

                    //Vérification du client
                    bool isCommandinProgress = clientCrm.HasCommandInProgress(command.GetClientId());


                    return CheckClientAvailability(prixCommande, isCommandinProgress);
                }
                return new ValidateCommandProcessResult() { IsSucces = false, Message = "Not enoug product in stock" };

            }
            catch (Exception e)
            {
                return new ValidateCommandProcessResult() { IsSucces = false, Message = e.Message };
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

