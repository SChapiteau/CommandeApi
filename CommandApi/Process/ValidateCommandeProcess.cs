using CommandApi.Entity;
using CommandApi.Entity.Interface;
using CommandApi.Service;

namespace CommandApi.Process
{
    public interface IValidateCommandeProcess
    {
        ValidateCommandProcessResult ValidateCommande(int commandId);

    }

    public class ValidateCommandeProcess : IValidateCommandeProcess
    {
        private Commande _commande;

        private readonly ICommandePriceCalculator priceCalculator;
        private readonly ICommandeRepository _commandeRepository;
        private readonly ICommandStockManager stockManager;
        private readonly IClientCrm clientCrm;

        public ValidateCommandeProcess(ICommandePriceCalculator commandePriceCalculator, IClientCrm clientCrm, ICommandeRepository commandeRepository, ICommandStockManager stockManager)
        {
            priceCalculator = commandePriceCalculator;
            this._commandeRepository = commandeRepository;
            this.stockManager = stockManager;
            this.clientCrm = clientCrm;
        }


        public ValidateCommandProcessResult ValidateCommande(int commandId)
        {
            try
            {
                InitialiseCommand(commandId);

                CheckStockAvailability();
                
                double prixCommande = priceCalculator.GetPriceTtc(_commande);

                bool isCommandInProgress = clientCrm.HasCommandInProgress(_commande.GetClientId());

                return CheckClientAvailability(prixCommande, isCommandInProgress);
            }
            catch (Exception e)
            {
                return new ValidateCommandProcessResult() { IsSucces = false, Message = e.Message };
            }
        }

        private void CheckStockAvailability()
        {
            if (!stockManager.IsStockAvailableForCommand(_commande))
            {
                throw new Exception("Not enough product in stock");
            }
        }

        private static ValidateCommandProcessResult CheckClientAvailability(double prixCommande, bool isCommandinProgress)
        {
            if (isCommandinProgress && prixCommande > 10000)
            {
                return new ValidateCommandProcessResult() { IsSucces = false, Message = "A commande is already in progress" };
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
            _commande = _commandeRepository.GetCommande(commandId);
        }

        
    }

    public class ValidateCommandProcessResult
    {
        public bool IsSucces { get; internal set; }
        public string Message { get; internal set; }
    }
}

