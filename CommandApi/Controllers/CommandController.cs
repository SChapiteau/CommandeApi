using CommandApi.DAL;
using CommandApi.Entity;
using CommandApi.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CommandApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommandController : ControllerBase
    {

        //[HttpGet(Name = "ValidateCommand")]
        [HttpGet]
        public ApiResult ValidateCommand(int commandeId)
        {
           try
            {
                Command command = CommandeRepository.Instance.GetCommand(commandeId);

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
                        return new ApiResult() { IsSucces = false, Message = "A command is already in progress" };
                    }
                    else
                    {
                        return new ApiResult()
                        {
                            IsSucces = true,
                        };
                    }
                }
                return new ApiResult() { IsSucces = false, Message = "Not enoug product in stock" };

            }
            catch (Exception e)
            {
                return new ApiResult() { IsSucces = false, Message = e.Message };
            }

        }

        private bool CheckStock(List<Tuple<Produit, int>> produitCommande)
        {
            throw new NotImplementedException();
        }
    }
}