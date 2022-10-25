namespace CommandApi.Entity
{
    public class Command
    {
        public Client Client { get; set; }

        public List<Tuple<Produit, int>> ProduitCommande { get; set; }
        public EtatCommande Etat { get; set; }
    }

    public enum EtatCommande
    {
        NonPayer,
        Payer
    }
}