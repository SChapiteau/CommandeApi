﻿using CommandApi.Controllers;
using CommandApi.DAL;
using CommandApi.Entity;
using CommandApi.Service;
using System.ComponentModel.Design;

namespace CommandApi.Process
{
    public class ValidateCommandProcess
    {
        private Command command;

        public ValidateCommandProcessResult ValidateCommande(int commandId)
        {
            try
            {
                InitialiseCommand(commandId);

                var stockManager = new StockManager();
                if (stockManager.IsStockAvailabelForCommand(commandId))
                {
                    //Calcul du prix de la commande
                    var calculator = new CommandPriceCalculator();
                    double prixCommande = calculator.GetPriceTtc(command);

                    //Vérification du client
                    var clientWrapper = new ClientCrmWrapper();
                    bool isCommandinProgress = clientWrapper.HasCommandInProgress(command.Client.Id); //!!Lois de demeter !


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
                return new ValidateCommandProcessResult() { IsSucces = false, Message = "Not enoug product in stock" };

            }
            catch (Exception e)
            {
                return new ValidateCommandProcessResult() { IsSucces = false, Message = e.Message };
            }
        }

        private bool isStockAvailabelForCommand()
        {
            //Use the command field
            throw new NotImplementedException();
        }

        private void InitialiseCommand(int commandId)
        {
            //fill the command filed
            throw new NotImplementedException();
        }

        
    }

    public class ValidateCommandProcessResult
    {
        public bool IsSucces { get; internal set; }
        public string Message { get; internal set; }
    }
}

