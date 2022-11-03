﻿using CommandApi.Controllers;
using CommandApi.DAL;
using CommandApi.Entity;
using CommandApi.Service;
using System.ComponentModel.Design;

namespace CommandApi.Process
{
    public class ValidateCommandProcess
    {
        public ValidateCommandProcessResult ValidateCommande(int commandId)
        {
            try
            {
                Command command = CommandRepository.Instance.GetCommand(commandId);

                if (CheckStock(command.ProduitCommande))
                {
                    //Calcul du prix de la commande
                    double prixCommande = 0;
                    foreach (var produitquandtite in command.ProduitCommande)
                    {
                        double prixProduit = TarifHelper.GetPrixByProduit(produitquandtite.Item1.Id);
                        prixProduit = prixProduit * produitquandtite.Item2;
                        prixCommande += prixProduit;
                    }

                    //Vérification du client
                    var urlWebServiceClient = ConfigurationHelper.getUrlCrm();
                    ClientWebService web = new ClientWebService(urlWebServiceClient);
                    Client client = web.GetClientCrm(command.Client.Id);

                    bool isCommandinProgress = false;
                    foreach (var cmd in client.CommandesClient)
                    {
                        if (cmd.Etat == EtatCommande.NonPayer)
                        {
                            isCommandinProgress = true; break;
                        }
                    }

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

        private bool CheckStock(List<Tuple<Produit, int>> produitCommande)
        {
            throw new NotImplementedException();
        }
    }

    public class ValidateCommandProcessResult
    {
        public bool IsSucces { get; internal set; }
        public string Message { get; internal set; }
    }
}

