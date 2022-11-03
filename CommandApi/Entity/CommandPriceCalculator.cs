using CommandApi.Controllers;

namespace CommandApi.Entity
{
    public class CommandPriceCalculator
    {
        internal double GetPriceTtc(Command command)
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
