using CommandApi.Controllers;
using CommandApi.Entity;

namespace CommandApi.Service
{

    public interface ICommandePriceCalculator
    {
        double GetPriceTtc(Commande commande);
    }

    public class CommandePriceCalculator : ICommandePriceCalculator
    {
        public double GetPriceTtc(Commande commande)
        {
            double commandPrice = 0;
            foreach (var produitquandtite in commande.ProduitCommande)
            {
                double prixProduit = TarifHelper.GetPrixByProduit(produitquandtite.Item1.Id);
                prixProduit *= produitquandtite.Item2;
                commandPrice += prixProduit;
            }
            return commandPrice;

        }
    }
}
