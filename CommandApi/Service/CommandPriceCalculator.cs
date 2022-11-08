using CommandApi.Controllers;
using CommandApi.Entity;

namespace CommandApi.Service
{

    public interface ICommandPriceCalculator
    {
        double GetPriceTtc(Command command);
    }

    public class CommandPriceCalculator : ICommandPriceCalculator
    {
        public double GetPriceTtc(Command command)
        {
            double commandPrice = 0;
            foreach (var produitquandtite in command.ProduitCommande)
            {
                double prixProduit = TarifHelper.GetPrixByProduit(produitquandtite.Item1.Id);
                prixProduit = prixProduit * produitquandtite.Item2;
                commandPrice += prixProduit;
            }
            return commandPrice;

        }
    }
}
