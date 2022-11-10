namespace CommandApi.Entity
{
    public class Commande
    {
        public Client Client { get; set; }

        public List<Tuple<Produit, int>> ProduitCommande { get; set; }
        public EtatCommande Etat { get; set; }

        internal int GetClientId()
        {
            return Client.Id;
        }
    }

    public enum EtatCommande
    {
        NonPayer,
        Payer
    }
}